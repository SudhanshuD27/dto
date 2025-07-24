using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            LogErrorToFile(exception);

            context.Result = new JsonResult(new
            {
                success = false,
                message = exception.Message
            })
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }

        private void LogErrorToFile(Exception exception)
        {
            string logPath = Path.Combine(_env.WebRootPath, "logs");
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            string fullPath = Path.Combine(logPath, "error_log.txt");
            using (StreamWriter writer = new StreamWriter(fullPath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {exception.Message}");
                //writer.WriteLine(exception.Message);
                writer.WriteLine("---------------------------");
            }
        }
    }
}


