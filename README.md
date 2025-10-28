# ASP.NET Core Razor Stock Ticker
A sample ASP.NET Core Razor Pages application that retrieves and displays live stock quotes using the free Finnhub.io API. This application also demonstrates the use of in-memory caching to cut down on some of the API calls. You can configure the application to run in mock API mode, which uses a fake stock quote API to demo the functionality in an offline fashion.

<img width="1339" height="719" alt="image" src="https://github.com/user-attachments/assets/b34f6d83-23eb-42d6-b4fa-5c44bb87a7be" />

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

- ASP.NET Core Razor Pages
- Integration with the Finnhub.io Stock Quotes API  
- API key configuration via `appsettings.json`, environment variables, or secrets.json in Visual Studio   
- Dependency injection is configured in Program.cs 
- Unit tests (xUnit) project added
- Caching of stock tickers using in-memory cache with a configurable TTL in appsettings.json
- Stock ticker status badges for each grid row (i.e., New, Old, Error, Rate Limit).
- Ability to configure auto-refresh of the watched stock quotes in the UI
- The stock symbols input textbox is seeded with 20 random stocks when running in development mode
- ARIA attributes added to enhance accessibility
- Mock API mode allows you to run a demo without calling the live stock quotes API
- Validate against the Finnhub API using this Postman collection: [Finnhub_API_Postman_Collection.json](https://github.com/geowittejr/DotNetRazorStockTicker/blob/main/Finnhub_API_Postman_Collection.json)

---

## Prerequisites

Make sure you have the following installed:

- .NET 8 SDK
- A valid API key for the stock quotes API (<a href="https://finnhub.io/register" target="_blank">Finnhub.io</a>)

---

## Finnhub API Assumptions

- The stock exchange code for all Finnhub symbol lookup requests is "US" for US exchanges (NYSE, Nasdaq).
- The Finnhub "epsTTM" metric value is the last reported EPS value for a given stock symbol.
- The Finnhub "peTTM" metric value is the last reported PE Ratio value for a given stock symbol.

---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/geowittejr/DotNetRazorStockTicker.git
cd DotNetRazorStockTicker
```

### 2. Configure the API Key

Register at <a href="https://finnhub.io/register" target="_blank">Finnhub.io/register</a> to generate an API key. Then, configure your API key for the application in one of three ways:

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
http://localhost:5250 
```

---

### 4. Run the App in Demo Mode

In `src/RazorApp/appsettings.Development.json`, change the value of
MockApiMode to "true".

```json
 "MockApiMode": "true"
```
Then restart the application and it will run in demo mode with a 
mocked stock quote API that returns fake data.

---


## License

This project is licensed under the MIT License.
