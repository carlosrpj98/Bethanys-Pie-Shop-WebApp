using BethanysPieShopTests.Mocks;
using BethanysPieShopWebApp.Controllers;
using BethanysPieShopWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShopTests.Controllers
{
    public class PieControllerTests
    {
        public void List_EmptyCategory_ReturnsAllPies()
        {
            //arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var pieController = new PieController(mockPieRepository.Object, mockCategoryRepository.Object);

            //act
            var result = pieController.List("");

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(10, pieListViewModel.Pies.Count());
        }
    }
}
