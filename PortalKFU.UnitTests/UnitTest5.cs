using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortalKFU.Domain.Abstract;
using PortalKFU.Domain.Entities;
using Moq;
using PortalKFU.WebUI.Controllers;

namespace PortalKFU.UnitTests
{
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Организация - создание имитированного хранилища
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new Event[] {
        new Event { EventId = 1, Name = "Статья1", Category="Математика"},
        new Event { EventId = 2, Name = "Статья2", Category="Физика"}
    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Организация - определение выбранной категории
            string categoryToSelect = "Физика";

            // Действие
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Утверждение
            Assert.AreEqual(categoryToSelect, result);
        }
    }
}
