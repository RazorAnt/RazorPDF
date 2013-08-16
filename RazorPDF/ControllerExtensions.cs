using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace RazorPDF
{
    public static class ControllerExtensions
    {
        public static ActionResult Pdf(this Controller ctrl)
        {
            return new PdfResult();
        }

        public static ActionResult Pdf(this Controller ctrl, string viewName)
        {
            return new PdfResult(viewName, null);
        }

        public static ActionResult Pdf(this Controller ctrl, object model)
        {
            return new PdfResult(null, model);
        }

        public static ActionResult Pdf(this Controller ctrl, byte[] buffer)
        {
            return new FileStreamResult(new MemoryStream(buffer), "application/pdf");
        }

        public static ActionResult PdfFile(this Controller ctrl, string filePath)
        {
            return new FileStreamResult(new FileStream(filePath, FileMode.Open), "application/pdf");
        }
    }
}
