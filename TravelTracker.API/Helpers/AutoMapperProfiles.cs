using AutoMapper;
using TravelTracker.API.Data;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DetailedUserDTO,User>().ReverseMap();
        }
    }
}