using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using OpenAiProxy;
using System.Text.Json;

namespace OpenAiAssistant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssistantController : ControllerBase
    {
        private readonly ILogger<AssistantController> _logger;
        private readonly IOpenAiClient _openAiClient;
        private readonly OpenAiSettings _openAiSettings;


        public AssistantController(ILogger<AssistantController> logger
            , IOptions<OpenAiSettings> openAiSettings, IOpenAiClient openAiClient)
        {
            _logger = logger;
            _openAiClient = openAiClient;
            _openAiSettings = openAiSettings.Value;
        }

        [HttpGet(Name = "ListAssistants")]
        public async Task<string> ListAssistants()
        {
            var ret = await _openAiClient.AssistantsGetAsync(0, Order.Asc, null, null);
            return JsonSerializer.Serialize(ret);
        }
    }
}