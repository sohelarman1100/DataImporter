using Autofac;
using DataImporter.Functionality.Services;
//using DataImporter.Membership.BusinessObjects;
//using DataImporter.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class GetGroupInfoModel
    {
        private readonly IGroupService _groupService;

        public GetGroupInfoModel()
        {
            //_groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }
        public GetGroupInfoModel(IGroupService groupService)
        {
            _groupService = groupService;
        }
        internal List<string> GetGroups()
        {
            //DataImporter.Membership.Entities.Group obj = new DataImporter.Membership.Entities.Group();
            List<string> lst = new List<string>();
            lst.Add("Id = 1; GroupName = friends;");
            lst.Add("Id = 2; GroupName = teacher;");
            return lst;
            //return _groupService.GetGroupInfo();
        }
    }
}
