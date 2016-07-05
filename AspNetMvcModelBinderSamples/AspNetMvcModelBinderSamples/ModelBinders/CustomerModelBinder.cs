using System.IO;
using System.Text;
using System.Web.Mvc;
using AspNetMvcModelBinderSamples.JsonConverters;
using Newtonsoft.Json;

namespace AspNetMvcModelBinderSamples.ModelBinders
{
    public class CustomerModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            controllerContext.HttpContext.Request.InputStream.Seek(0, SeekOrigin.Begin);

            var stream = new StreamReader(controllerContext.RequestContext.HttpContext.Request.InputStream, Encoding.UTF8);

            var json = stream.ReadToEnd();

            return JsonConvert.DeserializeObject(json, bindingContext.ModelType, new CustomerConverter(), new OrderConverter());
        }
    }
}