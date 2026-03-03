using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok(new { message = "Backend running" }))
   .WithName("Health");

app.MapGet("/api/events", async (AppDbContext db) =>
    await db.Events.AsNoTracking().ToListAsync())
   .WithName("GetEvents");

app.MapPost("/api/events", async (EventItem evt, AppDbContext db) =>
{
    db.Events.Add(evt);
    await db.SaveChangesAsync();
    return Results.Created($"/api/events/{evt.Id}", evt);
}).WithName("CreateEvent");

app.MapGet("/api/events/{id:int}", async (int id, AppDbContext db) =>
    await db.Events.FindAsync(id) is EventItem evt
        ? Results.Ok(evt)
        : Results.NotFound())
   .WithName("GetEventById");

app.MapDelete("/api/events/{id:int}", async (int id, AppDbContext db) =>
{
    var evt = await db.Events.FindAsync(id);
    if (evt is null)
    {
        return Results.NotFound();
    }

    db.Events.Remove(evt);
    await db.SaveChangesAsync();
    return Results.NoContent();
}).WithName("DeleteEvent");

// Apply pending EF Core migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Apply pending EF Core migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Serve static files and fallback to index.html for SPA
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapFallbackToFile("index.html");

app.Run();
