using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;

namespace AspNetCoreActionResultsDemo.Controllers
{
    public class HomeController : Controller
    {
        #region View Results
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IndexWithName()
        {
            //Views/Home/AboutUs.cshtml View’ına yönlendirir. 
            return View("AboutUs");
        }

        public IActionResult PartialViewResult()
        {
            return PartialView();
        }

        public IActionResult ViewComponentResult()
        {
            return ViewComponent("ViewComponentExample");
        }

        public IActionResult JsonResult()
        {
            return Json(new { message = "Json içeriği.", date = DateTime.Now });
        }

        public IActionResult ContentResult()
        {
            return Content("ContentResult içeriği.", "text/plain");
        }

        public IActionResult EmptyResult()
        {
            return EmptyResult();
        }


        #endregion

        #region Status Code Result

        /// <summary>
        /// BadRequestResult(Kısa yöntemi: BadRequest()) HTTP 400 (Bad Request) Bu, alınan isteğin yanlış söz diziminden kaynaklanabilecek bir hata 
        /// nedeniyle sunucu tarafından işlenemediğini belirtmek için kullanılır.
        /// </summary>
        /// <returns>400 Kodu Döndürür</returns>
        public BadRequestResult BadRequestResult()
        {
            return BadRequest();
        }

        /// <summary>
        /// BadRequestResult tipinin geriye nesne döndüren halidir.
        /// </summary>
        /// <returns>Nesne ve 400 Kodu Döndürür</returns>
        public BadRequestObjectResult BadRequestObjectActionResult()
        {
            //var result = new BadRequestObjectResult(new { message = "400 Bad Request", currentDate = DateTime.Now });
            //return result;
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Name", "Name is required.");
            return BadRequest(modelState);
        }

        /// <summary>
        /// NotFoundResult(Kısa yöntemi: NotFound()) HTTP 404 (Not Found) durum kodu döndererek aranılan içeriğin bulunmadığını belirtebilirsiniz.
        /// </summary>
        /// <returns>404 Kodu Döndürür</returns>
        public NotFoundResult NotFoundActionResult()
        {
            return NotFound();
        }

        /// <summary>
        /// NotFoundResult tipinin geriye nesne döndüren halidir.
        /// </summary>
        /// <returns>Nesne ve 404 Kodu Döndürür</returns>
        public IActionResult NotFoundObjectActionResult()
        {
            var result = new NotFoundObjectResult(new { message = "404 Not Found", currentDate = DateTime.Now });
            return result;
        }

        /// <summary>
        /// NotFoundResult tipinin geriye nesne döndüren halidir.
        /// </summary>
        /// <returns>Nesne ve 404 Kodu Döndürür</returns>
        public IActionResult ObjectResult()
        {
            var result = new ObjectResult(new { currentDate = DateTime.Now });
            return result;
        }

        /// <summary>
        /// OkResult(Kısa yöntemi: Ok()) HTTP 200 (OK) durum kodu döndürür, yanıtın başarılı olduğunu gösterir. Yani, istemci ile sunucu arasındaki 
        /// iletişim herhangi bir hata olmadan sorunsuz bir şekilde gerçekleştiğini belirtebilirsiniz.
        /// </summary>
        /// <returns>200 Kodu Döndürür</returns>
        public OkResult SuccessResult()
        {
            return Ok();
        }

        /// <summary>
        /// OkResult tipinin geriye nesne döndüren halidir.
        /// </summary>
        /// <returns>Nesne ve 200 Kodu Döndürür</returns>
        public IActionResult OkObjectResult()
        {
            var result = new OkObjectResult(new { message = "200 OK", currentDate = DateTime.Now });
            return result;
        }

        /// <summary>
        /// NoContentResult(Kısa yöntemi: NoContent()) HTTP 204 (No Content) Bu, gönderilen isteğin başarılı olduğunu, ancak yanıt olarak 
        /// gönderilecek ek veri olmadığını belirtmek için kullanabilirsiniz.
        /// </summary>
        /// <returns>204 Kodu Döndürür</returns>
        public NoContentResult NoContentResult()
        {
            return NoContent();
        }

        /// <summary>
        /// Action Result’da bulunmayan durum kodları için StatusCode() tipini kulanabiliriz.
        /// </summary>
        /// <returns>Durum Kodu Döndürür</returns>
        public StatusCodeResult StatusCodeResult(int durumKodu)
        {
            return StatusCode(durumKodu);
        }

        /// <summary>
        /// Action Result’da bulunmayan durum kodları ve nesne döndürmek için StatusCode() tipini kulanabiliriz.
        /// </summary>
        /// <returns>Durum Kodu ve Nesne Döndürür</returns>
        public ObjectResult StatusCodeWithObject()
        {
            return StatusCode(404, new { Message = "404 Not found!..." });
        }

