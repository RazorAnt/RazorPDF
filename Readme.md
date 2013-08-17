RazorPDF
==============

RazorPDF is a simple project that makes it a breeze to create PDFs using the Razor view engine. Since Razor is really a template syntax, it can do lot more than just generate HTML.  RazorPDF uses it to generate iText XML.  Then using the iTextSharp library, we turn that iText XML into a PDF to return.  The end result is a easy to use, clean method for generating PDFs.

##Usage

Just return the PdfResult 
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

Or parse it to stream 

Get razor from file or from your CMS 
``` 
const string razor = "<h1>PDF </h1><p>Hello @Model.Field1</p><p>@Model.Field2</p>";
```

Parse PDF from Razor template 
```
var ms = (new RazorPDF.PdfParser()).ParseRazor(razor, model);
```

Them you can save the memory stream to file 
```
var content = ms.ToArray();
using (var fs = System.IO.File.OpenWrite(HttpContext.Server.MapPath("~/App_Data/foobar.pdf")))
{
	fs.Write(content, 0, (int)content.Length);
}
```

Or just return it as PdfView
```
return this.Pdf(ms.ToArray()); 
``` 

In case you want return PDF files from file system 
```
return this.PdfFile(HttpContext.Server.MapPath("~/App_Data/foobar.pdf"));
``` 

