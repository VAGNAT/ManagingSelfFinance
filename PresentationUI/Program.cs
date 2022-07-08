using Blazorise;
using Blazorise.Bootstrap;
using PresentationUI.Services.Interfaces;
using PresentationUI.Services;
using PresentationUI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//services
builder.Services.AddSingleton<IManagingSelfFinanceServiceAPI, ManagingSelfFinanceServiceAPI>();
builder.Services.AddScoped<ICheck, CheckTypesService>();
builder.Services.AddScoped<IReport, ReportService>();
builder.Services.AddScoped<ICRUD<Expense>, ExpenseService>();
builder.Services.AddScoped<ICRUD<Income>, IncomeService>();
builder.Services.AddScoped<ICRUD<TypeExpense>, TypeExpenseService>();
builder.Services.AddScoped<ICRUD<TypeIncome>, TypeIncomeService>();

//blazorise
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
