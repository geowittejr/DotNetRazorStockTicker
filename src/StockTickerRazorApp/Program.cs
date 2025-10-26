using StockTickerData.ConfigOptions;
using StockTickerServices.Quotes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Bind StockQuotesOptions from configuration
builder.Services.Configure<StockQuotesOptions>(
    builder.Configuration.GetSection(StockQuotesOptions.SectionName)
);

// Register StockQuoteService
builder.Services.AddScoped<IStockQuoteService, StockQuoteService>();

// Ensure we make IHttpClientFactory available for services that need it
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-8.0
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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
