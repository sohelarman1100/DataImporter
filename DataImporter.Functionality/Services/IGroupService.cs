//using DataImporter.Membership.BusinessObjects;
using DataImporter.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Services
{
    public interface IGroupService
    {
        void CreateGroup(GroupBO group);
        (IList<GroupBO> records, int total, int totalDisplay) GetAllGroups(int pageIndex, int pageSize,
            string searchText, string sortText, Guid UserId);
        GroupBO EditGroup(int id);
        void UpdateGroup(GroupBO groupInfo);
        string GetGroupById(int grpId);
        void DeleteGroup(int id);
        //object GetGroupInfo();
        //void DeleteStudent(int id);
    }
}
