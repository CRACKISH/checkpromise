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
- Don't invest time in Backend changes without confirming the scope with the user first.
