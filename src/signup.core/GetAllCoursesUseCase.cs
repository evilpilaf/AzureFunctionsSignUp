using SignUp.core.Ports;
using SignUp.core.ValueObjects;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignUp.core
{
    public sealed class GetAllCoursesUseCase
    {
        private readonly ICourseLoader _courseLoader;

        public GetAllCoursesUseCase(ICourseLoader courseLoader)
        {
            _courseLoader = courseLoader;
        }

        public Task<Result<IEnumerable<Course>>> Execute()
        {
            return _courseLoader.GetAllCourses();
        }
    }
}
