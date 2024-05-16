using DigitalWallet.Common.Persistence;
using DigitalWallet.Features.MultiCurrency;
using DigitalWallet.Features.MultiCurrency.Create;
using DigitalWallet.Features.MultiCurrency.UpdateRation;
using DigitalWallet.Features.UserWallet;
using DigitalWallet.Features.UserWallet.ChangeTitle;
using DigitalWallet.Features.UserWallet.Create;
using DigitalWallet.Features.UserWallet.Suspend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WalletDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(WalletDbContextSchema.DefaultConnectionStringName));
});

builder.Services.AddScoped<CurrencyService>();
builder.Services.AddScoped<WalletService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddCreateCurrencyEndPoint();
app.AddUpdateRationCurrencyEndPoint();
app.AddCreateUserWalletEndPoint();
app.AddChangeTitleWalletEndPoint();
app.AddSuspendWalletEndPoint();

app.Run();
