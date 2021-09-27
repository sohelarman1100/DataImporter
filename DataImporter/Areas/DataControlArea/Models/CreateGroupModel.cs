using Autofac;
using AutoMapper;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Services;
//using DataImporter.Membership.BusinessObjects;
//using DataImporter.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class CreateGroupModel
    {
        [Required(ErrorMessage = "please provide your name"), MaxLength(50, ErrorMessage =
            "Group Name should be less than 50 charcaters")]
        public string GroupName { get; set; }

        public Guid UserId { get; set; }

        private IGroupService _groupService;
        private IMapper _mapper;
        private ILifetimeScope _scope;

        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _groupService = _scope.Resolve<IGroupService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        public CreateGroupModel()
        {

        }
        public CreateGroupModel(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }
        internal void CreateGroup()
        {
            var group = _mapper.Map<GroupBO>(this);

            _groupService.CreateGroup(group);
        }
    }
}
