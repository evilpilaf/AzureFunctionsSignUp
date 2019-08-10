using SignUp.core;
using SignUp.core.Exceptions;
using SignUp.core.Ports;
using SignUp.core.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Adapter
{
    internal sealed class CourseLoaderInMemory : ICourseLoader
    {
        private readonly Dictionary<int, Course> _courses;

        public CourseLoaderInMemory()
        {
            _courses = new Dictionary<int, Course>()
            {
                { 1, new Course(1, "MyCourse", 10, new HashSet<Guid>()) }
            };
        }

        public Task<Result<IEnumerable<Course>>> GetAllCourses()
        {
            Result<IEnumerable<Course>> coursesResult = _courses.Values.ToList();
            return Task.FromResult(coursesResult);
        }

        public Task<Result<Course>> GetCourse(int courseId)
        {
            Result<Course> courseResult;
            if (_courses.ContainsKey(courseId))
            {
                courseResult = _courses[courseId];
                return Task.FromResult(courseResult);
            }
            courseResult = new CourseNotFoundException(courseId);
            return Task.FromResult(courseResult);
        }
    }
}
