using AutoMapper;
using Looking2.Web.Domain;
using Looking2.Web.ViewModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BusinessFormData, BusinessFormViewModel>().ReverseMap();
            CreateMap<EventFormData, EventFormViewModel>().ReverseMap();
            CreateMap<ObjectId, string>().ConvertUsing(src => src.ToString());
            CreateMap<string, ObjectId>().ConvertUsing(src => new ObjectId(src));
        }
    }
}
