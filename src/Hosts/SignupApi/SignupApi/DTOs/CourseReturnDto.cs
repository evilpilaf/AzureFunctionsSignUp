﻿using SignUp.core;

using System;

namespace SignupApi.DTOs
{
    public readonly struct CourseReturnDto
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Capacity { get; }
        public int RegisteredUsers { get; }
        public bool IsAvailable => Capacity > RegisteredUsers;

        public CourseReturnDto(Course course)
        {
            Id = course.Id;
            Name = course.Name;
            Capacity = course.Capacity;
            RegisteredUsers = course.RegisteredStudents.Count;
        }
    }
}
