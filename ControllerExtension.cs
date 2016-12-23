namespace RazorPDFCore
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        public PdfResult ViewPdf(object model, string fileName)
        {
            ViewData.Model = model;

            return new PdfResult()
            {
                FileName = fileName,
                TempData = TempData,
                ViewData = ViewData
            };
        }

        public PdfResult ViewPdf(object model, string fileName, bool download)
        {
            ViewData.Model = model;

            return new PdfResult()
            {
                FileName = fileName,
                TempData = TempData,
                ViewData = ViewData,
                Download = download
            };
        }

        public PdfResult ViewPdf(object model, string fileName, string viewName)
        {
            ViewData.Model = model;

            return new PdfResult()
            {
                ViewName = viewName,
                FileName = fileName,
                TempData = TempData,
                ViewData = ViewData
            };
        }

        public PdfResult ViewPdf(object model, string fileName, string viewName, bool download)
        {
            ViewData.Model = model;

            return new PdfResult()
            {
                ViewName = viewName,
                FileName = fileName,
                TempData = TempData,
                ViewData = ViewData,
                Download = download
            };
        }
    }
}
