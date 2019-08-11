using SignUp.core.Ports;
using SignUp.core.ValueObjects;

using System;
using System.Threading.Tasks;

namespace SignUp.core
{
    public sealed class SignUpToCourseV2UseCase
    {
        private readonly ICourseLoader _courseLoader;
        private readonly ISignUpNotifier _signUpNotifier;

        public SignUpToCourseV2UseCase(ICourseLoader courseLoader, ISignUpNotifier signUpNotifier)
        {
            _courseLoader = courseLoader;
            _signUpNotifier = signUpNotifier;
        }

        public async Task<Result> Execute(Student student, Guid courseId)
        {
            var courseResult = await _courseLoader.GetCourse(courseId);
            Result result;
            if (courseResult.IsSuccess)
            {
                Course course = courseResult;
                result = course.RegisterStudent(student.Id);
                if (result.IsSuccess)
                {
                    result = await _courseLoader.UpdateCourse(course);
                }
                await _signUpNotifier.NotifyOfRegistrationOutcome(student, course, result.IsSuccess);
            }
            else
            {
                result = courseResult;
            }
            return result;
        }
    }
}
