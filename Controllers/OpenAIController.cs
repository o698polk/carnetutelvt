using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;

namespace carnetutelvt.Controllers
{
    [Route("chatbot")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        [HttpPost]
        [Route("cahtutlvt")]
        public IActionResult ChatResponse([FromBody] string prompt)
        {
            string apikey = "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";
            string respuetsa = string.Empty;
            var chatbotIA = new OpenAIAPI(apikey);
            var completion = new CompletionRequest();
            completion.Prompt = prompt;
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 100;
            var result = chatbotIA.Completions.CreateCompletionAsync(completion);
            if (result != null)
            {
                foreach (var item in result.Result.Completions)
                {
                    respuetsa = item.Text;
                }
                return Ok(respuetsa);
            }
            else
            {
                return BadRequest("Not Found");
            }
        }
    }
}
