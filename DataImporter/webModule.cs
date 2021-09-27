using Autofac;
using DataImporter.Areas.DataControlArea.Models;
using DataImporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter
{
    public class webModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GooglereCaptchaService>().AsSelf().InstancePerLifetimeScope();
            
            builder.RegisterType<CreateGroupModel>().AsSelf();
            builder.RegisterType<EditGroupModel>().AsSelf();
            builder.RegisterType<ExcelManageModel>().AsSelf();
            builder.RegisterType<GetExportedFilesModel>().AsSelf();
            builder.RegisterType<ExportFileModel>().AsSelf();
            builder.RegisterType<FileUploadModel>().AsSelf();
            builder.RegisterType<GetGroupModel>().AsSelf();
            builder.RegisterType<GetImportedFilesModel>().AsSelf();
            //builder.RegisterType<CreateGroupModel>().AsSelf();

            base.Load(builder);
        }
    }
}
