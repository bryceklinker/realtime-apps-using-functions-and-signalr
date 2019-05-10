using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Realtime.Presenter.Function.Presentations.Dtos;
using Realtime.Presenter.Function.Presentations.Services;

namespace Realtime.Presenter.Function.Presentations
{
    public class PresentationsController
    {
        private readonly IPresentationsService _presentationsService;

        public PresentationsController(IPresentationsService presentationsService)
        {
            _presentationsService = presentationsService;
        }
        
        
        [FunctionName("AddPresentation")]
        public async Task<IActionResult> AddAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "presentations")] [FromBody] PresentationDto presentation)
        {
            var dto = await _presentationsService.AddAsync(presentation);
            return new OkObjectResult(dto);
        }

        [FunctionName("GetAllPresentations")]
        public async Task<IActionResult> GetAllAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "presentations")] HttpRequestMessage req)
        {
            var result = await _presentationsService.GetAllAsync();
            return new OkObjectResult(result);
        }
    }
}