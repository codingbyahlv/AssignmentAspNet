using Infrastructure_Api.Entities;
using Infrastructure_Api.Models;

namespace Infrastructure_Api.Factories;

public class SubscriberFactory
{
    public static SubscriberEntity Create(SubscriberModel model)
    {
        return new SubscriberEntity
        {
            Email = model.Email,
            DailyNewsLetter = model.DailyNewsLetter,
            AdvertisingUpdates = model.AdvertisingUpdates,
            WeekInReview = model.WeekInReview,
            EventUpdates = model.EventUpdates,
            StartupsWeekly = model.StartupsWeekly,
            Podcasts = model.Podcasts,
        };
    }
}
