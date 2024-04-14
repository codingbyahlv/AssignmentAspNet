using WebApp.ViewModels.Sections;

namespace WebApp.ViewModels.Views
{
    public class HomeIndexViewModel
    {
        public string Title { get; set; } = "Start";
        public SubscribeSectionViewModel SubscribeSection { get; set; } = new SubscribeSectionViewModel();
    }
}
