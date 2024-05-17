using DigitalWallet.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDbContexts(builder.Configuration);
 
builder.Services.ConfigureMultiCurrencyFeature();
builder.Services.ConfigureWalletFeature();
builder.Services.ConfigureTransactionFeature();
  
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCurrencyFeatures();
app.MapWalletFeatures();


app.Run();
 