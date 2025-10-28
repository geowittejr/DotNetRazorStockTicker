# Dot Net Razor Stock Ticker (ASP.NET Core Razor Pages)
A sample ASP.NET Core Razor Pages application that retrieves and displays live stock quotes using the free Finnhub.io API.  
This application also demonstrates the use of in-memory caching to cut down on some of the API calls. You can configure the
application to run in mock API mode, which uses a fake stock quote API to demo the functionality in an offline fashion.

---

## Stock Quotes API Assumptions (Using Finnhub.io)

- The stock exchange code for all Finnhub symbol lookup requests is "US" for US exchanges (NYSE, Nasdaq).
- The Finnhub "epsTTM" metric value is the last reported EPS value for a given stock symbol.
- The Finnhub "peTTM" metric value is the last reported PE Ratio value for a given stock symbol.

---

## Project Structure

```
DotNetRazorStockTicker/ # The main solution
│
├── src/
│   ├── AppData/        # Class library containing data models
│   ├── AppServices/    # Services class library
│   ├── RazorApp/       # The main Razor pages app
│
├── tests/
│   ├── AppTests/       # xUnit test project
│
├── .gitignore
├── DotNetRazorStockTicker.sln
└── README.md
```

---

## Features

- ASP.NET Core Razor Pages frontend  
- Integration with the Finnhub.io Stock Quotes API  
- API key configuration via `appsettings.json`, environment variables, or secrets.json in Visual Studio   
- Dependency injection configured in Program.cs 
- Unit tests (xUnit) for core business logic  

---

## Prerequisites

Make sure you have the following installed:

- .NET 8 SDK
- A valid API key for the stock quotes API (Finnhub.io)

---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/geowittejr/DotNetRazorStockTicker.git
cd DotNetRazorStockTicker
```

### 2. Configure the API Key

You can provide your API key in one of three ways:

#### Option 1 — Environment Variable

Set the environment variable:

**Windows (PowerShell):**
```bash
$env:StockQuotesApi__ApiKey = "your_api_key_here"
```

#### Option 2 — appsettings.Development.json

In `src/RazorApp/appsettings.Development.json`, add:
```json
{
  "StockQuotesApi": {
    "BaseUrl": "https://finnhub.io/api/v1",
    "ApiKey": "your_api_key_here"
  }
}
```

#### Option 3 — secrets.json in Visual Studio

Right-click on the RazorApp project in the solution explorer.
Choose the "Manage User Secrets" menu item.

Add the same ApiKey json to the secrets.json file shown above from
the appsettings.Development.json file.

Note: Do not commit your API key. Ensure it’s excluded via `.gitignore`.

---

### 3. Build and Run

```bash
dotnet restore
dotnet build
dotnet run --project src/RazorApp
```

Then open your browser and navigate to:

```
https://localhost:7184
```

---

### 4. Run the App in Demo Mode

In `src/RazorApp/appsettings.Development.json`, change the value of
MockApiMode to "true".

```json
 "MockApiMode": "true"
```
Then start the application and it will run in demo mode with a 
fake stock API that mimics a live one.

---


## License

This project is licensed under the MIT License.