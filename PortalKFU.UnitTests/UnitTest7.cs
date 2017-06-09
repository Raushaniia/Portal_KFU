using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortalKFU.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using PortalKFU.WebUI.Controllers;
using PortalKFU.WebUI.Models;
using PortalKFU.Domain.Abstract;
using Moq;
using System.Web.Mvc;

namespace PortalKFU.UnitTests
{
    [TestClass]
    public class UnitTest7
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Организация - создание нескольких тестовых статей
            Event event1 = new Event { EventId = 1, Name = "Статья1" };
            Event event2 = new Event { EventId = 2, Name = "Статья2" };

            // Организация - создание архива избранных статей
            Library library = new Library();

            // Действие
            library.AddItem(event1, 1);
            library.AddItem(event2, 1);
            List<LibraryLine> results = library.Lines.ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Event, event1);
            Assert.AreEqual(results[1].Event, event2);
        }
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Организация - создание нескольких тестовых статей
            Event event1 = new Event { EventId = 1, Name = "Статья1" };
            Event event2 = new Event { EventId = 2, Name = "Статья2" };

            // Организация - создание архива избранных статей
            Library library = new Library();

            // Действие
            library.AddItem(event1, 1);
            library.AddItem(event2, 1);
            library.AddItem(event1, 5);
            List<LibraryLine> results = library.Lines.OrderBy(c => c.Event.EventId).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);    // 6 экземпляров добавлено в избранное
            Assert.AreEqual(results[1].Quantity, 1);
        }
        [TestMethod]
        public void Can_Remove_Line()
        {
            // Организация - создание нескольких тестовых статей
            Event event1 = new Event { EventId = 1, Name = "Статья1" };
            Event event2 = new Event { EventId = 2, Name = "Статья2" };
            Event event3 = new Event { EventId = 3, Name = "Статья3" };

            // Организация - создание архиван избранных статей
            Library library = new Library();

            // Организация - добавление нескольких статей в избранноу
            library.AddItem(event1, 1);
            library.AddItem(event2, 4);
            library.AddItem(event3, 2);
            library.AddItem(event2, 1);

            // Действие
            library.RemoveLine(event2);

            // Утверждение
            Assert.AreEqual(library.Lines.Where(c => c.Event == event2).Count(), 0);
            Assert.AreEqual(library.Lines.Count(), 2);
        }
        // С помощью этого теста проверяем, все ли корректно удаляется
        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Организация - создание нескольких тестовых статей
            Event event1 = new Event { EventId = 1, Name = "Статья1", };
            Event event2 = new Event { EventId = 2, Name = "Статья2", };

            // Организация - создание архива избранных статей
            Library library = new Library();

            // Действие
            library.AddItem(event1, 1);
            library.AddItem(event2, 1);
            library.AddItem(event1, 5);
            library.Clear();

            // Утверждение
            Assert.AreEqual(library.Lines.Count(), 0);
        }

        // Проверяем URL
        [TestMethod]
        public void Can_View_Library_Contents()
        {
            // Организация - создание архива избранных статей
            Library library = new Library();

            // Организация - создание контроллера
            LibraryController target = new LibraryController(null);

            // Действие - вызов метода действия Index()
            LibraryIndexViewModel result
                = (LibraryIndexViewModel)target.Index(library, "myUrl").ViewData.Model;

            // Утверждение
            Assert.AreSame(result.Library, library);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

    }
}
