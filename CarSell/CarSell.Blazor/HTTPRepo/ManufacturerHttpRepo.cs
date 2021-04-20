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
    public class ManufacturerHttpRepo : ICarsHttpRepo<Manufacturer>
    {
        private readonly HttpClient _client;


        public ManufacturerHttpRepo(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateAsync(Manufacturer entity)
        {
            var content = JsonSerializer.Serialize(entity);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync("https://localhost:5011/api/manufacturers", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public  async Task DeleteAsync(int id)
        {
            var url = Path.Combine("https://localhost:5011/api/manufacturers", id.ToString());

            var deleteResult = await _client.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();
            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }

        public async Task<PageResponse<Manufacturer>> GetAsync(ModelParameters modelParam)
        {
            var queryParam = new Dictionary<string, string>
            {
                ["pageNumber"] = modelParam.PageNumber.ToString()
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString("https://localhost:5011/api/manufacturers", queryParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pageResponse = new PageResponse<Manufacturer>
            {
                Items = JsonSerializer.Deserialize<List<Manufacturer>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                PageParameters = JsonSerializer.Deserialize<PageParam>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pageResponse;
        }

        public async Task<Manufacturer> GetById(string id)
        {
            var url = Path.Combine("https://localhost:5011/api/manufacturers/", id);

            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var manufacturer = JsonSerializer.Deserialize<Manufacturer>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return manufacturer;
        }

        public async Task UpdateAsync(Manufacturer entity)
        {
            var content = JsonSerializer.Serialize(entity);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("https://localhost:5011/api/manufacturers/", entity.Id.ToString());

            var putResult = await _client.PutAsync(url, bodyContent);
            var putContent = await putResult.Content.ReadAsStringAsync();

            if (!putResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }
        }
    }
}
