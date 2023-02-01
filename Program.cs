using Elasticsearch.Demo;
using Microsoft.OpenApi.Models;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Elasticsearch.Demo", Version = "v1" });
});

//------------------------------[Instruction a ajouter don't forget]--------------------------------------

var settings = new ConnectionSettings().DefaultMappingFor<User>(x=>x.IndexName("users"));
builder.Services.AddSingleton<IElasticClient>(new ElasticClient(settings));
//--------------------------------------------[Fin]-------------------------------------------------------

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
