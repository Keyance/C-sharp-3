var builder = WebApplication.CreateBuilder(args);
{

    //configure DI
    builder.Services.AddControllers();
}


var app = builder.Build();
{
    //Configure Middleware
    app.MapControllers();
}


app.Run();
