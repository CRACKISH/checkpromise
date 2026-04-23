# Repo guide for AI agents

This repo hosts the [checkpromise.info](https://www.checkpromise.info) project — a tracker for publicly made promises by the Ukrainian government.

## Layout

```
Frontend/    Angular SPA (live, production)
Backend/     .NET 8 worker service — builds data.json and uploads it via FTP
```

## Frontend — `Frontend/`

- **Angular 21**, zoneless change detection, signals-first
- Data source is a static JSON bundled with the app (`src/assets/data/data.json`) loaded via `httpResource`
- See `Frontend/README.md` for the full stack, commands, and structure
- When touching Frontend: no `zone.js`, no RxJS imports in app code (`rxjs` is only Angular's transitive peer dep), Material 21 component styling is overridden via `panelClass` + CSS tokens (`--mat-*`), not `::ng-deep`

## Backend — `Backend/`

- **.NET 8 LTS** worker service (no web API — the Frontend reads `data.json` directly from FTP hosting)
- Central package management (`Backend/Directory.Packages.props`) and common build props (`Backend/Directory.Build.props`) — all projects target `net8.0`
- Onion layering:

  ```
  CheckPromise.Data           -> EF Core 8 domain entities + DbContext (SQLite)
  CheckPromise.DTO            -> client contract (matches Frontend/src/assets/data/data.json shape)
  CheckPromise.BusinessLayer  -> IClientDataBuilder + Domain→DTO mapping (formatting, ordering)
  CheckPromise.Ingestion      -> IIndicatorDataSource + IIndicatorIngestionService + per-source scrapers/clients that upsert IndicatorValues
  Checkpromise.Provider       -> IClientDataProvider + FluentFTP implementation (IOptions<FtpClientDataProviderOptions>)
  CheckPromise.Uploader       -> composition root: Microsoft.NET.Sdk.Worker, BackgroundService running once a day
  ```

- **Daily pipeline** inside `UploaderWorker.RunOnceAsync`:

  ```
  IIndicatorIngestionService.IngestAllAsync()  -- scrape/call upstream sources, upsert into IndicatorValue
  IClientDataBuilder.BuildAsync()              -- read DB, map + format into DTO.ClientData
  IClientDataProvider.PushAsync(clientData)    -- serialize to JSON, push via FTP
  ```

- **Contract authority.** The canonical JSON contract lives in `Frontend/src/assets/data/data.json`. Any DTO change must keep the exact JSON shape (property names, string dates `dd.MM.yyyy`, money values as strings with 2 decimals, etc.).
- **Configuration.** Secrets (DB connection string, FTP credentials) must come from user-secrets / environment variables in dev, and env vars / vaults in prod. Never commit real credentials. The `appsettings.json` in `CheckPromise.Uploader/` ships with empty values as the schema.
- **Docker.** `Backend/CheckPromise.Uploader/Dockerfile` is a multi-stage build on `mcr.microsoft.com/dotnet/sdk:8.0` → `runtime:8.0`, non-root user. Build context is `Backend/` (so `Directory.*.props` siblings resolve). SQLite DB lives at `/app/data/checkpromise.db` — declared as a `VOLUME` so the file survives container restarts (mount a named volume or host path to `/app/data`).

- **Storage.** SQLite (chosen over SQL Server because volume is tiny: ~20 indicators × rare updates). DB file is auto-created on startup via `Database.EnsureCreatedAsync()` in `Program.cs`. No migrations yet — once the schema starts evolving, switch from `EnsureCreated` to `Migrate()` and generate migrations with `dotnet ef migrations add <name> --project CheckPromise.Data --startup-project CheckPromise.Uploader`.
- **Scheduling.** `UploaderWorker` uses `TimeProvider` + `PeriodicTimer`-style delay loop, runs on startup (if `Uploader:RunOnStartup`) and then daily at `Uploader:RunAt` (UTC). No Quartz/Hangfire — keep it in-process.
- **When touching Backend.** No `Newtonsoft.Json` (use `System.Text.Json`); no `WebClient` / `FtpWebRequest` (use `FluentFTP`); no Onion violations (DTO must not reference `CheckPromise.Data.Models`); favor async + `CancellationToken` at service boundaries.

- **Adding a new indicator source.** Drop a new class under `CheckPromise.Ingestion/Sources/<Provider>/` that implements `IIndicatorDataSource` (hard-code its `IndicatorId`, declare its `Cadence`, return `IndicatorDatapoint?` from `FetchLatestAsync`). Register it in `CheckPromise.Ingestion.ServiceCollectionExtensions.AddIndicatorIngestion`: `services.AddHttpClient<TSource>()` + `services.AddScoped<IIndicatorDataSource>(sp => sp.GetRequiredService<TSource>())`. The orchestrator picks up every registered `IIndicatorDataSource` via DI and catches per-source exceptions so one bad scraper does not fail the whole run. For HTML scraping of `index.minfin.com.ua`, add `AngleSharp` to `Directory.Packages.props` when the first HTML source is implemented.

- **Cadence + dedup semantics.** Each source declares its `IngestionCadence` (`Daily` / `Monthly` / `Quarterly` / `Yearly`). `IndicatorIngestionService`:
  - **Skips the fetch entirely** if the latest DB row for the indicator is still within the declared window (no wasted HTTP to minfin for a monthly indicator that already has this month's point). Uses `TimeProvider` so tests can freeze time.
  - **After fetching**, compares the incoming datapoint to the latest stored row — if `Value`/`Value2`/`Quantity` all match, it does NOT insert a duplicate row. A new row lands only when the value actually changed. This keeps `IndicatorValue` sparse and is also the mechanism preventing duplicates in the generated `data.json` (the builder reads from DB, so fewer rows → cleaner output).

- **Implemented sources so far.** `Sources/Nbu/NbuUsdExchangeRateSource` (IndicatorId=1, `Cadence.Daily`, USD/UAH via NBU JSON API `bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode=USD&json`). Minfin scrapers are not implemented yet — each page has its own HTML layout and must be added one at a time with the actual markup on hand.
