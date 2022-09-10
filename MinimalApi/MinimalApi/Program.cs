using MinimalApi.Controllers;
using MinimalApi.Data;
using MinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

string CORS = "MyCORS";
ApiContext context = new(builder.Configuration.GetConnectionString("PostgreSQL"));
UsersController controller = new(context);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy(name: CORS, builderCors => {
        builderCors.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

/*builder.Services.AddDbContext<ApiContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("MySQLConnection"));
});*/

var app = builder.Build();

app.UseCors(CORS);
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

/*----- ENDPOINTS -----*/

app.MapGet("/users/{userId}", async (int userId) => {
    var user = await controller.Get(userId);
    return Results.Ok(user);
});

app.MapGet("/users", async () => {
    var users = await controller.GetAll();
    return Results.Ok(users);
});

app.MapPost("/users", async (User user) => {
    var userAdded = await controller.Add(user);
    return Results.Created($"/users/{ userAdded.id }", userAdded);
});

app.MapPut("/users", async (User user) => {
    bool userWasUpdated = await controller.Update(user);
    return userWasUpdated ? Results.NoContent() : Results.BadRequest();
});

app.MapDelete("/users/{userId}", async (int userId) => {
    bool userWasDeleted = await controller.Delete(userId);
    return userWasDeleted ? Results.Ok() : Results.BadRequest();
});

app.Run();