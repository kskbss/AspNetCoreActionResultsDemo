using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreActionResultsDemo.Components
{
    [ViewComponent(Name = "ViewComponentExample")]
    public class ViewComponentExample : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
