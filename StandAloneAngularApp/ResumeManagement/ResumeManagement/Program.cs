using Microsoft.EntityFrameworkCore;
using ResumeManagement.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
     .AddNewtonsoftJson(option =>
     {
         option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
         option.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
     });
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("con")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: 
 "{controller}/{action}/{id?}");
app.UseCors(x =>
{
    x.AllowAnyHeader(); x.AllowAnyMethod(); x.AllowAnyOrigin();
});
app.Run();
