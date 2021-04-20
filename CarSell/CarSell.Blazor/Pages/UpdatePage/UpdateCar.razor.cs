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
    public partial class UpdateCar
    {
        private Car _car;
        private SuccessfulNotification _notification;
        [Inject]
        ICarsHttpRepo<Car> CarRepository { get; set; }
        [Parameter]
        public string Id { get; set; }
        protected async override Task OnInitializedAsync()
        {
            _car = await CarRepository.GetById(Id);
        }
        private async Task Update()
        {
            await CarRepository.UpdateAsync(_car);
            _notification.Show();
        }
    }
}
