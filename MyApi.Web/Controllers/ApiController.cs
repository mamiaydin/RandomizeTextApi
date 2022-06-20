using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApi.Data.Entities.Models;
using MyApi.Data.Infrastructure;
using MyApi.Web.Models;
using Newtonsoft.Json;

namespace MyApi.Web.Controllers
{
    
    [ApiController]
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IRepositoryManager _repositoryManager;

        public ApiController(ILogger<ApiController> logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }
        
        [HttpGet]
        [Authorize]
        [Route("get")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Route("translate/{text}"), AllowAnonymous, IgnoreAntiforgeryToken, DisableCors]
        public async Task<IActionResult> Translate(string text)
        {
          
            var responseModel = new ResponseModel();
            
            
            Object[] body = { new { text = text } };  
            var requestBody = JsonConvert.SerializeObject(body);

            using var client = new HttpClient();
            using var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;  
            //request.RequestUri = new Uri($"	https://api.funtranslations.com/translate/sith.json");   this api is not free to use
            request.RequestUri = new Uri($"https://random-words-api.vercel.app/word");  
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
    
            try
            {
                var response = await client.SendAsync(request);  
                var responseBody = await response.Content.ReadAsStringAsync();  
                responseModel = JsonConvert.DeserializeObject<ResponseModel>(responseBody.Substring(1, responseBody.Length-2));
                if (responseModel != null) responseModel.random_text = $"{text}{responseModel.word}";
            
                _repositoryManager.RequestsLog.Create(new RequestsLog
                {
                    Response = responseBody,
                    Text = text,
                    RandomText = responseModel?.random_text,
                    CreatedDate = DateTime.Now,
                    Guid = new Guid()
                });
                _repositoryManager.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            

            return Json(responseModel);
        }
    }
}