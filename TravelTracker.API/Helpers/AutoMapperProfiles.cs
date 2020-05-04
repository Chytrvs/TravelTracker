using AutoMapper;
using TravelTracker.API.Data;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,DetailedUserDTO>();
            CreateMap<Flight,DetailedFlightDTO>();
            CreateMap<Flight,FlightEndpointsDTO>();
            CreateMap<Airport,AirportResponseDTO>();
            CreateMap<UpdateFlightDTO,Flight>().ForMember(d=>d.FlightDate,d=>d
                                               .MapFrom(x=>FlightDateResolver.ResolveFlightDate(x.FlightDate)));
        }
    }
}