using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortalKFU.Domain.Abstract;
using Moq;
using System.Collections.Generic;
using PortalKFU.Domain.Entities;
using PortalKFU.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using PortalKFU.WebUI.HtmlHelpers;
using PortalKFU.WebUI.Models;

namespace PortalKFU.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new List<Event>
    {
        new Event { EventId = 1, Name = "Статья1"},
        new Event { EventId = 2, Name = "Статья2"},
        new Event { EventId = 3, Name = "Статья3"},
        new Event { EventId = 4, Name = "Статья4"},
        new Event { EventId = 5, Name = "Статья5"}
    });
            EventController controller = new EventController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            EventsListViewModel result = (EventsListViewModel)controller.List(null, 2).Model;

            // Утверждение
            List<Event> events = result.Events.ToList();
            Assert.IsTrue(events.Count == 2);
            Assert.AreEqual(events[0].Name, "Статья4");
            Assert.AreEqual(events[1].Name, "Статья5");
        }
        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new List<Event>
    {
        new Event { EventId = 1, Name = "Статья1"},
        new Event { EventId = 2, Name = "Статья2"},
        new Event { EventId = 3, Name = "Статья3"},
        new Event { EventId = 4, Name = "Статья4"},
        new Event { EventId = 5, Name = "Статья5"}
    });
            EventController controller = new EventController(mock.Object);
            controller.pageSize = 3;
           

            // Act
            EventsListViewModel result
        = (EventsListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}
