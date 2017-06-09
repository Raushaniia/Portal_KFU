using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortalKFU.WebUI.Models;
using PortalKFU.WebUI.Controllers;
using PortalKFU.Domain.Entities;
using PortalKFU.Domain.Abstract;
using Moq;
using System.Collections.Generic;

namespace PortalKFU.UnitTests
{
    [TestClass]
    public class UnitTest6
    {
        [TestMethod]
        public void Generate_Category_Specific_Event_Count()
        {
            /// Организация (arrange)
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

            // Действие - тестирование счетчиков статей для различных категорий
            int res1 = ((EventsListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((EventsListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((EventsListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((EventsListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Утверждение
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
