using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace RazorPDF
{
    public class PdfView : IView, IViewEngine
    {
        private readonly ViewEngineResult _result;
        public PdfView(ViewEngineResult result)
        {
            _result = result;
        }


        public void Render(ViewContext viewContext, TextWriter writer)
        {
            // generate view into string
            var sb = new System.Text.StringBuilder();
            TextWriter tw = new System.IO.StringWriter(sb);
            _result.View.Render(viewContext, tw);
            var resultCache = sb.ToString();

            var ms = new MemoryStream();
            var document = new Document();
            var pdfWriter = PdfWriter.GetInstance(document, ms);
            var worker = new HTMLWorker(document);
            document.Open();
            worker.StartDocument();

            pdfWriter.CloseStream = false;


            worker.Parse(new StringReader(resultCache));
            worker.EndDocument();
            worker.Close();
            document.CloseDocument();
            document.Close();

            // this is as close as we can get to being "success" before writing output
            // so set the content type now
            viewContext.HttpContext.Response.ContentType = "application/pdf";
            pdfWriter.Flush();
            pdfWriter.Close();

            viewContext.HttpContext.Response.BinaryWrite(ms.ToArray());
        }


        private static XmlTextReader GetXmlReader(string source)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(source);
            MemoryStream stream = new MemoryStream(byteArray);


            var xtr = new XmlTextReader(stream);
            xtr.WhitespaceHandling = WhitespaceHandling.None; // Helps iTextSharp parse 
            return xtr;
        }


        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            throw new System.NotImplementedException();
        }


        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            throw new System.NotImplementedException();
        }


        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            _result.ViewEngine.ReleaseView(controllerContext, _result.View);
        }
    }
}