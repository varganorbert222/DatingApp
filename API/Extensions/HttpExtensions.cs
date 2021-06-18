using System.Text.Json;
using API.Helpers;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);

            // Make header camelCase instead TitleCase for client app
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Add custom header
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));

            // Add CORS header to pagination header to make available
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}