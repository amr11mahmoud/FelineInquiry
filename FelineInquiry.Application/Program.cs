using FelineInquiry.Application;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

//Built-in logging
// Disable all logging providers (console, eventLog ...etc)
//builder.Logging.ClearProviders();
//// Add console as logging provider
//builder.Logging.AddConsole();
//// Add debug window as logging provider
//builder.Logging.AddDebug();
//// Add EventLog as logging provider, works ONLY on windows (windows app [event viewer])
//builder.Logging.AddEventLog();

//Serilog Logging Framework config
builder.Host.UseSerilog((
    HostBuilderContext context, IServiceProvider services,
    LoggerConfiguration loggerConfig) =>{
        loggerConfig
        //Read configuration setting from the Host (appsetting.json)
        .ReadFrom.Configuration(context.Configuration)
        //Read Registered services from IOC container
        .ReadFrom.Services(services);});

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();

