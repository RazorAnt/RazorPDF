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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace RazorPDFCore
{
    public class PdfResult : ActionResult
    {
        /// <summary>
        /// Gets or sets the Content-Type header for the response.
        /// </summary>
        public string ContentType { get; set; }
        public string ContentDisposition { get; private set; }
        /// <summary>
        /// Gets the view data model.
        /// </summary>
        public object Model => ViewData?.Model;

        public int StatusCode { get { return 200; } }

        public ViewDataDictionary ViewData{ get; set; }

        public ITempDataDictionary TempData { get; set; }

        public string ViewName { get; set; }

        public string FileName { get; set; }
        public bool Download { get; set; }

        public IViewEngine ViewEngine { get; set; }


        //Constructors
        public PdfResult()
        {
            ContentType = "application/pdf";
        }

        public async override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if(Download)
                ContentDisposition = $"attachment; filename=\"{FileName}\"";
            else
                ContentDisposition = $"inline; filename=\"{FileName}\"";

            var services = context.HttpContext.RequestServices;
            var executor = services.GetRequiredService<PdfResultExecutor>();

            var result = executor.FindView(context, this);
            result.EnsureSuccessful(originalLocations: null);

            var view = result.View;
            using (view as IDisposable)
            {
                await executor.ExecuteAsync(context, view, this);
            }
        }
    }
}
