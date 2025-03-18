using Northwind.DataContext.SqlServer;
using Northwind.EntityModels;

#region Konfigurera web server host och tjänster
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddNorthwindContext();
builder.Services.AddRequestDecompression(); //lägger till compression tjänster (Accept-Encoding i


var app = builder.Build();
#endregion

#region Konfiguration av HTTP pipeline och routing
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.Use(async (HttpContext context, Func<Task> next) =>
{
    RouteEndpoint? rep = context.GetEndpoint() as RouteEndpoint;

    if (rep is not null)
    {
        WriteLine($"Endpoint name: {rep.DisplayName}");
        WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");
    }
    if (context.Request.Path == "/bonjour")
    {
        //ifall att vi har ett match på URL path, detta avbryter pipeline,
        //delegate skickar inte anrop till nästa middelware
        await context.Response.WriteAsync("Bonjour Monde!");
        return;
    }
    //vi kan modifiera request och response här innan anropet går vidare
    await next();

    //vi kan modifiera response här efter att vi har kallat next delegate
    //och innan den skickas till klienten
});

app.UseHttpsRedirection();
app.UseRequestDecompression(); //anrop till att använda decompression middelware i pipeline

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapRazorPages();

app.MapGet("/hello", () => $"Enviroment is {app.Environment.EnvironmentName}");
#endregion

//körs web servern
app.Run();
WriteLine("Detta exekveras efter att web server har stoppats!");
