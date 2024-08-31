using Microsoft.AspNetCore.Mvc;

namespace Session02.Controllers
{
    public class ProductsController : Controller

    {
        public ContentResult Get(int id)
        {
            ContentResult result = new ContentResult();

            result.Content = $"Product{id}";
            //result.ContentType = "object/pdf" ;
            

            return result;
        }
        public RedirectResult redirect()
        {
            RedirectResult redirectREsult = new RedirectResult("https://www.google.com");
            return redirectREsult;
        }
        public RedirectToActionResult redirectToActionResult()
        {
            RedirectToActionResult redirectToActionResult = new RedirectToActionResult("Get", "Products", new {id = 10 });
            return redirectToActionResult;
        }
    }
}
