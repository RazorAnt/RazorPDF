using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPDF
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        public PdfResult ViewPdf(object model, string fileName)
        {
            ViewData.Model = model;

            return new RazorPDF.PdfResult()
            {
                FileName = fileName,
                TempData = TempData,
                ViewData = ViewData
            };
        }

        public PdfResult ViewPdf(object model, string fileName, bool download)
        {
            ViewData.Model = model;

            return new RazorPDF.PdfResult()
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

            return new RazorPDF.PdfResult()
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

            return new RazorPDF.PdfResult()
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
