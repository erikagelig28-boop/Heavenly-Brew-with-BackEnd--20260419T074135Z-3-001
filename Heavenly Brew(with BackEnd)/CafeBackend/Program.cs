var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 1. FIX: Define the CORS policy to allow the frontend to talk to the backend
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

//phone hotspot connected to a laptop, go to script.js find
//dont change anything
app.Urls.Add("http://0.0.0.0:5267");

// 2. FIX: Use the CORS policy (MUST be placed before MapControllers)
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
