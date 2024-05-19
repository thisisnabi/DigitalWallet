using Carter;
using DigitalWallet.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDbContexts(builder.Configuration);
builder.Services.ConfigureValidator();
 
builder.Services.ConfigureMultiCurrencyFeature();
builder.Services.ConfigureWalletFeature();
builder.Services.ConfigureTransactionFeature();
builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();

app.Run();
 