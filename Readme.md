RazorPDF Core
==============

RazorPDF is a simple project that makes it a breeze to create PDFs using the Razor view engine. Since Razor is really a template syntax, it can do lot more than just generate HTML.  RazorPDF uses it to generate iText XML.  Then using the iTextSharp library, we turn that iText XML into a PDF to return.  The end result is a easy to use, clean method for generating PDFs.

## Installation

**PLEASE NOTE:** This installation only applies to the original package

For Visual Studio 2015 use the Package Manager Console to install `RazorPDFCore` (https://www.nuget.org/packages/RazorPDFCore/)

`PM> Install-Package RazorPDFCore`

or download it from here and add it as additional project

## Usage

Add the `PdfResultExecutor` service into your `Startup.cs`

```
public void ConfigureServices(IServiceCollection services) {
    // [...]
    services.AddSingleton<PdfResultExecutor>();
}
```

Return the below command in your controller action

`ViewPdf(object model, string fileName, string viewName, bool download)`

Example:

```
class YourBaseController : RazorPDF.Controller {
    // [...]
    public IActionResult Pdf() {
        var model = /* any model you wish */
        return ViewPdf(model);
    }
}
```

PLEASE NOTE: 
This method becomes ONLY available, when you use the inherited Controller class `RazorPDF.Controller`

## Changelog

**v1.0.6**
- updated AspNetCore v2.0

**v1.0.5**
- updated AspNetCore v1.1.3

**v1.0.3**
- make use of the XMLWorker instead to give more html/css flexibility

**v1.0.2**
- replaced *.csproj with *.xproj


**v1.0.1**
- updated source code to work with ASP.NET Core (tested on v1.0.1)

**v1.0.0**
- forked from RazorAnt/RazorPDF

