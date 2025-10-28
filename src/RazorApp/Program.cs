using AppDataModels.Config;
using AppServices;
using AppServices.Caching;
using AppServices.Quotes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add memory caching services.
builder.Services.AddMemoryCache();

// Bind StockQuotesOptions from configuration
builder.Services.Configure<StockQuotesApiOptions>(
    builder.Configuration.GetSection(StockQuotesApiOptions.SectionName)
);

// Register services
builder.Services.AddScoped<ITickerCache, TickerCache>();

// Use mockApiMode to demo the app without calling the real stock quotes API
bool mockApiMode = (builder.Configuration["MockApiMode"] ?? "false") == "true";
if (mockApiMode)
{
    builder.Services.AddScoped<IStockQuoteService, MockStockQuoteService>();
}
else
{
    builder.Services.AddScoped<IStockQuoteService, StockQuoteService>();
}
builder.Services.AddScoped<ITickerService, TickerService>();

// Ensure we make IHttpClientFactory available for services that need it
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-8.0
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Configure the HTTP request pipeline.
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
