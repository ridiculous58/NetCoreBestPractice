using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bp.Api.Service.Mappings
{
    public class AutoMapperMappingProfile : AutoMapperCustomProfile
    {
        public AutoMapperMappingProfile()
        {

            CreateMap<Bp.Api.Data.Models.Contact, Bp.Api.Models.ContactDTO>()
                .ForMember(x => x.FullName, y => y.MapFrom(z => $"{z.FirstName} {z.LastName}"))
                //.ForMember(x=>x.Id,y=>y.MapFrom(z=>z.Id))  //isimler aynı ise problem yok
                .ReverseMap();
        }
    }
}
