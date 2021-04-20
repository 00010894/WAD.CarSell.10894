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
    public partial class CreateCar
    {
        private readonly Car _car = new Car();
        
        private SuccessfulNotification _notification;

        [Inject]
        public ICarsHttpRepo<Car> CarRepository { get; set; }
        
        private async Task Create()
        {
            await CarRepository.CreateAsync(_car);
            _notification.Show();
        }
    }
}
