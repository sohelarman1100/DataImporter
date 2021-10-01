using Autofac;
using DataImporter.Common.DateTimeUtilities;
using DataImporter.Common.Methods;
using DataImporter.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();

            builder.RegisterType<FileDownload>().As<IFileDownload>().InstancePerLifetimeScope();

            builder.RegisterType<DateTimeUtility>().As<IDateTimeUtility>().InstancePerLifetimeScope();

            builder.RegisterType<CopyExcelDataToList>().As<ICopyExcelDataToList>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
