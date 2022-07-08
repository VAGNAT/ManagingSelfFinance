using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Model;
using Presentation;
using Services;
using Services.Interfaces;
using System.Reflection;

//Logger
var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Managing self finance API",
        Description = "Management of own finances",
        TermsOfService = new Uri("https://example.com/terms"),
        
        Contact = new OpenApiContact
        {
            Name = "Vladimir Gnatyuk",
            Email = "gna.vladimir@gmail.com",
            Url = new Uri("https://t.me/V_A_GNAT"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
}
    );

//DB
builder.Services.AddDbContext<FinanceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repository
builder.Services.AddScoped<IRepository<Expense>, ExpenseRepository>();
builder.Services.AddScoped<IRepository<Income>, IncomeRepository>();
builder.Services.AddScoped<IRepository<TypeExpense>, TypeExpenseRepository>();
builder.Services.AddScoped<IRepository<TypeIncome>, TypeIncomeRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//services
builder.Services.AddScoped<ICRUD<Expense>, ExpenseService>();
builder.Services.AddScoped<ICRUD<Income>, IncomeService>();
builder.Services.AddScoped<ICRUD<TypeExpense>, TypeExpenseService>();
builder.Services.AddScoped<ICRUD<TypeIncome>, TypeIncomeService>();
builder.Services.AddTransient<IReport, ReportService>();

WebApplication app;
try
{
    logger.Trace("****** Start application ******");
    app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint("/swagger/v1/swagger.json", "Managing self finance");
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseExceptionHandlerMiddleware();

        app.Run();
    }
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    logger.Trace("****** Shutdown application ******");
    NLog.LogManager.Shutdown();
}
