using System;
using System.Threading.Tasks;
using SignUp.core.Ports;
using SignUp.core.ValueObjects;

namespace SignUp.core
{
    public class SignUpToCourseV1UseCase
    {
        private readonly ICourseLoader _courseLoader;

        public SignUpToCourseV1UseCase(ICourseLoader courseLoader)
        {
            _courseLoader = courseLoader;
        }

        public async Task<Result> Execute(Guid studentId, int courseId)
        {
            var courseResult = await _courseLoader.GetCourse(courseId);
            Result result;
            if (courseResult.IsSuccess)
            {
                Course course = courseResult;
                result = course.RegisterStudent(studentId);
            }
            else
            {
                result = courseResult;
            };

            return result;
        }
    }
}
