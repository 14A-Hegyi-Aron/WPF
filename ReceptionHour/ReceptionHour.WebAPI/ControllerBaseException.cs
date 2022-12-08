using Microsoft.AspNetCore.Mvc;

namespace ReceptionHour.WebAPI
{
    public static class ControllerBaseException
    {
        public static IActionResult Run(this ControllerBase controller, Func<IActionResult> action)
        {
            try
            {
                return action();
            }
            catch (ApiException ex)
            {
                return controller.StatusCode(501, new
                {
                    error = ex.Message
                });
            }
            catch (Exception ex)
            {
                try
                {
                    LogToFile(ex);
                }
                catch { }
#if DEBUG
                return controller.StatusCode(500, new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
#else
                return controller.StatusCode(500, new
                {
                    error = "Internal server error"
                });
#endif
            }
        }
        private static string? path = null;
        private static object lockLogFile = new object();
        private static void LogToFile(Exception ex)
        {
            if (path == null)
            {

                var configuraion = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                path = configuraion.GetSection("LogFile:Path").Value;
            }
            var fileName = Path.Combine(path, DateTime.Today.Year.ToString());
            if (!Directory.Exists(fileName))
                Directory.CreateDirectory(path);
            fileName = Path.Combine(fileName, DateTime.Today.Month.ToString());
            if (!Directory.Exists(fileName))
                Directory.CreateDirectory(path);
            fileName = Path.Combine(fileName, $"WebApiLog_{DateTime.Now:yyyy-MM-dd}.log");

            lock (lockLogFile)
            {
                using (var sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine($"[{DateTime.Now:HH:mm:ss}] {ex.Message}");
                    sw.WriteLine($"\t\t{ex.StackTrace?.Replace("\n", "\n\t\t")}");
                    sw.WriteLine();
                }
            }
        }
    }
}
