using Microsoft.EntityFrameworkCore;
using IMS.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure the connection string
var connectionString = builder.Configuration.GetConnectionString("Net1710_221_4_IMSContext");

builder.Services.AddDbContext<Net1710_221_4_IMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Net1710_221_4_IMSContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
