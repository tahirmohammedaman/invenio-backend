# Invenio API

**Backend service** for **Invenio**, an inventory and warehouse management system covering the full loop from supplier to shelf: products, stock levels across multiple warehouses, supply (restocking) orders, and customer sale orders — with a delivery workflow designed to be triggered by real hardware, not just a form submit button.

🔗 Frontend client: [invenio-frontend](https://github.com/tahirmohammedaman/invenio-frontend)

## Features

- **Product catalog** — products, categories, pricing, and min/max order quantities.
- **Multi-warehouse stock tracking** — per-warehouse stock quantities, SKUs, and configurable low-stock thresholds.
- **Supply orders (restocking)** — order inventory from suppliers, with lead-time-based delivery date estimation and automatic stock top-up on delivery confirmation.
- **Sale orders** — customer-facing orders drawn against live stock.
- **Suppliers & customers** — full CRUD with relational integrity to products, supplies, and orders.
- **Dashboard/reporting endpoint** — aggregate figures for the admin console.
- **Role-based JWT auth** — bearer-token authentication with `Admin`-gated mutation endpoints.
- **Queryable API** — OData-backed endpoints (`$filter`, `$select`, `$orderby`, `$top`/`$skip`) for every resource, so the client can paginate and search without bespoke query params.
- **Transactional email** — automatic notification emails (MailKit/MimeKit) fired on new supply orders and on delivery confirmation.

## Hardware-triggered delivery confirmation

Receiving stock is normally the most manual step in a warehouse — someone has to open the app, find the order, and click "delivered." Invenio's `POST /api/supplyorders/{id}/delivery` endpoint is deliberately hardware-agnostic (it just needs an order ID) so it can be triggered from more than a UI button:

- **QR code, from a phone** — the companion frontend can scan a QR/barcode with a phone camera and hit this endpoint directly, so a warehouse worker confirms a delivery by scanning a label instead of navigating a form. *(Implemented in `invenio-frontend`.)*
- **RFID, from a microcontroller** — because delivery confirmation is a single authenticated REST call keyed on the order ID, the same endpoint is a natural fit for a dock-mounted **RFID reader on a microcontroller** (e.g. an ESP32/Arduino with an RC522/PN532 reader) that scans a tagged pallet or crate and posts the confirmation over Wi-Fi — enabling hands-free receiving at the physical warehouse door, no phone or workstation required.

Either path runs through the same logic: mark the order delivered, atomically increment (or create) the corresponding `Stock` row for that product/warehouse, and fire the notification email.

## Tech stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 (C#), Web API |
| Data access | Entity Framework Core 8 + Npgsql (PostgreSQL) |
| Query layer | Microsoft.AspNetCore.OData |
| Auth | JWT bearer auth with role-based authorization |
| Mapping | AutoMapper (entity ⇄ DTO) |
| Email | MailKit / MimeKit |
| API docs | Swashbuckle (Swagger / OpenAPI) |
| Architecture | Repository pattern (`IRepositoryWrapper`) over EF Core, DTO layer per resource |

## Domain model

`Product` → `Category`, `Supply` (supplier offering), `Stock` (per-warehouse quantity), `SaleOrder`
`Supply` → `Supplier`, `SupplyOrder` (restock request against a `Warehouse`)
`Warehouse` → `Stock`, `SupplyOrder`
`User` → role-based (`Admin` / standard) JWT identity

## Getting started

```bash
dotnet restore
# configure appsettings.Development.json: PostgreSQL connection string, JWT key, mail config
dotnet ef database update
dotnet run
```

Swagger UI is available at `/swagger` once running, with a bearer-token auth flow pre-wired for testing protected endpoints.
