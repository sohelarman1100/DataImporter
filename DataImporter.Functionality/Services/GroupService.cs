using AutoMapper;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Entities;
using DataImporter.Functionality.Exceptions;
using DataImporter.Functionality.UnitOfWorks;
//using DataImporter.Membership.BusinessObjects;
//using DataImporter.Membership.Entities;
//using DataImporter.Membership.Exceptions;
//using DataImporter.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Services
{
    public class GroupService : IGroupService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        private readonly IMapper _mapper;
        public GroupService(IFunctionalityUnitOfWork functionalityUnitOfWork, IMapper mapper)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
            _mapper = mapper;
        }

        public void CreateGroup(GroupBO group)
        {
            if (group == null)
                throw new InvalidParameterException("group info was not provided");

            var groupEntity = _mapper.Map<Group>(group);

            _functionalityUnitOfWork.Groups.Add(groupEntity);

            _functionalityUnitOfWork.Save();
        }

        public (IList<GroupBO> records, int total, int totalDisplay) GetAllGroups(int pageIndex,
            int pageSize, string searchText, string sortText, Guid UserId)
        {
            var groupData = _functionalityUnitOfWork.Groups.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? x => x.UserId == UserId : x => x.GroupName.Contains(searchText) 
                && x.UserId==UserId,
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from grp in groupData.data
                              select _mapper.Map<GroupBO>(grp)).ToList();

            return (resultData, groupData.total, groupData.totalDisplay);
        }

        public GroupBO EditGroup(int id)
        {
            var entityGroup = _functionalityUnitOfWork.Groups.GetById(id);
            var groupBO = _mapper.Map<GroupBO>(entityGroup);

            return groupBO;
        }

        public void UpdateGroup(GroupBO groupInfo)
        {
            if (groupInfo == null)
                throw new InvalidOperationException("group is missing");

            var entityGroup = _functionalityUnitOfWork.Groups.GetById(groupInfo.Id);

            if (entityGroup != null)
            {
                //_mapper.Map(groupInfo, entityGroup);
                entityGroup.GroupName = groupInfo.GroupName;

                _functionalityUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("couldn't find group");
        }

        public string GetGroupById(int grpId)
        {
            var entityGroup = _functionalityUnitOfWork.Groups.GetById(grpId);
            return entityGroup.GroupName;
        }

        public void DeleteGroup(int id)
        {
            _functionalityUnitOfWork.Groups.Remove(id);

            _functionalityUnitOfWork.Save();
        }
    }
}
