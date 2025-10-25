# Dot Net Razor Stock Ticker (ASP.NET Core Razor Pages)
A sample ASP.NET Core Razor Pages application that displays live stock ticker data using a third-party Stock API.  
This repository also includes a supporting Services class library for API integration and an xUnit test project for unit testing the application logic.

---

## Project Structure

```
DotNetRazorStockTicker/
│
├── src/
│   ├── StockTickerRazorApp/          # Main Razor Pages application
│   ├── StockTickerServices/ # Services library (API integration, business logic)
│
├── tests/
│   ├── StockTickerTests/    # xUnit test project
│
├── .gitignore
├── DotNetRazorStockTicker.sln
└── README.md
```

---

## Features

- ASP.NET Core Razor Pages frontend  
- Integration with a third-party Stock Ticker API  
- Configurable API key management via `appsettings.json` or environment variables  
- Dependency injection for modular service design  
- Unit tests (xUnit) for core business logic  

---

## Prerequisites

Make sure you have the following installed:

- .NET 8 SDK
- Visual Studio 2022 or VS Code
- A valid API key for the stock ticker service (Finnhub)

---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/geowittejr/DotNetRazorStockTicker.git
cd DotNetRazorStockTicker
```

### 2. Configure the API Key

You can provide your API key in one of two ways:

#### Option 1 — Environment Variable (Recommended)

Set the environment variable:

**Windows (PowerShell):**
```bash
$env:STOCK_API_KEY = "your_api_key_here"
```

**macOS/Linux (bash):**
```bash
export STOCK_API_KEY="your_api_key_here"
```

#### Option 2 — appsettings.Development.json

In `src/StockTickerRazorApp/appsettings.Development.json`, add:
```json
{
  "StockApi": {
    "BaseUrl": "https://api.example.com",
    "ApiKey": "your_api_key_here"
  }
}
```

Note: Do not commit your API key. Ensure it’s excluded via `.gitignore`.

---

### 3. Build and Run

```bash
dotnet build
dotnet run --project src/StockTickerRazorApp
```

Then open your browser and navigate to:

```
https://localhost:5001
```

---

## Running Tests

To execute the xUnit test suite:

```bash
dotnet test
```

Example output:

```
Passed!  - Failed: 0, Passed: 12, Skipped: 0, Total: 12
```

---

## Services Library Overview

The `StockTickerServices` project includes:

- `IStockTickerService` – defines the contract for fetching stock data  
- `StockTickerService` – implements API integration and data mapping  
- `Models/` – contains domain models used across layers  
- `HttpClient` – registered in `Program.cs` via DI for the stock API  

---

## Configuration Overview

| Setting | Location | Description |
|----------|-----------|-------------|
| `StockApi:BaseUrl` | `appsettings.json` | Base URL for the 3rd-party stock ticker API |
| `StockApi:ApiKey` | Env Var / Secret | API key for authentication |

---

## Local Development Notes

- Logs output to console with configurable log levels.  
- Mocked services available for testing without hitting live APIs.  

---

## License

This project is licensed under the MIT License.