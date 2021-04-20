using CarSell.Blazor.HTTPRepo;
using CarSell.Blazor.Shared;
using CarSell.DAL.DBO;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSell.Blazor.Pages.UpdatePage
{
    public partial class UpdateManufacturer
    {
        private Manufacturer _manufacturer;
        private SuccessfulNotification _notification;
        [Inject]
        ICarsHttpRepo<Manufacturer> ManufacturerRepository { get; set; }
        [Parameter]
        public string Id { get; set; }
        protected async override Task OnInitializedAsync()
        {
            _manufacturer = await ManufacturerRepository.GetById(Id);
        }
        private async Task Update()
        {
            await ManufacturerRepository.UpdateAsync(_manufacturer);
            _notification.Show();
        }
    }
}
