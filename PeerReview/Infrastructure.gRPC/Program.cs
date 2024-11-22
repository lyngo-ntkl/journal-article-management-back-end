using Infrastructure.gRPC.Services;
using Infrastructure.gRPC;
using UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddUseCase();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ManuscriptApiImpl>();
app.MapGrpcService<ReviewRequestApi>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.MapSwaggerConfiguration();
app.Run();
