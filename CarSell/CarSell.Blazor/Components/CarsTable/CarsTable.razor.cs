using CarSell.DAL.DBO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarSell.Blazor.Components.CarsTable
{
    public partial class CarsTable
    {
        [Parameter]
        public List<Car> Cars { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/UpdateCar/", id.ToString());
            NavigationManager.NavigateTo(url);
        }


        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        //JS invoke 
        [Inject]
        public IJSRuntime Js { get; set; }

        private async Task Delete(int id)
        {
            var car = Cars.FirstOrDefault(p => p.Id.Equals(id));

            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete car with id: {car.Id}?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
            }
        }
    }
}
