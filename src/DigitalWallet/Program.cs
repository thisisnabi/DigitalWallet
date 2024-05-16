using DigitalWallet.Features.Transactions.DecrementWalletBalance;
using DigitalWallet.Features.Transactions.IncrementWalletBalance;
using DigitalWallet.Features.Transactions.WalletFunds;
using DigitalWallet.Features.Transactions.WalletTransactions;
using DigitalWallet.Features.UserWallet.ChangeTitle;
using DigitalWallet.Features.UserWallet.Create;
using DigitalWallet.Features.UserWallet.Suspend;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WalletDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(WalletDbContextSchema.DefaultConnectionStringName));
});

builder.Services.ConfigureMultiCurrencyFeature();


builder.Services.AddScoped<WalletService>();
builder.Services.AddScoped<TransactionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCurrencyFeatures();

app.AddCreateUserWalletEndPoint();
app.AddChangeTitleWalletEndPoint();
app.AddSuspendWalletEndPoint();
app.AddIncrementWalletBalanceEndPoint();
app.AddDecrementWalletBalanceEndPoint();
app.AddGetWalletTransactionsEndPoint();
app.AddWalletFundsEndPoint();

app.Run();
