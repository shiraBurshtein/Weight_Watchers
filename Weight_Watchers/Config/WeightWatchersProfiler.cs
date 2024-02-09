using AutoMapper;
using Subscriber.CORE.DTO;
using Subscriber.Data.entites;

namespace Subscriber.WebApi.Config
{
    public class WeightWatchersProfiler:Profile
    {

        public WeightWatchersProfiler()
        {
            CreateMap<Subscribers, SubscriberDTO>().ForMember(dest => dest.Height, opt => opt.Ignore()).ReverseMap();
        }
    }
}
