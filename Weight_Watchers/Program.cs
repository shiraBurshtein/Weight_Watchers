//using Microsoft.EntityFrameworkCore;
//using Subscriber.Data;
//using Subscriber.WebApi.Config;
//using Serilog;


//var builder = WebApplication.CreateBuilder(args);

//ConfigurationManager configuration = builder.Configuration;
//builder.Services.AddDbContext<Weight_WatchersContext>(option =>
//{
//    ///הגדרתי לאיזה DB להתחבר 
//    option.UseSqlServer(configuration.GetConnectionString("Weight_WatchersConnectionString"));
//} );
//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.ConfigurationService();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddCors(opt => opt.AddPolicy("MyPolicy", policy => {
//    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
//})
//);
//builder.Host.UseSerilog();
//var app = builder.Build();
//app.UseSerilogRequestLogging();
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseCors("MyPolicy");
//app.UseAuthorization();

//app.MapControllers();
//app.UseMiddleware(typeof(ErrorHandlingMiddleware));
//app.Run();
using Microsoft.EntityFrameworkCore;
using Serilog;
using Subscriber.Data;
using Subscriber.WebApi.Config;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.ConfigurationService();
builder.Host.UseSerilog((context, configuration) =>
{

    configuration.ReadFrom.Configuration(context.Configuration);

});

builder.Services.AddDbContext<Weight_WatchersContext>(option =>
{

    option.UseSqlServer(configuration.GetConnectionString("Weight_WatchersConnectionString"));
});
//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt => opt.AddPolicy("MyPolicy", policy =>
{
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
})
);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseAuthorization();
app.UseCors("MyPolicy");
app.MapControllers();

app.UseMiddleware(typeof(ErrorHandlingMiddleware));

app.Run();

