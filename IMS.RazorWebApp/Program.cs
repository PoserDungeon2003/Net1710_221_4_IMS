using Microsoft.EntityFrameworkCore;
using IMS.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionStringDB");

builder.Services.AddDbContext<Net1710_221_4_IMSContext>(options => options.UseSqlServer(connectionString));

app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
