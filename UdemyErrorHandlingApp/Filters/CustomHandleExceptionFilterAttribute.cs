using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace UdemyErrorHandlingApp.Filters;

// Filter lar uygulama da class bazlı veya metod bazlı kontroller yapabilmememizi saplayan yapılardır.
// Uygulama da kullanabilmek için classa veya metoda attribute olarak eklememiz gerekir.
// ExceptionFilterAttribute tanımlanan class da veya metod da hata oluştuğu zaman yakalamk için kullanılır.
// Bu hatayı OnException metodu yakalar.
// Yazılımcılar mümkün oldukça hata yakalamak için middleware kullanılmasını tavsiye eder.
// Spesifik bir hata yakalama için filterlar kullanılır.
public class CustomHandleExceptionFilterAttribute : ExceptionFilterAttribute
{
    public string ErrorPage { get; set; }
    public override void OnException(ExceptionContext context)
    {
        var result = new ViewResult() {ViewName = ErrorPage};
        result.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState);
        result.ViewData.Add("Exception", context.Exception);
        result.ViewData.Add("Url",context.HttpContext.Request.Path.Value);
        context.Result = result;
    }
}