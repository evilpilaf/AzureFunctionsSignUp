using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

using Serilog;

using SignUp.core;
using SignUp.core.Exceptions;
using SignUp.core.ValueObjects;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SignupApi
{
    public class SignUpFunction
    {
        private readonly ILogger _logger = Log.Logger;
        private readonly SignUpToCourseV1UseCase _signUpToCourseV1;
        private readonly GetAllCoursesUseCase _getAllCoursesUseCase;

        public SignUpFunction(SignUpToCourseV1UseCase signUpToCourseV1,
                              GetAllCoursesUseCase getAllCoursesUseCase)
        {
            _signUpToCourseV1 = signUpToCourseV1;
            _getAllCoursesUseCase = getAllCoursesUseCase;
        }

        [FunctionName("SignUpFunctionV1")]
        public async Task<IActionResult> SignUp(
            [HttpTrigger(AuthorizationLevel.Function,
                         "post",
                         Route = "api/courses/{courseId}/students/{studentId:guid}")]
            HttpRequest _,
            int courseId,
            string studentId)
        {
            if (!Guid.TryParse(studentId, out var studentGuid))
            {
                _logger.Warning("Received invalid student id {studentId}", studentId);
                return new BadRequestResult();
            }

            try
            {
                var result = await _signUpToCourseV1.Execute(studentGuid, courseId);

                if (result.IsSuccess)
                {
                    return new OkResult();
                }
                else
                {
                    return new InternalServerErrorResult();
                }
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case CourseNotFoundException _:
                        return new NotFoundResult();
                    default:
                        _logger.Error(ex,
                            "Exception when trying to register a new student {StudentId} to the course {CourseId}", 1,
                            courseId);
                        return new InternalServerErrorResult();
                }
            }
        }

        [FunctionName("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses(
            [HttpTrigger(AuthorizationLevel.Function,
                         "get",
                         Route = "api/courses/")]
            HttpRequest _)
        {
            try
            {
                Result<IEnumerable<Course>> result = await _getAllCoursesUseCase.Execute();
                if (result.IsSuccess)
                {
                    IEnumerable<Course> courses = result.Content;
                    return new JsonResult(courses);
                }
                else
                {
                    return new InternalServerErrorResult();
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to retrieve the available courses.");
                return new InternalServerErrorResult();
            }
        }
    }
}