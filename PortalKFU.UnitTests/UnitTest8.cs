using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortalKFU.Domain.Abstract;
using Moq;
using System.Collections.Generic;
using PortalKFU.Domain.Entities;
using PortalKFU.WebUI.Controllers;
using System.Linq;

namespace PortalKFU.UnitTests
{
    [TestClass]
    public class UnitTest8
    {
        //Корректный возврат в админской части
        [TestMethod]
        public void Index_Contains_All_Events()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new List<Event>
            {
                new Event { EventId = 1, Name = "Статья1"},
                new Event { EventId = 2, Name = "Статья2"},
                new Event { EventId = 3, Name = "Статья3"},
                new Event { EventId = 4, Name = "Статья4"},
                new Event { EventId = 5, Name = "Статья5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<Event> result = ((IEnumerable<Event>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Статья1", result[0].Name);
            Assert.AreEqual("Статья2", result[1].Name);
            Assert.AreEqual("Статья3", result[2].Name);
        }
    }
}
    

