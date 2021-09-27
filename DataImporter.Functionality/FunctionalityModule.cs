using Autofac;
using DataImporter.Functionality.Contexts;
using DataImporter.Functionality.Repositories;
using DataImporter.Functionality.Services;
using DataImporter.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality
{
    public class FunctionalityModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public FunctionalityModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FunctionalityDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<FunctionalityDbContext>().As<IFunctionalityDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<GroupRepository>().As<IGroupRepository>().InstancePerLifetimeScope();

            builder.RegisterType<FunctionalityUnitOfWork>().As<IFunctionalityUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<GroupService>().As<IGroupService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AllDataRepository>().As<IAllDataRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AllDataService>().As<IAllDataService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ImportedFileRepository>().As<IImportedFileRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ImportedFileService>().As<IImportedFileService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExportedFileRepository>().As<IExportedFileRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ExportedFileService>().As<IExportedFileService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
