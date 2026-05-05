# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Solution layout

`ElistaWeb.sln` is an Orchard Core 1.8.4 / .NET 8 multi-tenant CMS host. Projects compose the running site through Orchard's module system, not direct references at runtime — features are activated per tenant from the admin UI / setup recipe.

- **`ElistaWeb/`** — the web host (`Microsoft.NET.Sdk.Web`). `Startup.cs` calls `AddOrchardCms()` and toggles between `AddSetupFeatures("OrchardCore.AutoSetup")` in `DEBUG` and `AddAzureShellsConfiguration()` in `RELEASE` (shell info goes to Azure Blob in prod). `appsettings.json` holds the AutoSetup tenant + Azure Media/Shells config; the dev tenant `formAdria` is auto-created via the `formAdria` recipe with a SQLite DB. AutoSetup is **disabled in non-debug builds**, so the bundled admin password only works locally.
- **`ElistaTheme/`** (`Microsoft.NET.Sdk.Razor`) — the active theme. `Manifest.cs` declares `[Theme]`. Adds Razor + Liquid views in `Views/`, `placement.json`, recipes under `Recipes/`, frontend assets under `wwwroot/` (css/js/vendor/img/fonts/icons), and registers `IDataMigration` (`Migrations.cs`) that defines the `GeneralSettings` custom-settings content type (Email/Phone/ExchangeRate/Instagram fields).
- **`Carousel.OrchardCore/`** — Bootstrap 4 carousel widget module. `Manifest.cs` declares `[Module]` with deps `OrchardCore.ContentFields`, `OrchardCore.Flows`, `OrchardCore.Media`, `OrchardCore.Widgets`. `Migrations.cs` defines `Slide` (part) + `Carousel` (Widget stereotype with a `Slides` BagPart of `Slide`s).
- **`Commerce/`** — vendored fork of [OrchardCMS/OrchardCore.Commerce](https://github.com/OrchardCMS/OrchardCore.Commerce) (Nwazet Commerce port). Contains:
  - `OrchardCore.Commerce/` — main module (Products, ShoppingCart, Orders, ProductAttributes, Pricing, Migrations recipes for `Product`, `Order`, `productList`, etc.). Declares multiple `[Feature]`s in `Manifest.cs` (Core, SessionCartStorage, CommerceSettingsCurrencySelector) keyed off `CommerceConstants.Features.*`.
  - `MoneyDataType/`, `AddressDataType/` — primitive money/currency/address types used across the module.
  - `OrchardCore.Commerce.Tests/` — xUnit test project (note: still targets `net6.0` while everything else is `net8.0`; `TreatWarningsAsErrors=true`).
  - `SampleWebApp/` — a standalone Orchard host used for testing the Commerce module in isolation (per `Commerce/README.md`).

## Build / run / deploy

```
dotnet build --configuration Release                   # whole solution
dotnet run --project ElistaWeb                         # local dev (DEBUG → AutoSetup)
dotnet test Commerce/OrchardCore.Commerce.Tests        # xUnit tests
dotnet test Commerce/OrchardCore.Commerce.Tests --filter "FullyQualifiedName~AmountTests"   # single class
dotnet publish ElistaWeb -c Release -o <out>           # what the GH Action runs
```

Three GitHub Actions workflows in `.github/workflows/` (`master_formadria.yml`, `master_formadria2.yml`, `master_ugpweb2.yml`) build & deploy the same artifact to three Azure Web Apps on every push to `master` — code is shared, tenants differ.

## Things worth knowing before changing code

- **Modules are wired via Orchard's startup discovery, not the host's `Startup.cs`.** Each module/theme has its own `Startup : StartupBase` (e.g. `Carousel.OrchardCore/Startup.cs`, `ElistaTheme/Startup.cs`) that registers `IDataMigration`, drivers, display events, etc. Don't add module-level services to `ElistaWeb/Startup.cs`.
- **Content types are defined in C# migrations**, not the admin UI alone. `IDataMigration.CreateAsync` runs once; subsequent edits go in `UpdateFromNAsync` methods that bump the returned version (`ElistaTheme/Migrations.cs` already shows the pattern). Editing `CreateAsync` retroactively will **not** apply to tenants that already migrated.
- **Recipes** (`*.recipe.json` under `ElistaTheme/Recipes/`, `Commerce/OrchardCore.Commerce/Migrations/`) seed content / settings during setup or via `IRecipeMigrator`. The dev tenant runs `formAdria` recipe at autosetup; theme also ships `homepage`, `sr.homepage`, `sr`, `the` recipe variants for localized content.
- **Localization**: empty `Localization/sr/` and `Localization/hr/` folders exist for Serbian/Croatian PO files; `*.pot` template files live at the repo root (`Carousel.OrchardCore.pot`, `ElistaTheme.pot`, `OrchardCore.Commerce.pot`).
- **Production storage is Azure Blob.** `OrchardCore_Media_Azure` and `OrchardCore_Shells_Azure` connection strings are injected via Azure App Service settings (`OrchardCore__OrchardCore_Media_Azure__ConnectionString`, etc.) — they're intentionally blank in `appsettings.json`. Container `formadria` is shared; per-tenant scoping comes from `BasePath` (`{{ ShellSettings.Name }}-media`).
- **Commerce is a vendored fork**, not a NuGet reference. Bug fixes likely belong here; consider whether they should also flow upstream. Its tests still target `net6.0` even though the rest of the solution is `net8.0`.
- **Theme placement**: `ElistaTheme/placement.json` maps shape → zone/position and overrides shapes from referenced modules. Razor/Liquid views in `ElistaTheme/Views/` shadow those in `Carousel.OrchardCore/Views/` and `Commerce/OrchardCore.Commerce/Views/` by file name.
