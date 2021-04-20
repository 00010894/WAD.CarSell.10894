using CarSell.DAL.DBO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarSell.Blazor.Components.ManufacturersTable
{
    public partial class ManufacturerTable
    {
        [Parameter]
        public List<Manufacturer> Manufacturers { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updateManufacturer/", id.ToString());
            NavigationManager.NavigateTo(url);
        }
        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        //JS invoke 
        [Inject]
        public IJSRuntime Js { get; set; }

        private async Task Delete(int id)
        {
            var manufacturer = Manufacturers.FirstOrDefault(p => p.Id.Equals(id));

            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete: {manufacturer.Name}?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
            }
        }
    }
}
