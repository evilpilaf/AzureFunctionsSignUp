using Microsoft.Azure.Cosmos;

using Persistence.Adapter.Dtos;
using Persistence.Adapter.Mappers;

using Serilog;

using SignUp.core;
using SignUp.core.Ports;
using SignUp.core.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignUp.core.Exceptions;

namespace Persistence.Adapter
{
    internal sealed class CourseLoaderCosmosDb : ICourseLoader
    {
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger _logger = Log.Logger;

        public CourseLoaderCosmosDb(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<Result<IEnumerable<Course>>> GetAllCourses()
        {
            const string query = "SELECT * FROM c";
            var container = _cosmosClient.GetContainer("CoursesChama", "Courses");

            var queryDefinition = new QueryDefinition(query);

            try
            {
                _logger.Information("Attempting to retrieve all courses.");
                FeedIterator<CourseCosmosDto> feedIterator =
                    container.GetItemQueryIterator<CourseCosmosDto>(queryDefinition);

                var results = new List<CourseCosmosDto>();
                while (feedIterator.HasMoreResults)
                {
                    var currentResultSet = await feedIterator.ReadNextAsync();
                    results.AddRange(currentResultSet.Resource);
                }

                return Result<IEnumerable<Course>>.Success(results.Select(r => r.ToEntity()));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception when retrieving courses");
                return new PersistenceException("Exception when retrieving courses", ex);
            }
        }

        public Task<Result<Course>> GetCourse(Guid courseId)
        {
            throw new NotImplementedException();
        }
    }
}
