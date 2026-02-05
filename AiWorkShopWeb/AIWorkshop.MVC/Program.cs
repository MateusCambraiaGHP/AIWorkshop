using AIWorkshop.MVC.Application.Handlers;
using AIWorkshop.MVC.Application.Handlers.Interfaces;
using AIWorkshop.MVC.Application.Services;
using AIWorkshop.MVC.Application.Services.Interfaces;
using AIWorkshop.MVC.Components;
using AIWorkshop.MVC.Data;
using AIWorkshop.MVC.Infrastructure.Utils.Agents.PromptAnalysis;
using AIWorkshop.MVC.Infrastructure.Utils.Agents.PromptAnalysis.Interfaces;
using AIWorkshop.MVC.Infrastructure.Utils.Factories;
using AIWorkshop.MVC.Infrastructure.Utils.Factories.Interfaces;
using AIWorkshop.MVC.Infrastructure.Utils.Helpers;
using AIWorkshop.MVC.Infrastructure.Utils.Helpers.Interfaces;
using AIWorkshop.MVC.Infrastructure.Utils.Models;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
});

builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = 32 * 1024;
    options.StreamBufferCapacity = 10;
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
});

// Configure Blazor Server circuit options
builder.Services.AddServerSideBlazor(options =>
{
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
    options.DisconnectedCircuitMaxRetained = 100;
    options.JSInteropDefaultCallTimeout = TimeSpan.FromSeconds(30);
    options.MaxBufferedUnacknowledgedRenderBatches = 10;
});

// Configuration
builder.Services.Configure<AIAgentOptions>(builder.Configuration.GetSection("OpenAI"));

// Infrastructure
builder.Services.AddSingleton<IPromptLoader, PromptLoader>();
builder.Services.AddSingleton<IAIAgentFactory, AIAgentFactory>();

builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlite("Data Source=aiworkshop.db;Cache=Shared;Mode=ReadWriteCreate",
        sqliteOptions => sqliteOptions.CommandTimeout(30)));

// Session for user tracking
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(24);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

// Agents
builder.Services.AddTransient<IPromptAnalysisAgent, PromptAnalysisAgent>();

// Services 
builder.Services.AddScoped<IPromptAnalysisHandler, PromptAnalysisHandler>();
builder.Services.AddScoped<IUserService, UserService>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 200;
    options.Limits.MaxConcurrentUpgradedConnections = 200;
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
});

var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseSession();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();