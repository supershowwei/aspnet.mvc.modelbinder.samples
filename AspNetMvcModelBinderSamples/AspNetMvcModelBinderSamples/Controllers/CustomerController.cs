using System.Collections.Generic;
using System.Web.Mvc;
using AspNetMvcModelBinderSamples.ModelBinders;
using AspNetMvcModelBinderSamples.Models;
using Newtonsoft.Json;

namespace AspNetMvcModelBinderSamples.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            var str =
                JsonConvert.SerializeObject(
                    new TaiwanCustomer()
                    {
                        Id = 1,
                        Type = CustomerType.Taiwan,
                        Name = "Johnny",
                        Tel = "0800-092-000",
                        Orders = new List<Order>()
                        {
                            new BookOrder()
                            {
                                Id = 1,
                                Type = OrderType.Book,
                                Name = "哈利波特"
                            }
                        }
                    });

            return View();
        }

        public ActionResult Add([ModelBinder(typeof(CustomerModelBinder))]Customer customer)
        {
            return Json(customer);
        }
    }
}