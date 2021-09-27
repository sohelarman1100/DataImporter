using DataImporter.Worker.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataImporter.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IAllDataProcessModel _allDataProcessModel;
        private readonly IImportedFileProcessModel _importedFileProcessModel;

        public Worker(ILogger<Worker> logger, IAllDataProcessModel allDataProcessModel, 
            IImportedFileProcessModel importedFileProcessModel)
        {
            _logger = logger;
            _allDataProcessModel = allDataProcessModel;
            _importedFileProcessModel = importedFileProcessModel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                Console.WriteLine("HEllo!!!");

                _importedFileProcessModel.ImportFileInfo();

                await Task.Delay(20*1000, stoppingToken);
            }
        }
    }
}
