using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortalKFU.Domain.Abstract;
using Moq;
using PortalKFU.Domain.Entities;
using System.Collections.Generic;
using PortalKFU.WebUI.Controllers;
using System.Linq;

namespace PortalKFU.UnitTests
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Организация - создание имитированного хранилища
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new List<Event> {
        new Event { EventId = 1, Name = "Статья1", Category="Математика"},
        new Event { EventId = 2, Name = "Статья2", Category="Матемитика"},
        new Event { EventId = 3, Name = "Статья3", Category="Физика"},
        new Event { EventId = 4, Name = "Статья4", Category="Философия"},
    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Действие - получение набора категорий
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 4);
            Assert.AreEqual(results[0], "Математика");
            Assert.AreEqual(results[2], "Физика");
            Assert.AreEqual(results[3], "Философия");
        }
    }
}
