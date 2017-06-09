using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortalKFU.Domain.Abstract;
using Moq;
using PortalKFU.Domain.Entities;
using System.Collections.Generic;
using PortalKFU.WebUI.Controllers;
using System.Web.Mvc;

namespace PortalKFU.UnitTests
{
    [TestClass]
    public class UnitTest9
    {
        //Проверка запрашеваемой статьи
        [TestMethod]
        public void Can_Edit_Event()
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
            Event event1 = controller.Edit(1).ViewData.Model as Event;
            Event event2 = controller.Edit(2).ViewData.Model as Event;
            Event event3 = controller.Edit(3).ViewData.Model as Event;

            // Assert
            Assert.AreEqual(1, event1.EventId);
            Assert.AreEqual(2, event2.EventId);
            Assert.AreEqual(3, event3.EventId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Event()
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
            Event result = controller.Edit(6).ViewData.Model as Event;

            // Assert
        }
        //Удаление статей
        [TestMethod]
        public void Can_Delete_Valid_Events()
        {
            // Организация - создание объекта Event
            Event ev = new Event { EventId = 2, Name = "Статья2" };

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

            // Действие - удаление игры
            controller.Delete(ev.EventId);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта Event
            mock.Verify(m => m.DeleteEvent(ev.EventId));
        }
    }
}
