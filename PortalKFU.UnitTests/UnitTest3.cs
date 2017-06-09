using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PortalKFU.Domain.Abstract;
using PortalKFU.Domain.Entities;
using System.Collections.Generic;
using PortalKFU.WebUI.Controllers;
using PortalKFU.WebUI.Models;
using System.Linq;

namespace PortalKFU.UnitTests
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void Can_Filter_Events()
        {
            // Организация (arrange)
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new List<Event>
    {
        new Event { EventId = 1, Name = "Статья1", Category="Cat1"},
        new Event { EventId = 2, Name = "Статья2", Category="Cat2"},
        new Event { EventId = 3, Name = "Статья3", Category="Cat1"},
        new Event { EventId = 4, Name = "Статья4", Category="Cat2"},
        new Event { EventId = 5, Name = "Статья5", Category="Cat3"}
    });
            EventController controller = new EventController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<Event> result = ((EventsListViewModel)controller.List("Cat2", 1).Model)
                .Events.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Статья2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "Статья4" && result[1].Category == "Cat2");
        }
    }
}
