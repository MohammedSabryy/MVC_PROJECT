using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

namespace Session02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            //builder.Services.AddControllers();
            //builder.Services.AddRazorPages();
            //builder.Services.AddMvc();


            var app = builder.Build();
            app.UseStaticFiles();

            //app.MapGet("/", () => "Hello World!");
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapControllerRoute(
                    name: "Default", pattern: "{Controller=Home}/{Action=Index}/{id:int?}"
                    );
            }
            );


            app.Run();
        }
    }
}
