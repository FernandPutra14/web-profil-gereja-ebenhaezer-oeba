using Microsoft.AspNetCore.Mvc;
using PKMGerejaEbenhaezer.Web.Models;

namespace PKMGerejaEbenhaezer.Web.ViewComponents
{
    public class ChartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ChartModel chartModel)
        {
            return View(chartModel);
        }
    }
}
