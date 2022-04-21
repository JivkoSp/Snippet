using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using SnippetApi.Data.Context;
using SnippetApi.Extensions;
using SnippetApi.Profiles;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddNewtonsoftJson(action => {

    action.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository();

builder.Services.AddDbContext<AppDbContext>(options => {

    options.UseSqlServer(builder.Configuration.GetConnectionString("SnippetApiDbConnection"));
});

builder.Services.AddAutoMapper(config => {

    config.AddProfile<GroupProfile>();
    config.AddProfile<CommandProfile>();
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
