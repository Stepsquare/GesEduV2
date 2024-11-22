using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Playwright;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;

namespace GesEdu.Helpers.ExportPdf
{
    public class PdfRenderer(HtmlRenderer htmlRenderer)
    {
        private readonly HtmlRenderer _htmlRenderer = htmlRenderer;

        public Task<byte[]> RenderComponent<T>() where T : IComponent
            => RenderComponent<T>(ParameterView.Empty);

        public Task<byte[]> RenderComponent<T>(Dictionary<string, object?> dictionary) where T : IComponent
            => RenderComponent<T>(ParameterView.FromDictionary(dictionary));

        private async Task<byte[]> RenderComponent<T>(ParameterView parameters) where T : IComponent
        {
            var html = await _htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                HtmlRootComponent output = await _htmlRenderer.RenderComponentAsync<T>(parameters);

                return output.ToHtmlString();
            });

            using var playwright = await Playwright.CreateAsync();

            var options = new BrowserTypeLaunchOptions
            {
                Headless = true
            };

            var browser = await LaunchChromiumBrowser(playwright, options);

            var page = await browser.NewPageAsync();
            await page.SetContentAsync(html);

            var pdfFile = await page.PdfAsync(new PagePdfOptions
            {
                Format = "A4",
                Scale = 0.8f,
                Margin = new Margin
                {
                    Top = "50px",
                    Bottom = "80px",
                    Left = "50px",
                    Right = "50px"
                },
                DisplayHeaderFooter = true,
                HeaderTemplate = @"<div></div>",
                FooterTemplate = @"<div style='font-size:10px; text-align:right; width:100%;margin:0 55px 20px 0;'>
                                       Página <span class='pageNumber'></span> de <span class='totalPages'></span>
                                   </div>"
            });

            await page.CloseAsync();

            return pdfFile;
        }

        private async Task<IBrowser> LaunchChromiumBrowser(IPlaywright playwright, BrowserTypeLaunchOptions options)
        {
            IBrowser browser;

            try
            {
                browser = await playwright.Chromium.LaunchAsync(options);
            }
            catch (PlaywrightException e) when (e.Message.Contains("Executable doesn't exist"))
            {
                Microsoft.Playwright.Program.Main(["install", "chromium"]);
                browser = await playwright.Chromium.LaunchAsync(options);
            }

            return browser;
        }
    }
}
