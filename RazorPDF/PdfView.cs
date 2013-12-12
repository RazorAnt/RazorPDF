// Copyright 2012 Al Nyveldt - http://nyveldt.com
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 

using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;

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
            var sb = new StringBuilder();
            TextWriter tw = new StringWriter(sb);
            _result.View.Render(viewContext, tw);
            var resultCache = sb.ToString();

            // detect itext (or html) format of response
            XmlParser parser;
            using (var reader = GetXmlReader(resultCache))
            {
                while (reader.Read() && reader.NodeType != XmlNodeType.Element)
                {
                    // no-op
                }

                if (reader.NodeType == XmlNodeType.Element && reader.Name == "itext")
                    parser = new XmlParser();
                else
                    parser = new HtmlParser();
            }

            // Create a document processing context
            var document = new Document();
            document.Open();

            // associate output with response stream
            var pdfWriter = PdfWriter.GetInstance(document, viewContext.HttpContext.Response.OutputStream);
            pdfWriter.CloseStream = false;

            // this is as close as we can get to being "success" before writing output
            // so set the content type now
            viewContext.HttpContext.Response.ContentType = "application/pdf";

            // parse memory through document into output
            using (var reader = GetXmlReader(resultCache))
            {
                parser.Go(document, reader);
            }

            pdfWriter.Close();
        }

        private static XmlTextReader GetXmlReader(string source)
        {
            var byteArray = Encoding.UTF8.GetBytes(source);
            var stream = new MemoryStream(byteArray);

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
