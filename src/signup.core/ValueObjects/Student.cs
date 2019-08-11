using System;

namespace SignUp.core.ValueObjects
{
    public readonly struct Student
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }

        public Student(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
