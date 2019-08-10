using Newtonsoft.Json;

using System;

namespace Persistence.Adapter.Dtos
{
    internal class CourseCosmosDto
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public StudentCosmosDto[] RegisteredStudents { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
