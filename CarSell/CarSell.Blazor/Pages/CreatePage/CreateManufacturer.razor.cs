using CarSell.Blazor.HTTPRepo;
using CarSell.Blazor.Shared;
using CarSell.DAL.DBO;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSell.Blazor.Pages.CreatePage
{
    public partial class CreateManufacturer
    {
        private readonly Manufacturer _manufacturer = new Manufacturer();
        private SuccessfulNotification _notification;
        [Inject]
        public ICarsHttpRepo<Manufacturer> ManRepository { get; set; }

        private async Task Create()
        {
            await ManRepository.CreateAsync(_manufacturer);
            _notification.Show();
        }
    }
}
