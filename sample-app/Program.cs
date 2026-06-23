var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new { Message = "Hello from Azure DevOps CI/CD sample!", Environment = builder.Environment.EnvironmentName, Version = Environment.GetEnvironmentVariable("ARTIFACT_VERSION") ?? "1.0.0" });

app.Run();
