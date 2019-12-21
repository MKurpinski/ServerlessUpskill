using System.Linq;
using System.Threading.Tasks;
using Application.Api.Events.External;
using Application.Api.Events.External.ApplicationAdded;
using Application.Commands.Commands;
using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Upskill.EventPublisher.Publishers;

namespace Application.Api.Functions.ApplicationProcess
{
    public class ApplicationProcessFinishedEventPublisher
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;

        public ApplicationProcessFinishedEventPublisher(
            IEventPublisher eventPublisher,
            IMapper mapper)
        {
            _eventPublisher = eventPublisher;
            _mapper = mapper;
        }

        [FunctionName(nameof(ApplicationProcessFinishedEventPublisher))]
        public async Task Run(
            [ActivityTrigger] IDurableActivityContext context)
        {
            var command = context.GetInput<SaveApplicationCommand>();

            var categoryUsedEvent = new CategoryUsedEvent(command.Category, $"{nameof(DataStorage.Models.Application)}_{command.Id}");
            await _eventPublisher.PublishEvent(categoryUsedEvent);

            var applicationAddedEvent = _mapper.Map<SaveApplicationCommand, ApplicationAddedEvent>(command);
            await _eventPublisher.PublishEvent(applicationAddedEvent);
        }
    }
}