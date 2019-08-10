using System;
using SignUp.core.ValueObjects;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignUp.core.Ports
{
    public interface ICourseLoader
    {
        Task<Result<IEnumerable<Course>>> GetAllCourses();
        Task<Result<Course>> GetCourse(Guid courseId);
    }
}
