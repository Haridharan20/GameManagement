using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using GameInventoryManagement.Controllers;
using GameInventoryManagement.Services;
using GameInventoryManagement.Models;

namespace GameInventoryManagementTest.Services
{
    public class WeaponServiceTest
    {
        private readonly WeaponController _controller;
        Mock<WeaponService> servicemock = new Mock<WeaponService>();

        public WeaponServiceTest()
        {
            _controller = new WeaponController(servicemock.Object);
        }

        [Fact]
        public async Task GetWeaponById_Return_Success()
        {
            var wid = 2;
            var ExpectedWeapon = new Weapon
            {
                Id = wid,
                Name = "M4",
                Price = 1350
            };
            servicemock.Setup(x => x.GetWeapon(wid)).ReturnsAsync(ExpectedWeapon);

            var ActualWeapon = await _controller.Get(wid);
            var result = ActualWeapon as OkObjectResult;

            Assert.IsType<OkObjectResult>(ActualWeapon);

        }
        [Fact]
        public async Task DeleteWeapon_Return_Success()
        {
            var wid = 5;
            servicemock.Setup(x => x.DeleteWeapon(wid)).ReturnsAsync(new { message = "Weapon Deleted Successfully" });
            var ActualWeapon = await _controller.Delete(wid);
            var result = ActualWeapon as OkObjectResult;

            Assert.IsType<OkObjectResult>(ActualWeapon);

        }
    }
}
