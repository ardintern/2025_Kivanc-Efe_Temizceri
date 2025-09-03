
using ECommerceApi.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Debugging;
using Serilog.Sinks.Elasticsearch;
using Elastic.Transport;

SelfLog.Enable(msg =>
{
    System.IO.File.AppendAllText("serilog-selflog.txt", msg + Environment.NewLine);
});

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var esUri = configuration["Elasticsearch:Uri"] ?? "https://localhost:9200";
var esUser = configuration["Elasticsearch:Username"] ?? "elastic";
var esPass = configuration["Elasticsearch:Password"] ?? "qwe123qwe";
var esFingerprint = configuration["Elasticsearch:Fingerprint"]; 


var loggerConfig = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(esUri))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "webapi-logs-{0:yyyy.MM.dd}",
        ModifyConnectionSettings = s =>
        {
            
            s = s.BasicAuthentication(esUser, esPass);

            
            if (!string.IsNullOrWhiteSpace(esFingerprint))
            {
                
                s = s.CertificateFingerprint(esFingerprint);
            }
            else
            {
                
             s = s.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
            }

            return s;
        }
    });


Log.Logger = loggerConfig.CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, services, lc) =>
    {
        
        lc.ReadFrom.Configuration(ctx.Configuration)
          .Enrich.FromLogContext()
          
          .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(esUri))
        {
           AutoRegisterTemplate = true,
           IndexFormat = "webapi-logs-{0:yyyy.MM.dd}",
           ModifyConnectionSettings = s =>
              {
               s = s.BasicAuthentication(esUser, esPass);
               if (!string.IsNullOrWhiteSpace(esFingerprint))
               s = s.CertificateFingerprint(esFingerprint);
               else
               s = s.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
               return s;   
              }
       });
    });

    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngularApp", policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();
    app.UseCors("AllowAngularApp");
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