        /// <summary>
        /// CreatedResult(Kısa yöntemi: Created()) Kaynak oluşturma işleminin başarılı olduğunu ve kaynak adresini döndürür.
        /// </summary>
        /// <returns>201 Durum Kodu ve Nesne Döndürür</returns>
        public CreatedResult CreatedActionResult()
        {
            return Created(new Uri("/Home/Index", UriKind.Relative), new { Name = "Created Action Result" });
        }

        /// <summary>
        /// CreatedAtActionResult(Kısa yöntemi: CreatedAtAction()) Kaynak oluşturma işleminin başarılı olduğunu ve Controller Action ve Route Data döndürür.
        /// </summary>
        /// <returns>201 Durum Kodu ve Controller Action ve Route Data Döndürür</returns>
        public CreatedAtActionResult CreatedAtActionActionResult()
        {
            return CreatedAtAction("IndexWithId", "Home", new { id = 2, area = "" }, new { Name = "201 Created Object!.." });
        }

        /// <summary>
        /// CreatedAtRouteResult(Kısa yöntemi: CreatedAtRoute()) Kaynak oluşturma işleminin başarılı olduğunu ve Controller Action ve Route Data döndürür.
        /// </summary>
        /// <returns>201 Durum Kodu ve Route Name Döndürür</returns>
        public CreatedAtRouteResult CreatedAtRouteActionResult()
        {
            return CreatedAtRoute("default", new { Id = 2, area = "" }, new { Name = "201 Created Object!.." });
        }

        /// <summary>
        /// AcceptedResult(Kısa yöntemi: Accepted()) 202 Durum Kodu döndürür. Kaynak oluşturma talebinin kabul edildiği belirtmek için kullanılır.
        /// </summary>
        /// <returns>202 Durum Kodu, ilgili Url ve Nesne döndürür.</returns>
        public AcceptedResult AcceptedActionResult()
        {
            return Accepted(new Uri("/Home/Index", UriKind.Relative), new { Name = "Accepted Result" });
        }

        /// <summary>
        /// AcceptedAtActionResult(Kısa yöntemi: AcceptedAtAction()) 202 Durum Kodu döndürür. Kaynak oluşturma talebinin kabul edildiği belirtmek için kullanılır.
        /// </summary>
        /// <returns>202 Durum Kodu, ilgili Route Parametreleri ve Nesne döndürür.</returns>
        public AcceptedAtActionResult AcceptedAtActionActionResult()
        {
            return AcceptedAtAction("IndexWithId", "Home", new { Id = 2, area = "" }, new { Name = "Hamid" });
        }

        /// <summary>
        /// AcceptedAtActionResult(Kısa yöntemi: AcceptedAtAction()) 202 Durum Kodu döndürür. Kaynak oluşturma talebinin kabul edildiği belirtmek için kullanılır.
        /// </summary>
        /// <returns>202 Durum Kodu, ilgili Route Adını ve Parametreleri ve Nesne döndürür.</returns>
        public AcceptedAtRouteResult AcceptedAtRouteActionResult()
        {
            return AcceptedAtRoute("default", new { Id = 2, area = "" }, new { Name = "Hamid" });
        }

        /// <summary>
        /// 415 Desteklenmeyen Medya Tipi durum kodu döndürür. Örneğin sadece “.jpg” uzantılı resimleri yüklemeye izin 
        /// verdiğinizi varsayalım. Kullanıcı “.bmp” uzantılı bir resim yüklemeye çalıştığında UnsupportedMediaTypeResult() döndürebilirsiniz.
        /// </summary>
        /// <returns>415 Durum Kodu</returns>
        public IActionResult UnsupportedMediaTypeResult()
        {
            return new UnsupportedMediaTypeResult();
        }

        /// <summary>
        /// UnauthorizedResult(Kısa yöntemi: Unauthorized()) HTTP 401 (Unauthorized) durum kodu döndürerek erişmek istenilen içerik için 
        /// kullanıcının yetkisinin olmadığını belirtenbilirsiniz.
        /// </summary>
        /// <returns>401 Durum Kodu</returns>
        public UnauthorizedResult UnauthorizedResult()
        {
            return Unauthorized();
        }

        #endregion

        #region Redirect Action Result

        /// <summary>
        /// RedirectResult(Kısa yöntemi: Redirect()) Kullancıyı farklı bir sayfaya yönlendirmek istediğimizde kullanılır.
        /// </summary>
        /// <returns>HTTP 301 veya 302 durum kodu</returns>
        public RedirectResult RedirectResult()
        {
            //Return Http 301 Status Code
            //return RedirectPermanent("/");
            //Return Http 301 Status Code
            //return new RedirectResult("https://www.domain.net") {Permanent = true};
            //Return Http 302 Status Code
            return Redirect("https://www.domain.net"); // 302 Yönlendirmesi yapar
        }

