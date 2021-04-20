using CarSell.Blazor.Pagination;
using CarSell.DAL.DBO;
using CarSell.DAL.Parameteres;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarSell.Blazor.HTTPRepo
{
    public class CarHttpRepo : ICarsHttpRepo<Car>
    {
        private readonly HttpClient _client;


        public CarHttpRepo(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateAsync(Car entity)
        {
            var content = JsonSerializer.Serialize(entity);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync("https://localhost:5011/api/cars", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var url = Path.Combine("https://localhost:5011/api/cars", id.ToString());

            var deleteResult = await _client.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();
            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }

        public async Task<PageResponse<Car>> GetAsync(ModelParameters modelParam)
        {
            var queryParam = new Dictionary<string, string>
            {
                ["pageNumber"] = modelParam.PageNumber.ToString(),
                ["searchTerm"] = modelParam.SearchTerm ?? "",
                ["orderBy"] = modelParam.OrderBy
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString("https://localhost:5011/api/cars", queryParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PageResponse<Car>
            {
                Items = JsonSerializer.Deserialize<List<Car>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                PageParameters = JsonSerializer.Deserialize<PageParam>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<Car> GetById(string id)
        {
            var url = Path.Combine("https://localhost:5011/api/cars/", id);

            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var car = JsonSerializer.Deserialize<Car>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return car;
        }

        public async Task UpdateAsync(Car entity)
        {
            var content = JsonSerializer.Serialize(entity);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("https://localhost:5011/api/cars/", entity.Id.ToString());

            var putResult = await _client.PutAsync(url, bodyContent);
            var putContent = await putResult.Content.ReadAsStringAsync();

            if (!putResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }
        }
    }
}
