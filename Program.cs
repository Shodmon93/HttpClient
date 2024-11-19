using RecipeFinder.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/as
builder.Services.AddControllers();
builder.Services.AddSingleton<RecipeService>();
builder.Services.AddHttpClient("SpoonacularClient", client =>
{
    client.BaseAddress = new Uri("https://api.spoonacular.com/");
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("Accept","application/json");
    client.DefaultRequestHeaders.Add("User-Agent","RecipeFinderApp");
    var apiKey = Environment.GetEnvironmentVariable("SpoonacularApiKey");
    client.DefaultRequestHeaders.Add("x-api-key", apiKey);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
