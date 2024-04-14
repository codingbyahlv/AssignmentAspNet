using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Sections;

public class SubscribeSectionViewModel
{
    [Required]
    [Display(Name = "Email", Prompt = "Your email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Daily NewsLetter")]
    public bool DailyNewsLetter { get; set; }

    [Display(Name = "Advertising Updates")]
    public bool AdvertisingUpdates { get; set; }

    [Display(Name = "Week In Review")]
    public bool WeekInReview { get; set; }

    [Display(Name = "Event Updates")]
    public bool EventUpdates { get; set; }

    [Display(Name = "Startups Weekly")]
    public bool StartupsWeekly { get; set; }

    [Display(Name = "Podcasts")]
    public bool Podcasts { get; set; }
}
