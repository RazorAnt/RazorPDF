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

using System.IO;
using System.Web.Mvc;

namespace RazorPDF
{
    public class PdfView : IView, IViewEngine
    {
        private readonly ViewEngineResult result;

        public PdfView(ViewEngineResult result)
        {
            this.result = result;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            var sb = new System.Text.StringBuilder();
            var tw = new System.IO.StringWriter(sb);
            this.result.View.Render(viewContext, tw);
            var resultCache = sb.ToString();
            var parser = new PdfParser();
            var ms = parser.ParseHtml(resultCache);
            viewContext.HttpContext.Response.ContentType = "application/pdf";
            viewContext.HttpContext.Response.BinaryWrite(ms.ToArray());
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
            this.result.ViewEngine.ReleaseView(controllerContext, this.result.View);
        }
    }
}