using Imagile.Domain.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// var logger = app.Services.GetRequiredService<ILogger<Program>>();
// foreach(var file in Directory.GetFiles(Directory.GetCurrentDirectory()))
// logger.LogInformation(file);
// logger.LogWarning("File exists for cert:{certExists}", File.Exists(Path.Combine(Directory.GetCurrentDirectory(),"https","aspnetapp.pfx" )));
// logger.LogWarning("Using Environment: {env}", ImagileEnvironment.Get());
// Configure the HTTP request pipeline.
if (ImagileEnvironment.Get() == ImagileEnvironment.Types.Local)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
