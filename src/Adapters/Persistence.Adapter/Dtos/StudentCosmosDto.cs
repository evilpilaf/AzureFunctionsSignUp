using System;

namespace Persistence.Adapter.Dtos
{
    internal sealed class StudentCosmosDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
