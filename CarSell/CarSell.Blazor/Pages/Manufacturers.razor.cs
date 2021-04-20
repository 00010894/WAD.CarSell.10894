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
    public partial class Manufacturers
    {
        public List<Manufacturer> manufacturerList { get; set; } = new List<Manufacturer>();
        public PageParam PageParameters { get; set; } = new PageParam();
        private ModelParameters _manParameters = new ModelParameters();


        [Inject]
        public ICarsHttpRepo<Manufacturer> ManRepository { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetAsync();

        }

        private async Task SelectedPage(int page)
        {
            _manParameters.PageNumber = page;
            await GetAsync();
        }
        private async Task GetAsync()
        {
            var pagingResponse = await ManRepository.GetAsync(_manParameters);
            manufacturerList = pagingResponse.Items;
            PageParameters = pagingResponse.PageParameters;
        }

        private async Task DeleteManufacturer(int id)
        {
            await ManRepository.DeleteAsync(id);
            _manParameters.PageNumber = 1;
            await GetAsync();
        }
    }
}
