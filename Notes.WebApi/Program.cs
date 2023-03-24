using System.Reflection;
using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(INotesDBContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = builder.Services.BuildServiceProvider().CreateScope()) 
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<NotesDbContext>(); 
        DbInitializer.Initialize(context); 
    }
    catch(Exception e) { }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();