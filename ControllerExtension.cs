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

        public PdfResult ViewPdf(object model, string fileName, bool download = false, iTextSharp.text.Rectangle pageSize = null)
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

        public PdfResult ViewPdf(object model, string fileName, string viewName, bool download = false, iTextSharp.text.Rectangle pageSize = null)
        {
            ViewData.Model = model;

            if (pageSize == null) pageSize = iTextSharp.text.PageSize.A4;

            return new PdfResult()
            {
                ViewName = viewName,
                FileName = fileName,
                TempData = TempData,
                ViewData = ViewData,
                Download = download,
                PageSize = pageSize
            };
        }
    }
}
