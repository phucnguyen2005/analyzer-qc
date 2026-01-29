using AnalyzerQC;
using AnalyzerQC.WebApi.Database;
using AnalyzerQC.WebApi.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/seed-groups-and-models",async (AppDbContext dbContext) =>
    {
        
        //todo: doc file csv
        List<ModelGroupCsvDto> modelGroupDtos = [];

        // parse entity
        var modelGroupEntities = modelGroupDtos
            .Select(x => new ModelGroup(
                x.ModelGroupName,
                x.ModelGroupCode))
            .ToList();

        //todo: doc file csv 2 
        List<ModelCsvDto> modelDtos = [];

        // parse entity
        var modelEntities = modelDtos
            //string modelCode, string modelName, int modelGroupId
            .Select(x =>
            {
                var mg = modelGroupEntities
                    .FirstOrDefault(m => m.ModelGroupCode == x.ModelGroupCode);
                // todo: what if the mg null
                if (mg == null) throw new Exception("Model group code not found");
                return new Model(
                    x.ModelCode,
                    x.ModelName,
                    mg.Id); //todo: Id will be null for sure
            })
            .ToList();

        // call db context and add to db 
        await dbContext.ModelGroups.AddRangeAsync(modelGroupEntities); // TODO: async please
        await dbContext.Models.AddRangeAsync(modelEntities);

        dbContext.SaveChanges();

        return "Ok";
    })
    .WithName("Seed master data - Model groups and models");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
