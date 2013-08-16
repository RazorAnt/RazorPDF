using RazorPDF;
using RazorPDFSample.Models;
using System.Web.Mvc; 

namespace RazorPDFSample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PdfView()
        {
            return this.Pdf(new PdfModel
            {
                Field1 = "Jim Panse",
                Field2 = "Foo Bar"
            });
        }

        public ActionResult PdfRazor()
        {
            // Get razor from file or from your CMS 
            const string razor = "<h1>PDF  3</h1><p>Hello @Model.Field1</p><p>@Model.Field2</p>";

            // Provide model 
            var model = new PdfModel
            {
                Field1 = "Jim Panse",
                Field2 = "Foo Bar"
            };

            // Parse PDF from Razor template 
            var ms = (new RazorPDF.PdfParser()).ParseRazor(razor, model);

            // Write out PDF to file system 
            var content = ms.ToArray();
            using (var fs = System.IO.File.OpenWrite(HttpContext.Server.MapPath("~/App_Data/foobar.pdf")))
            {
                fs.Write(content, 0, (int)content.Length);
            }
            
            // Return memory stream 
            return this.Pdf(content); 

            // Or return file as stream 
            return this.PdfFile(HttpContext.Server.MapPath("~/App_Data/foobar.pdf")); 
        }
    }
}
