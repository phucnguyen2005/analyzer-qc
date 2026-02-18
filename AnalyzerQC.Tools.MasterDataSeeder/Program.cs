using AnalyzerQC;
using AnalyzerQC.Application.Dtos;
using AnalyzerQC.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });


    // Define the security scheme (e.g., Bearer Authentication)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();
app.UseHttpsRedirection();


app.MapGet("/seed-groups-and-models", async (AppDbContext dbContext) =>
    {
        
        List<ModelGroupCsvDto> modelGroupDtos = [];
        using (StreamReader sr = new StreamReader("C:\\Users\\Admin\\Downloads\\model-groups.csv"))
        {
            var headerLine = sr.ReadLine(); // read and ignore header
            while (sr.ReadLine() is { } line)
            {
                
                var values = line.Split(',');
                modelGroupDtos.Add(new ModelGroupCsvDto(int.Parse(values[0]), values[1], values[2]));
            }
        }
        

        // parse entity
        var modelGroupEntities = modelGroupDtos
            .Select(x => new ModelGroup(
                x.ModelGroupName,
                x.ModelGroupCode))
            .ToList();
        
        
       
        List<ModelCsvDto> modelDtos = [];
        using (StreamReader sr = new StreamReader("C:\\Users\\Admin\\Downloads\\models.csv"))
        {
            var headerLine = sr.ReadLine(); // read and ignore header
            while (sr.ReadLine() is { } line)
            {
                var values = line.Split(',');
                modelDtos.Add(new ModelCsvDto(
                               int.Parse(values[0]),
                        values[1],
                        values[2], 
                    values[3], 
                      int.Parse(values[4])));
            }
        }


        for (int i = 0; i < modelDtos.Count; i++)
        {
            var a = modelDtos[i];
            var mg = modelGroupEntities
                .FirstOrDefault(m => m.ModelGroupCode == a.ModelGroupCode);
            if (mg == null) throw new Exception("Model group code not found");
            var modelEntity = new Model(
                a.ModelCode,
                a.ModelName,
                mg.Id);
            mg.AddModel(modelEntity);
        }
        // parse entity
        /*var modelEntities = modelDtos
            //string modelCode, string modelName, int modelGroupId
            .Select(x =>
            {
                var mg = modelGroupEntities
                    .FirstOrDefault(m => m.ModelGroupCode == x.ModelGroupCode);
                
                if (mg == null) throw new Exception("Model group code not found");
                return new Model(
                    x.ModelCode,
                    x.ModelName,
                    mg.Id); 
            })
            .ToList();*/

        // call db context and add to db 
        
        
        await dbContext.ModelGroups.AddRangeAsync(modelGroupEntities);
        await dbContext.SaveChangesAsync();

        return "Ok";
    })
    .WithName("Seed master data - Model groups and models");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
}

app.Run();