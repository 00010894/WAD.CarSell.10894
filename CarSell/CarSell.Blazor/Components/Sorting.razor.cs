using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSell.Blazor.Components
{
    public partial class Sorting
    {
        [Parameter]
        public EventCallback<string> OnSortChanged { get; set; }

        private async Task ApplySort(ChangeEventArgs eventArgs)
        {
            if (eventArgs.Value.ToString() == "-1")
                return;

            await OnSortChanged.InvokeAsync(eventArgs.Value.ToString());
        }
    }
}
