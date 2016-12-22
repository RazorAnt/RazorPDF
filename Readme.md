RazorPDF
==============

RazorPDF is a simple project that makes it a breeze to create PDFs using the Razor view engine. Since Razor is really a template syntax, it can do lot more than just generate HTML.  RazorPDF uses it to generate iText XML.  Then using the iTextSharp library, we turn that iText XML into a PDF to return.  The end result is a easy to use, clean method for generating PDFs.

## Installation

(not yet available on nuget)

Download it from github and add it as additional project

## Usage

Once installed, the easiest way to convert a razor view into a PDF is to use the method:

`ViewPdf(object model, string fileName, string viewName, bool download)`

This method becomes available, when using the inherited class `RazorPDF.Controller`

Example:

```
class YourController : RazorPDF.Controller {
    // [...]
    public IActionResult Pdf() {
        var model = /* any model you wish */
        return ViewPdf(model);
    }
}
```

## Changelog

**v1.0.1**
- updated source code to work with ASP.NET Core (tested on v1.0.1)

**v1.0.0**
- forked from RazorAnt/RazorPDF

