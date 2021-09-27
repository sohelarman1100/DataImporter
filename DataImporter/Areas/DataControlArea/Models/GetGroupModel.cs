using Autofac;
using DataImporter.Functionality.Services;
//using DataImporter.Membership.Services;
using DataImporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class GetGroupModel
    {
        public Guid UserId { get; set; }
        private IGroupService _groupService;
        private ILifetimeScope _scope;

        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _groupService = _scope.Resolve<IGroupService>();
        }

        public GetGroupModel()
        {

        }
        public GetGroupModel(IGroupService groupService)
        {
            _groupService = groupService;
        }
        internal object GetAllGroups(DataTablesAjaxRequestModel tableModel)
        {
            var data = _groupService.GetAllGroups(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "GroupName" }),
                UserId);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                             record.GroupName,
                             record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
