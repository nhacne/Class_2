// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Azure.Functions.Worker;
// using Microsoft.Extensions.Logging;

// namespace Company.Function
// {
//     public class HttpTrigger_Class2
//     {
//         private readonly ILogger<HttpTrigger_Class2> _logger;

//         public HttpTrigger_Class2(ILogger<HttpTrigger_Class2> logger)
//         {
//             _logger = logger;
//         }

//         [Function("HttpTrigger_Class2")]
//         public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
//         {
//             _logger.LogInformation("C# HTTP trigger function processed a request.");
//             return new OkObjectResult("Welcome to Azure Functions!");
//         }
//     }
// }
