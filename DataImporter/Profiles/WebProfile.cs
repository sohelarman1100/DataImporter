using AutoMapper;
using DataImporter.Areas.DataControlArea.Models;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Membership.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateGroupModel, GroupBO>().ReverseMap();
            CreateMap<EditGroupModel, GroupBO>().ReverseMap();
        }
    }
}
