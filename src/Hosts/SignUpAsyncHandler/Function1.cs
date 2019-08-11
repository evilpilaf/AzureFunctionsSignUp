using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace SignUpAsyncHandler
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([QueueTrigger("SignUpRequests-queue", Connection = "CourseSignUpRequests")]
            string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
 