using Microsoft.AspNetCore.Mvc;

namespace Session02.Controllers
{
    public class HomeController : Controller
    {
        public ContentResult Index()
        {
            ContentResult result = new ContentResult();

            result.Content= "Hello from Index";
            result.ContentType = "text/html";

            return result;
        }
    }
}