        /// <summary>
        /// RedirectToActionResult (kısa yöntem RedirectToAction ():), Kullanıcıyı farklı bir Action’a yönlendirmke istediğimizde kullanırız.
        /// </summary>
        /// <returns>HTTP 301 veya 302 durum kodu</returns>
        public RedirectToActionResult RedirectActionResult()
        {
            //Return Http 301 Status Code
            //return RedirectToActionPermanent("Index");
            //Return Http 302 Status Code
            return RedirectToAction("Index"); // 302 Yönlendirmesi yapar
        }

        /// <summary>
        /// RedirectToRouteResult (kısa yöntem RedirectToRoute():), Kullanıcıyı farklı bir Route’a yönlendirmek istediğimizde kullanırız.
        /// </summary>
        /// <returns>HTTP 301 veya 302 durum kodu</returns>
        public RedirectToRouteResult RedirectToRouteResult()
        {
            //Return Http 301 Status Code
            //var routeValue = new RouteValueDictionary(new { action = "Index", controller = "Home", area = "" });
            //return RedirectToRoutePermanent(routeValue);
            //Return Http 302 Status Code
            return RedirectToRoute("BlogListRoute");
        }

        /// <summary>
        /// LocalRedirectResult (kısa yöntem LocalRedirect():), Kullanıcıyı projemizde yer alan farklı bir lokal adrese yönlendirmek istediğimizde kullanırız.
        /// </summary>
        /// <returns>HTTP 301 veya 302 durum kodu</returns>
        public LocalRedirectResult LocalRedirectResult()
        {
            //Return Http 301 Status Code
            //return new LocalRedirectResult("/aboutus") {Permanent = true};
            //Return Http 301 Status Code
            //return LocalRedirectPermanent("/aboutus");
            //Return Http 302 Status Code
            return LocalRedirect("/aboutus");
        }

        #endregion

        #region File Result

        /// <summary>
        ///  FileResult() (kısa yöntem File():), Projemizde var olan bir dosyayı döndürür. Örneğin “/wwwroot/css” klasörümüzdeki site.css 
        ///  dosyamızı için aşağıdaki gibi yapılandırmamız gerekir.
        /// </summary>
        /// <returns>File</returns>
        public IActionResult FileResult()
        {
            return File("~/css/site.css", "text/plain", "rename.css");
        }

        /// <summary>
        ///  FileStreamResult Projemizde var olan bir dosyayı FileStream döndürür. Örneğin “/wwwroot/css” klasörümüzdeki site.css 
        ///  dosyamızı için aşağıdaki gibi yapılandırmamız gerekir.
        /// </summary>
        /// <returns>File</returns>
        public FileStreamResult FileStreamActionResult()
        {
            //var file = System.IO.File.ReadAllBytes(@"D:/websites/www/httpdocs//wwwroot/site.css");
            //var stream = new MemoryStream(file, writable:true);

            var fileStream = new FileStream("D:/websites/www/httpdocs/wwwroot/site.css", FileMode.Open, FileAccess.Read);

            return File(fileStream, "text/plain", "site.css");
        }

        /// <summary>
        /// FileContentResult sınıfının FileResulttan tek farkı; dosyayı medya olarak değil, byte[] tipinde döndürür. Web servisten mobil uygulamamıza 
        /// dosya transferinde veya veritabanınımızdaki ürünler listemizi excel olarak indirmek vb. durumlarda kullanılabilir.
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult FileContentResult()
        {
            var pdfBytes = System.IO.File.ReadAllBytes("wwwroot/css/site.css");
            return new FileContentResult(pdfBytes, "text/plain");
        }

        /// <summary>
        /// VirtualFileResult sınıfı, projemizdeki  “/wwwroot” klasöründe yer alan dosyalarımız için kullanabiliriz.
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult VirtualFileResult()
        {
            return new VirtualFileResult("/css/site.css", "text/plain");
        }

        /// <summary>
        /// Sunucumuzdaki fiziksel bir yoldan projemizin parçası olmayan bir dosya almamız gerekirse, PhysicalFileResult sınıfını kullanabiliriz.
        /// </summary>
        /// <returns>PhysicalFileResult</returns>
        public IActionResult PhysicalFileResult()
        {
            // Örnek fiziksel yol "D:/websites/www/httpdocs/"
            //string rootPath = _hostingEnvironment.ContentRootPath;
            //return PhysicalFile(@"C:\Users\User\Documents\Visual Studio 2017\Projects\VS2017Test\VS2017Test\Controllers\HomeController.cs", "text/plain", "HomeController.cs");
            return new PhysicalFileResult("D:/websites/www/httpdocs//wwwroot/site.css", "text/plain");
        }

        #endregion

        #region Custom Action Result
        /// <summary>
        /// Custom Javascript REsult Örneği
        /// </summary>
        /// <returns>Javscript Code</returns>
        public JavascriptResult JavascriptResult()
        {
            string alert ="alert('Custom Action Result')";
            return new JavascriptResult(alert);
        }
        #endregion
    }
}
