var builder = WebApplication.CreateBuilder(args);

string CORS = "MyCORS";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy(name: CORS, builderCors => {
        builderCors.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors(CORS);
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

/*----- ENDPOINTS -----*/

app.Run();