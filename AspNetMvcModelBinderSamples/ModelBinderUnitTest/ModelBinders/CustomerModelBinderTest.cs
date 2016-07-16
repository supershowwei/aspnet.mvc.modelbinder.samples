using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AspNetMvcModelBinderSamples.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;

namespace AspNetMvcModelBinderSamples.ModelBinders.Tests
{
    [TestClass()]
    public class CustomerModelBinderTest
    {
        [TestMethod]
        [TestCategory("CustomerModelBinder")]
        public void Test_BindModel_input_serialized_Customer_expect_can_binding_success()
        {
            // Arrange
            var controllerContext = Substitute.For<ControllerContext>();
            var bindingContext =
                new ModelBindingContext()
                {
                    ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Customer))
                };

            controllerContext.HttpContext
                             .Request
                             .InputStream
                             .Returns(new MemoryStream(Encoding.UTF8.GetBytes(DummySerializedCustomer)));

            var modelBinder = new CustomerModelBinder();

            // Act
            var actual = modelBinder.BindModel(controllerContext, bindingContext) as Customer;

            // Assert
            actual.Id.Should().Be(1);
            actual.Type.Should().Be(CustomerType.Taiwan);
            actual.Name.Should().Be("Johnny");
            actual.Orders.OfType<BookOrder>().First().Id.Should().Be(2);
            actual.Orders.OfType<BookOrder>().First().Name.Should().Be("哈利波特");
        }

        private static string DummySerializedCustomer
        {
            get
            {
                return
                    JsonConvert.SerializeObject(
                        new TaiwanCustomer()
                        {
                            Id = 1,
                            Type = CustomerType.Taiwan,
                            Name = "Johnny",
                            Tel = "02-12345678",
                            Orders =
                                new List<Order>()
                                {
                                    new BookOrder() { Id = 2, Type = OrderType.Book, Name = "哈利波特" }
                                }
                        });
            }
        }
    }
}