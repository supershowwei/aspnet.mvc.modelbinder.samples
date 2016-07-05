using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMvcModelBinderSamples.ModelBinders;
using AspNetMvcModelBinderSamples.Models;

namespace AspNetMvcModelBinderSamples.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test([ModelBinder(typeof(CustomerModelBinder))]Customer customer)
        {
            return View("Index");
        }
    }
}