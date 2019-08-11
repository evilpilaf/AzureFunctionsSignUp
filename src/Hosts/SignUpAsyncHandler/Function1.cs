using Microsoft.Azure.WebJobs;

using Serilog;
using SignUp.core;

namespace SignUpAsyncHandler
{
    public class Function1
    {
        private readonly SignUpToCourseV2UseCase _signUpToCourseUseCase;
        private ILogger _log = Log.Logger;

        public Function1(SignUpToCourseV2UseCase signUpToCourseUseCase)
        {
            _signUpToCourseUseCase = signUpToCourseUseCase;
        }

        [FunctionName("Function1")]
        public void Run([QueueTrigger("SignUpRequests-queue", Connection = "CourseSignUpRequests")]
            string myQueueItem)
        {
            _log.Information($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
 