using System;

namespace SignUp.core.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException(Guid courseId)
            : base($"Course with Id {courseId} was not found")
        {

        }
    }
}
