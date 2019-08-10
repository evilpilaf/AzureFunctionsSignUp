using System;

namespace SignUp.core.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException(int courseId)
            : base($"Course with Id {courseId} was not found")
        {

        }
    }
}
