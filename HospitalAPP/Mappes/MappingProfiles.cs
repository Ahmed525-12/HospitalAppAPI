using AutoMapper;
using HospitalDomain.DTOS;
using HospitalDomain.Entites.Identity;

namespace HospitalAPP.Mappes
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Guest, GuestDto>().ReverseMap();
        }
    }
}