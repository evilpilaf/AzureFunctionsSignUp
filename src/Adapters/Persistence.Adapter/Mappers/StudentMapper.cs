using Persistence.Adapter.Dtos;

using SignUp.core;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Adapter.Mappers
{
    internal static class StudentMapper
    {
        public static Course ToEntity(this CourseCosmosDto instance) =>
            new Course(id: instance.Id,
                       name: instance.Name,
                       capacity: instance.Capacity,
                       registeredUsers: new HashSet<Guid>(instance.RegisteredStudents.Select(s => s.Id)));
        public static CourseCosmosDto FromEntity(this Course instance) =>
            new CourseCosmosDto
            {
                Id = instance.Id,
                Name = instance.Name,
                Capacity = instance.Capacity,
                RegisteredStudents = instance.RegisteredStudents.Select(s => new StudentCosmosDto
                {
                    Id = s
                }).ToArray()
            };
    }
}
