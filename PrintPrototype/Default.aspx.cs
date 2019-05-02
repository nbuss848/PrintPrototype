using IronBarCode;
using IronPdf;
using Stubble.Core.Builders;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
            // TODO: Barcodes 10 %
            // TODO: Images 10 %
            // TODO: Mobile don't have it installed locally (20 %)
            if (!IsPostBack)
            {
                String pkInstalledPrinters;
                for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                    ddInstalledPrinters.Items.Add(pkInstalledPrinters);
                }
            }
        }

        protected void btPrint_Click(object sender, EventArgs e)
        {
            var stubble = new StubbleBuilder().Build();
            var myObj = new Test() { name = "Chris", value = 10000, taxed_value = 100000, in_ca = true };
            using (StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/templates/template.Mustache"), Encoding.UTF8))
            {
                var output = stubble.Render(streamReader.ReadToEnd(), myObj);    

                IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
                Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Screen;

                var path = Server.MapPath("~/Content/");
                var PDF = Renderer.RenderHtmlAsPdf(output);
                var document = PDF.GetPrintDocument();
                document.PrinterSettings.PrinterName = ddInstalledPrinters.SelectedValue;
                document.Print();
            }
        }

        protected void btToPDF_Click(object sender, EventArgs e)
        {
            GeneratedBarcode MyBarCode = BarcodeWriter.CreateBarcode("Any Number, String or Binary Value", BarcodeWriterEncoding.Code128);
            var image = MyBarCode.ToDataUrl();

            var stubble = new StubbleBuilder().Build();
            var myObj = new Test() { name = "Chris", value = 10000, taxed_value = 100000, in_ca = true, barcode =  image};
            using (StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/templates/template.Mustache"), Encoding.UTF8))
            {
                var output = stubble.Render(streamReader.ReadToEnd(), myObj);
                // Do Stuff

                IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();

                Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Screen;

                var path = Server.MapPath("~/Content/");
                var PDF = Renderer.RenderHtmlAsPdf(output);

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