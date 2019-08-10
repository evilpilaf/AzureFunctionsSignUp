using Microsoft.Azure.Cosmos;

using Persistence.Adapter.Dtos;
using Persistence.Adapter.Mappers;

using Serilog;

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
            var container = GetContainer();

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

        public async Task<Result<Course>> GetCourse(Guid courseId)
        {
            var container = GetContainer();
            var query = $"SELECT * FROM c WHERE c.id = \'{courseId}\'";

            var queryDefinition = new QueryDefinition(query);

            try
            {
                _logger.Information("Attempting to retrieve a course.");
                FeedIterator<CourseCosmosDto> feedIterator =
                    container.GetItemQueryIterator<CourseCosmosDto>(queryDefinition,
                        requestOptions: new QueryRequestOptions {MaxItemCount = 1});

                var results = new List<CourseCosmosDto>();

                var resultSet = await feedIterator.ReadNextAsync();
                results.AddRange(resultSet.Resource);

                if (results.Count != 1)
                    return new CourseNotFoundException(courseId);
                else
                    return results.Single().ToEntity();
            }
            catch (Exception ex)
            {
                return new PersistenceException("", ex);
            }
        }

        private Container GetContainer()
            => _cosmosClient.GetContainer("CoursesChama", "Courses");
    }
}
