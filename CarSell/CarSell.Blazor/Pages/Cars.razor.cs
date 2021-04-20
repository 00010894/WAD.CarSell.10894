using CarSell.Blazor.HTTPRepo;
using CarSell.DAL.DBO;
using CarSell.DAL.Parameteres;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSell.Blazor.Pages
{
    public partial class Cars
    {
        public List<Car> CarList { get; set; } = new List<Car>();
        public  PageParam PageParameters { get; set; } = new PageParam();
        private ModelParameters _carParameters = new ModelParameters();

        [Inject]
        public ICarsHttpRepo<Car> CarRepository{ get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetAsync();
        }

        private async Task SelectedPage(int page)
        {
            _carParameters.PageNumber = page;
            await GetAsync();
        }
        private async Task GetAsync()
        {
            var pagingResponse = await CarRepository.GetAsync(_carParameters);
            CarList = pagingResponse.Items;
            PageParameters = pagingResponse.PageParameters;
        }

        private async Task SearchChanged(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _carParameters.PageNumber = 1;
            _carParameters.SearchTerm = searchTerm;
            await GetAsync();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            _carParameters.OrderBy = orderBy;
            await GetAsync();
        }

        private async Task DeleteCar(int id)
        {
            await CarRepository.DeleteAsync(id);
            _carParameters.PageNumber = 1;
            await GetAsync();
        }
    }
}
