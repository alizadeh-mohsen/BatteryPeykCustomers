using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using BatteryPeykCustomers.Pages.Admin.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BatteryPeykCustomers.Pages.Admin.Batteries
{
    // This page returns a PDF file (no visible HTML needed).
    public class ExportPdfModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExportPdfModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var batteries = await _context.Battery
                .Include(b => b.Company)
                .Include(b => b.Amper)
                .Where(b => b.Quantity > 0)
                .Select(g => new Battery
                {
                    Company = g.Company,
                    Amper = g.Amper,
                    Quantity = g.Quantity
                })
                .OrderBy(b => b.Company!.Title)
                .ThenBy(b => b.Amper.Title)
                .ToListAsync();

            var document = new BatteriesReportDocument(batteries);
            byte[] pdfBytes = document.GeneratePdf();
            var fileName = "BatteryPeyk-" + DateTime.Today.ToString("yyyy-MM-dd") + ".pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }

        // Simple QuestPDF document that lists Company.Title, Amper.Title and Battery.Quantity
        private class BatteriesReportDocument : IDocument
        {
            private readonly List<Battery> _batteries;
            public BatteriesReportDocument(List<Battery> batteries) => _batteries = batteries;

            public DocumentMetadata GetMetadata() => new DocumentMetadata { Title = "Batteries Report: " + DateTime.Today.ToString("yyyy-MM-dd") };

            public void Compose(IDocumentContainer container)
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(20);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    //page.Header().Text("گزارش انبار - " + DateTime.Now.ToString("yyyy/MM/dd HH:mm")).SemiBold().FontSize(16).AlignCenter();
                    page.Header().Column(column =>
                    {
                        column.Item().Text(DateTime.Now.ToString("yyyy/MM/dd HH:mm")).FontSize(12).AlignLeft();
                        column.Item().Text("گزارش انبار").SemiBold().FontSize(16).AlignCenter();
                        column.Item().Text("موجودی: " + _batteries.Sum(b=>b.Quantity)).FontSize(12).AlignCenter();
                    });

                    page.Content().PaddingVertical(10).Element(ComposeContent);

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("صفحه ");
                        txt.CurrentPageNumber();
                        txt.Span(" از ");
                        txt.TotalPages();
                    });
                });
            }

            void ComposeContent(IContainer container)
            {
                container.Table(table =>
                {
                    // three columns: Company, Amper, Quantity
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(3);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).AlignLeft().Text("موجودی").Bold();
                        header.Cell().Element(CellStyle).AlignCenter().Text("آمپر").Bold();
                        header.Cell().Element(CellStyle).AlignRight().Text("باتری").Bold();
                    });

                    foreach (var b in _batteries)
                    {
                        table.Cell().Element(CellStyle).AlignLeft().Text(b.Quantity.ToString());
                        table.Cell().Element(CellStyle).AlignCenter().Text(b.Amper?.Title ?? string.Empty);
                        table.Cell().Element(CellStyle).AlignRight().Text(b.Company?.Title ?? string.Empty);
                    }
                });
            }

            static IContainer CellStyle(IContainer container) =>
                container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(4);
        }
    }
}