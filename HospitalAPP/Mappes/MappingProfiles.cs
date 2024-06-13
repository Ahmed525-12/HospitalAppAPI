using AutoMapper;
using HospitalDomain.DTOS;
using HospitalDomain.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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