﻿// Copyright 2012 Al Nyveldt - http://nyveldt.com
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

using System.Web.Mvc;

namespace RazorPDF
{
    public class PdfResult : ViewResult
    {
        //Constructors
        public PdfResult(object model, string name)
        {
            ViewData = new ViewDataDictionary(model);
            ViewName = name;
        }
        public PdfResult() : this(new ViewDataDictionary(), "Pdf")
        {
        }
        public PdfResult(object model) : this(model, "Pdf")
        {
        }

        //Override FindView to load PdfView
        protected override ViewEngineResult FindView(ControllerContext context)
        {
            var result = base.FindView(context);
            if (result.View == null)
                return result;

            var pdfView = new PdfView(result);
            return new ViewEngineResult(pdfView, pdfView);
        }
    }
}
