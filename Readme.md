RazorPDF
==============

RazorPDF is a simple project that makes it a breeze to create PDFs using the Razor view engine. Since Razor is really a template syntax, it can do lot more than just generate HTML.  RazorPDF uses it to generate iText XML.  Then using the iTextSharp library, we turn that iText XML into a PDF to return.  The end result is a easy to use, clean method for generating PDFs.

##Usage

Simple usage: Just return the PdfResult. In this case you will need a view file like usual.
```
public ActionResult PdfView()
{
	return this.Pdf(new PdfModel
	{
		Field1 = "Foo",
		Field2 = "Bar"
	});
}
```



More complex: Get razor template from file or from your CMS 
``` 
const string razor = "<h1>PDF </h1><p>Hello @Model.Field1</p><p>@Model.Field2</p>";
```

Parse PDF from Razor template 
```
var ms = (new RazorPDF.PdfParser()).ParseRazor(razor, model);
```

Then you can save the memory stream to file
```
var content = ms.ToArray();
using (var fs = System.IO.File.OpenWrite(HttpContext.Server.MapPath("~/App_Data/foobar.pdf")))
{
	fs.Write(content, 0, (int)content.Length);
}
```

Return memory stream as PdfView
```
return this.Pdf(ms.ToArray()); 
``` 

And if you already have PDF files on file system.
```
return this.PdfFile(HttpContext.Server.MapPath("~/App_Data/foobar.pdf"));
``` 

