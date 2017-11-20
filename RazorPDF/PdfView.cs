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

namespace RazorPDF
{
    using System.IO;
    using System.Text;
    using System.Web.Mvc;

    public class PdfView : IView, IViewEngine
    {
        private readonly ViewEngineResult _result;

        public PdfView(ViewEngineResult result)
        {
            this._result = result;
        }

        public void Render(
            ViewContext viewContext,
            TextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            this._result.View.Render(viewContext, tw);
            string resultCache = sb.ToString();
            PdfParser parser = new PdfParser();
            MemoryStream ms = parser.ParseHtml(resultCache);
            viewContext.HttpContext.Response.ContentType = "application/pdf";
            viewContext.HttpContext.Response.BinaryWrite(ms.ToArray());
        }
        
        public ViewEngineResult FindPartialView(
            ControllerContext controllerContext,
            string partialViewName,
            bool useCache)
        {
            throw new System.NotImplementedException();
        }


        public ViewEngineResult FindView(
            ControllerContext controllerContext, 
            string viewName, 
            string masterName,
            bool useCache)
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseView(
            ControllerContext controllerContext,
            IView view)
        {
            this._result.ViewEngine
                .ReleaseView(controllerContext, this._result.View);
        }
    }
}