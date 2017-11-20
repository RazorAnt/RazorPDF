// Copyright 2017 Russlan Akiev
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
    using System.Web.Mvc;

    public static class ControllerExtensions
    {
        public static ActionResult Pdf(
            this Controller ctrl)
        {
            return new PdfResult();
        }

        public static ActionResult Pdf(
            this Controller ctrl, 
            string viewName)
        {
            return new PdfResult(viewName, null);
        }

        public static ActionResult Pdf(
            this Controller ctrl, 
            object model)
        {
            return new PdfResult(null, model);
        }

        public static ActionResult Pdf(
            this Controller ctrl, 
            byte[] buffer)
        {
            return new FileStreamResult(
                new MemoryStream(buffer),
                "application/pdf"
            );
        }

        public static ActionResult PdfFile(
            this Controller ctrl,
            string filePath)
        {
            return new FileStreamResult(
                new FileStream(
                    filePath, 
                    FileMode.Open
                ), 
                "application/pdf"
            );
        }
    }
}
