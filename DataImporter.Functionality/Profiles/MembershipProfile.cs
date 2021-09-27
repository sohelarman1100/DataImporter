using AutoMapper;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Entities;
//using DataImporter.Membership.BusinessObjects;
//using DataImporter.Membership.Entities;
//using DataImporter.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Profiles
{
    class MembershipProfile : Profile
    {
        public MembershipProfile()
        {
            CreateMap<Group, GroupBO>().ReverseMap();
            CreateMap<ImportedFiles, ImportedFileBO>().ReverseMap();
            CreateMap<ExportedFiles, ExportedFileBO>().ReverseMap();
            CreateMap<AllData, AllDataBO>().ReverseMap();
        }
    }
}
