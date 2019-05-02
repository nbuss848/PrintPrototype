using IronPdf;
using Stubble.Core.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrintPrototype
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var stubble = new StubbleBuilder().Build();
            var myObj = new Test() { name = "Chris", value = 10000, taxed_value = 100000, in_ca = true };
            using (StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/templates/template.Mustache"), Encoding.UTF8))
            {
                var output = stubble.Render(streamReader.ReadToEnd(), myObj);
                // Do Stuff

                IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
         
                Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Screen;
                //  Renderer.vi = 1280;
                //// Renderer.RenderHtmlAsPdf(output).SaveAs(Server.MapPath("~/Content/templates/result.pdf"));
                var path = Server.MapPath("~/Content/");
                var PDF = Renderer.RenderHtmlAsPdf(output);
                //PDF.SaveAs("~/Content/Result.pdf");
                //Response.TransmitFile(PDF);
                byte[] Binary = PDF.BinaryData;
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "Result.pdf");
                Response.ContentType = "application/octet-stream";
                Context.Response.OutputStream.Write(Binary, 0, Binary.Length);
                Response.Flush();
            }
        }
    }
}