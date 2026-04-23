# Checkpromise — Frontend

Angular application for the [checkpromise.info](https://www.checkpromise.info) project — a tracker for publicly made promises by the Ukrainian government.

## Stack

- **Angular 21** — zoneless change detection (`provideZonelessChangeDetection`), signals-first (`signal`, `computed`, `httpResource`); `zone.js` is not used
- **Angular Material 21** — default `indigo-pink` prebuilt theme
- **ng2-charts 10** + **chart.js 4** — line chart on the indicator page (`BaseChartDirective` + `provideCharts`)
- **TypeScript 6**, **RxJS 7** (Angular peer dep only — no direct imports in app code)
- **@angular/build** (esbuild-based application builder)

Node **22+** recommended. The project builds and runs on Node 24, though Angular CLI prints a "not supported" warning for non-LTS Node versions.

## Install

```bash
npm install
```

## Dev server

```bash
npm start              # ng serve on http://localhost:4200
```

## Build

```bash
npm run build                                 # development build
npx ng build --configuration production       # production build
```

Output goes to `dist/checkpromise/browser/`.

## Unit tests

```bash
npm test
```

Tests run under Karma + Jasmine; tests are set up for zoneless change detection (`provideZonelessChangeDetection()` in `TestBed`).

## Lint

```bash
npm run lint
```

Uses `@angular-eslint` with `@typescript-eslint`. (There is no `tslint` — it was removed as part of the Angular 21 upgrade.)

## Project structure

```
src/
  app/
    components/              feature components (indicator, main-page, dialogs, etc.)
    services/                DataService (httpResource), ChangeCurrencyService (signal)
    models/                  DTO classes
    app.module.ts            NgModule root (provideZoneless, provideCharts, providers)
    app-routing.module.ts
  assets/data/data.json      promises + indicators dataset
  _shared.scss               shared SCSS mixins (e.g. link())
  styles.scss                global styles, Material theme, font imports, dialog/snackbar panel overrides
  polyfills.ts               empty — zone.js removed
```

## Notes

- The indicator chart expects `chart.js` dataset options (see `indicator-info-page.component.ts`).
- Material 21 tokens are overridden via `panelClass` for the donate dialog (`.donate-dialog-panel`) and the subscribe snack bar (`.subscribe-snack-bar-panel`) — see `src/styles.scss`.
- HTTP data is loaded as a single JSON file (`assets/data/data.json`) through `httpResource`; both the main list and the indicator detail page derive off the same resource via `computed`.
