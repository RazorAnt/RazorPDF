RazorPDF
==============

RazorPDF is a simple project that makes it a breeze to create PDFs using the Razor view engine. Since Razor is really a template syntax, it can do lot more than just generate HTML.  RazorPDF uses it to generate iText XML.  Then using the iTextSharp library, we turn that iText XML into a PDF to return.  The end result is a easy to use, clean method for generating PDFs.

##Usage
The easiest way to get started with RazorPDF is to add the Nuget package to your MVC project. There is a short screencast on [my blog](http://nyveldt.com/blog/page/razorpdf) to get you started so well as a sample project and some syntax samples.

##Acknowledgements
RazorPDF likely wouldn't exist without the [Spark view engine](http://sparkviewengine.com/). The ability to create PDFs with the Spark view engine is something I've missed often since switching to using Razor as my default view engine in MVC projects. Huge thanks to [Louis DeJardin](http://whereslou.com/) for putting together the Spark view engine many years ago and for the idea of mixing Spark with iTextSharp as a nice way to make PDFs.

Also, RazorPDF is worthless without [iTextSharp](http://sourceforge.net/projects/itextsharp/).  Thanks so much to the team that works on that incredible project.

