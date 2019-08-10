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
    }
}
