﻿using System.Threading.Tasks;
using Application.Results;
using Application.Results.Implementation;
using Application.Storage.Table.Model;
using Application.Storage.Table.Providers;

namespace Application.Storage.Table.Repository
{
    public class ProcessStatusRepository : Repository<ProcessStatus>, IProcessStatusRepository
    {
        public ProcessStatusRepository(
            ITableClientProvider tableClientProvider) 
            : base(tableClientProvider)
        {
        }

        public async Task CreateOrUpdate(string correlationId, string status, string information)
        {
            await this.CreateOrUpdate(new ProcessStatus(correlationId, status, information));
        }

        public async Task<IDataResult<IProcessStatus>> GetByCorrelationId(string correlationId)
        {
            var result = await this.GetById(correlationId);

            if (!result.Success)
            {
                return new FailedDataResult<IProcessStatus>();
            }
            return new SuccessfulDataResult<IProcessStatus>(result.Value);
        }
    }
}