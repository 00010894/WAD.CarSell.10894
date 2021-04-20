using CarSell.Blazor.Pagination;
using CarSell.DAL.Parameteres;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSell.Blazor.Components
{
    public partial class Pagination
    {
        [Parameter]
        public PageParam PageParam { get; set; }
        [Parameter]
        public int Spread { get; set; }
        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }

        private List<PageStatus> _pages;

        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }
        private void CreatePaginationLinks()
        {
            _pages = new List<PageStatus>();
            _pages.Add(new PageStatus(PageParam.CurrentPage - 1, PageParam.HasPrevious, "Previous"));
            for (int i = 1; i <= PageParam.TotalPages; i++)
            {
                if (i >= PageParam.CurrentPage - Spread && i <= PageParam.CurrentPage + Spread)
                {
                    _pages.Add(new PageStatus(i, true, i.ToString()) { Active = PageParam.CurrentPage == i });
                }
            }
            _pages.Add(new PageStatus(PageParam.CurrentPage + 1, PageParam.HasNext, "Next"));
        }
        private async Task OnSelectedPage(PageStatus link)
        {
            if (link.Page == PageParam.CurrentPage || !link.Enabled)
                return;
            PageParam.CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }
    }
}
