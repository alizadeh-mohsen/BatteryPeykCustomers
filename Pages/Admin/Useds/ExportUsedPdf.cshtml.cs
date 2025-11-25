using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Pages.Admin.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BatteryPeykCustomers.Pages.Admin.Batteries
{
    public class ExportUsedPdfModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExportUsedPdfModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<UsedReportGrouped> historyQuery = _context.UsedHistory.GroupBy(u => new { u.Amper })
      .Select(g => new UsedReportGrouped
      {
          Amper = g.Key.Amper,
          //Brand = g.Key.Brand,
          Count = g.Count()
      }).OrderBy(c => c.Amper);

            var history = await historyQuery.ToListAsync();

            var document = new UsedBatteriesReportDocument(history);
            byte[] pdfBytes = document.GeneratePdf();
            var fileName = "Daghi-" + DateTime.Today.ToString("yyyy-MM-dd") + ".pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }

        // Simple QuestPDF document that lists Company.Title, Amper.Title and Battery.Quantity
        private class UsedBatteriesReportDocument : IDocument
        {
            private readonly List<UsedReportGrouped> _usedHistories;
            public UsedBatteriesReportDocument(List<UsedReportGrouped> used) => _usedHistories = used;

            public DocumentMetadata GetMetadata() => new DocumentMetadata { Title = "Daghi Report: " + DateTime.Today.ToString("yyyy-MM-dd") };

            public void Compose(IDocumentContainer container)
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(20);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Column(column =>
                    {
                        column.Item().Text(DateTime.Now.ToString("yyyy/MM/dd HH:mm")).FontSize(12).AlignLeft();
                        column.Item().Text("گزارش داغی ها").SemiBold().FontSize(16).AlignCenter();
                        column.Item().Text("تعداد: " + _usedHistories.Sum(c => c.Count)).FontSize(12).AlignCenter();
                        column.Item().Text("آمپر: " + _usedHistories.Sum(b => b.Count * b.Amper)).FontSize(12).AlignCenter();
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
                        columns.RelativeColumn(2);
                        //columns.RelativeColumn(2);
                        columns.RelativeColumn(2);
                    });

                    table.Header(header =>
                    {
                        //header.Cell().Element(CellStyle).AlignCenter().Text("برند").Bold();
                        header.Cell().Element(CellStyle).AlignCenter().Text("آمپر").Bold();
                        header.Cell().Element(CellStyle).AlignCenter().Text("تعداد").Bold();


                    });

                    foreach (var b in _usedHistories)
                    {
                        //table.Cell().Element(CellStyle).AlignCenter().Text(b.Brand);
                        table.Cell().Element(CellStyle).AlignCenter().Text(b.Amper.ToString());
                        table.Cell().Element(CellStyle).AlignCenter().Text(b.Count.ToString());
                    }
                });
            }

            static IContainer CellStyle(IContainer container) =>
                container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(4);
        }
    }
}
