using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PortalKFU.Domain.Abstract;
using PortalKFU.Domain.Entities;
using PortalKFU.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace PortalKFU.UnitTests
{
    [TestClass]
    public class UnitTest10
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Организация - создание объекта Event с данными изображения
            Event ev = new Event
            {
                EventId = 2,
                Name = "Статья2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            // Организация - создание имитированного хранилища
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new List<Event> {
                new Event {EventId = 1, Name = "Статья1"},
                ev,
                new Event {EventId = 3, Name = "Статья3"}
            }.AsQueryable());

            // Организация - создание контроллера
            EventController controller = new EventController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(2);

            // Утверждение
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(ev.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Организация - создание имитированного хранилища
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new List<Event> {
                new Event {EventId = 1, Name = "Статья1"},
                new Event {EventId = 2, Name = "Статья2"}
            }.AsQueryable());

            // Организация - создание контроллера
            EventController controller = new EventController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(10);

            // Утверждение
            Assert.IsNull(result);
        }
    }
}
