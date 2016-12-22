// Copyright 2016 Al Nyveldt - http://nyveldt.com, Ole Koeckemann <ole.k@web.de>
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
using System.Xml;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Razor;

namespace RazorPDF
{
    public class PdfView : IView
    {
        private readonly IViewEngine _viewEngine;

        public string Path { get; set; }

        public PdfView(IViewEngine viewEngine)
        {
            _viewEngine = viewEngine;
        }

        public Task RenderAsync(ViewContext context)
        {
            var sb = new System.Text.StringBuilder();
            TextWriter tw = new System.IO.StringWriter(sb);


            var _bufferScope = context.HttpContext.RequestServices.GetRequiredService<IViewBufferScope>();


            var buffer = new ViewBuffer(_bufferScope, Path, ViewBuffer.ViewPageSize);
            var writer = new ViewBufferTextWriter(buffer, context.Writer.Encoding);
            
            context.Writer.Write("Hello PDF");

            return TaskCache.CompletedTask;

            // detect itext (or html) format of response
            /*XmlParser parser;
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

            pdfWriter.Close();*/
        }

        private static XmlTextReader GetXmlReader(string source)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(source);
            MemoryStream stream = new MemoryStream(byteArray);

            var xtr = new XmlTextReader(stream);
            xtr.WhitespaceHandling = WhitespaceHandling.None; // Helps iTextSharp parse 
            return xtr;
        }
    }
}
