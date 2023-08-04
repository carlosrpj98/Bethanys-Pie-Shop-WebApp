using BethanysPieShopWebApp.Models;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopWebApp.Pages.App
{
    public partial class PieCard
    {
        [Parameter]
        public Pie? Pie { get; set; }
    }
}
