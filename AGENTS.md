# Repo guide for AI agents

This repo hosts the [checkpromise.info](https://www.checkpromise.info) project — a tracker for publicly made promises by the Ukrainian government.

## Layout

```
Frontend/    Angular SPA (live, production)
Backend/    .NET backend (WIP — not used in production; not finished)
```

## Frontend — `Frontend/`

- **Angular 21**, zoneless change detection, signals-first
- Data source is a static JSON bundled with the app (`src/assets/data/data.json`) loaded via `httpResource`
- See `Frontend/README.md` for the full stack, commands, and structure
- When touching Frontend: no `zone.js`, no RxJS imports in app code (`rxjs` is only Angular's transitive peer dep), Material 21 component styling is overridden via `panelClass` + CSS tokens (`--mat-*`), not `::ng-deep`

## Backend — `Backend/`

- .NET **Core 2.2** (EOL), multi-project solution (`Checkpromise.sln`)
- Projects: `CheckPromise.Webapp`, `CheckPromise.BusinessLayer`, `CheckPromise.Data`, `CheckPromise.DTO`, `Checkpromise.Provider`, `CheckPromise.Uploader`
- **Status: unfinished**. The production site is served by the Frontend alone against the static JSON. The Backend is not wired up and has not been ported to a supported .NET version.

### Original intent

- A scheduled background service (hosted service / worker) that runs **once a day**, (re)generates `data.json` from upstream providers, and **uploads it via FTP** to the Frontend's hosting. The existing `CheckPromise.Uploader` project was the entrypoint for this job.
- Designed around **Onion architecture**: `CheckPromise.Data` (persistence) and `Checkpromise.Provider` (external data sources) as the outer infrastructure ring, `CheckPromise.BusinessLayer` as the inner domain/application ring, `CheckPromise.DTO` for boundary contracts, `CheckPromise.Webapp` and `CheckPromise.Uploader` as composition roots.
- Goal was to do it **"by the book"** — clean architecture, DI, async throughout, proper layering, tests. Short-term hack solutions should be avoided in favor of the clean design, unless explicitly approved.

### When picking this up

- Start with a target framework decision (port 2.2 → .NET 8 LTS or .NET 9) before touching code — .NET Core 2.2 tooling won't build on modern SDKs.
- Confirm the scope with the user before refactoring any Backend project.
