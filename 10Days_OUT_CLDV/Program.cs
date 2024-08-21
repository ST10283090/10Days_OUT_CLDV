using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using _10Days_OUT_CLDV.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(new BlobService(configuration.GetConnectionString("AzureStorage")));

builder.Services.AddSingleton(new TableStorageService(configuration.GetConnectionString("AzureStorage")));

builder.Services.AddSingleton<QueueService>(sp =>
{
    var connectionString = configuration.GetConnectionString("AzureStorage");
    return new QueueService(connectionString, "transactions");
});

builder.Services.AddSingleton<AzureFileShareService>(sp =>
{
    var connectionString = configuration.GetConnectionString("AzureStorage");
    return new AzureFileShareService(connectionString, "productshare");
});



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
