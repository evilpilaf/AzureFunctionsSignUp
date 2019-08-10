using SignUp.core.ValueObjects;

using System;
using System.Collections.Generic;

namespace SignUp.core
{
    public sealed class Course
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Capacity { get; }
        public HashSet<Guid> RegisteredStudents;

        public Course(Guid id, string name, int capacity, HashSet<Guid> registeredUsers)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            RegisteredStudents = registeredUsers;
        }

        public Result RegisterStudent(Guid studentId)
        {
            if (RegisteredStudents.Count < Capacity)
            {
                if (RegisteredStudents.Contains(studentId))
                {
                    return Result.Success();
                }
                else
                {
                    RegisteredStudents.Add(studentId);
                    return Result.Success();
                }
            }
            return new Exception("The course is fully booked");
        }
    }
}
