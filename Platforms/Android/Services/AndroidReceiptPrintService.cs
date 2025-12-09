#if ANDROID
using Android.Content;
using Android.OS;
using Android.Print;
using Android.Webkit;
using Android.Widget;
using CakeByHtoo.Models;
using CakeByHtoo.CommonService;
using CakeByHtoo.Interfaces;
using WebView = Android.Webkit.WebView;
using Android.Accounts;
using static System.Net.Mime.MediaTypeNames;
using Xamarin.Google.Crypto.Tink.Shaded.Protobuf;

namespace CakeByHtoo.Platforms.Android.Services
{
    public class AndroidReceiptPrintService : IPrintService
    {
        private WebView _webView;

        public void PrintReceipt(OrderData orderdata)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var context = Platform.CurrentActivity;
                    if (context == null) return;

                    // Create WebView for printing
                    _webView = new WebView(context);
                    _webView.Settings.JavaScriptEnabled = true;

                    // Generate HTML receipt
                    string htmlContent = GenerateReceiptHtml(orderdata);

                    // Load HTML
                    _webView.LoadDataWithBaseURL(
                        null,
                        htmlContent,
                        "text/html",
                        "UTF-8",
                        null);

                    // Set WebViewClient to handle printing after page loads
                    _webView.SetWebViewClient(new ReceiptWebViewClient(this));
                }
                catch (Exception ex)
                {
                    Toast.MakeText(Platform.CurrentActivity, $"Error: {ex.Message}", ToastLength.Short).Show();
                }
            });
        }

        public string GenerateReceiptHtml(OrderData orderData)
        {
            string receiptHTML = $@"
<!DOCTYPE html>
<html>
<head>
    <title>Sales Slip</title>
    <meta charset=""UTF-8"">
    <style>
        @page {{
            margin: 0;
            padding: 0;
            size: 88mm auto;
        }}
        body {{
            margin: 0;
            padding: 3mm;
            width: 88mm;
            background: white;
            font-family: 'Courier New', monospace;
            font-size: 11px;
            line-height: 1;
        }}
        .receipt {{
            width: 100%;
            max-width: 88mm;
        }}
        .receipt-header {{
            text-align: center;
            margin-bottom: 6px;
            border-bottom: 1px dashed #000;
            padding-bottom: 6px;
        }}
        .receipt-header h2 {{
            font-size: 14px;
            margin-bottom: 3px;
            font-weight: bold;
        }}
        .section {{
            margin-bottom: 8px;
            padding-bottom: 6px;
            border-bottom: 1px dashed #000;
        }}
        .section-title {{
            font-weight: bold;
            margin-bottom: 4px;
            text-decoration: underline;
        }}
        .details-grid div {{
            margin-bottom: 2px;
        }}
        .order-items table {{
            width: 100%;
            border-collapse: collapse;
        }}
        .order-items th {{
            border-bottom: 1px dashed #000;
            padding: 3px 1px;
            text-align: left;
            font-size: 9px;
        }}
        .order-items td {{
            padding: 2px 1px;
            border-bottom: 1px dashed #eee;
            font-size: 9px;
            vertical-align: top;
        }}
        .item-details {{
            font-size: 8px;
            color: #666;
            padding-left: 5px;
        }}
        .total-section {{
            margin-top: 8px;
            padding-top: 6px;
            border-top: 1px dashed #000;
            text-align: center;
            font-weight: bold;
        }}
        .receipt-footer {{
            text-align: center;
            margin-top: 12px;
            padding-top: 6px;
            border-top: 1px dashed #000;
            font-style: italic;
            font-size: 10px;
        }}
    </style>
</head>
<body>
    <div class=""receipt"">
        <div class=""receipt-header"">
            <h2>{(orderData.storeName ?? "Cake By Htoo").ToUpper()}</h2>
            <div>Tel: 09-797910344,09-786555820</div>
            <div>Order: ORD_{orderData.orderId.ToString().PadLeft(8, '0')}</div>
        </div>

        <div class=""section"">
            <div class=""section-title"">ORDER DETAILS</div>
            <div class=""details-grid"">
                <div><strong>Order Date:</strong> {FormatDate(orderData.orderDate)}</div>
                <div><strong>Pickup Date:</strong> {(orderData.pickupDate.HasValue ? FormatDate(orderData.pickupDate.Value) : "ASAP")}</div>
                <div><strong>Order Note:</strong> {orderData.orderNote ?? "-"}</div>
            </div>
        </div>

        <div class=""section"">
            <div class=""section-title"">CUSTOMER</div>
            <div class=""details-grid"">
                <div><strong>Name:</strong> {orderData.customerName}</div>
                <div><strong>Phone:</strong> {orderData.customerPhone}</div>
                <div><strong>Address:</strong> {orderData.customerAddress}</div>
            </div>
        </div>

        <div class=""section"">
            <div class=""section-title"">ORDER ITEMS</div>
            <div class=""order-items"">
                <table>
                    <thead>
                        <tr>
                            <th>ITEM</th>
                            <th>SIZE</th>
                            <th>QTY</th>
                            <th>PRICE</th>
                            <th>SUB-TOTAL</th>
                        </tr>
                    </thead>
                    <tbody>";

            // ⬇ Generate each item as C#
            foreach (var item in orderData.orderItems)
            {
                string itemName = GetItemDisplayName(item);
                string size = GetSizeDisplay(item);
                decimal totalPrice = (item.unitPrice + item.extraPrice) * item.quantity;

                receiptHTML += $@"
                        <tr>
                            <td>{TruncateText(itemName, 20)}</td>
                            <td>{size}</td>
                            <td>{item.quantity}</td>
                            <td>{FormatCurrency(item.unitPrice)}</td>
                            <td>{FormatCurrency(totalPrice)}</td>
                        </tr>";

                if (!string.IsNullOrWhiteSpace(item.accessory))
                {
                    receiptHTML += $@"
                        <tr>
                            <td colspan=""5"" class=""item-details"">Accessory+ {TruncateText(item.accessory, 30)}</td>
                        </tr>";
                }

                if (item.extraPrice > 0)
                {
                    receiptHTML += $@"
                        <tr>
                            <td colspan=""5"" class=""item-details"">ExtraPrice: {FormatCurrency(item.extraPrice)}</td>
                        </tr>";
                }

                if (!string.IsNullOrWhiteSpace(item.cakeNote))
                {
                    receiptHTML += $@"
                        <tr>
                            <td colspan=""5"" class=""item-details"">Note: {TruncateText(item.cakeNote, 40)}</td>
                        </tr>";
                }

                if (!string.IsNullOrWhiteSpace(item.flavourName) &&
                    (item.categoryId == 1 || item.categoryId == 2))
                {
                    receiptHTML += $@"
                        <tr>
                            <td colspan=""5"" class=""item-details"">Flavour: {TruncateText(item.flavourName, 35)}</td>
                        </tr>";
                }
            }

            receiptHTML += $@"
                    </tbody>
                </table>
            </div>
        </div>

        <div class=""total-section"">
            <div style=""font-size: 12px;"">TOTAL AMOUNT: <strong>{FormatCurrency(orderData.totalAmount)}</strong></div>
        </div>

        <div class=""receipt-footer"">
            *** THANK YOU ***<br>
            Thank you for your purchase! 🍰<br>
            Life is short, eat more cake.<br>
            Contact: 09-797910344,09-786555820
        </div>

        <div style=""text-align: center; margin-top: 8px; font-size: 8px;"">
            {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}
        </div>
    </div>
</body>
</html>";

            return receiptHTML;
        }

        // Format functions
        public string FormatDate(DateTime? date)
        {
            return date?.ToString("dd/MM/yyyy") ?? "";
        }

        public string FormatCurrency(decimal amount)
        {
            return string.Format("{0:N0} MMK", amount);
        }

        public string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            return text.Length > maxLength ? text.Substring(0, maxLength - 3) + "..." : text;
        }

        public string GetItemDisplayName(OrderItemData item)
        {
            return (item.categoryId != 3 && item.categoryId != 4)
                ? item.categoryName ?? item.productItemName
                : item.productItemName;
        }

        public string GetSizeDisplay(OrderItemData item)
        {
            string size = item.productSize ?? "";

            if (item.categoryId == 1) size += "\"";
            else if (item.categoryId == 2) size += " ppl";

            return size;
        }

        private void StartPrintJob()
        {
            try
            {
                var context = Platform.CurrentActivity;
                if (context == null || _webView == null) return;

                var printManager = (PrintManager)context.GetSystemService(Context.PrintService);
                if (printManager == null) return;

                // Create print adapter from WebView
                var printAdapter = _webView.CreatePrintDocumentAdapter("Receipt-" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss"));

                // Start print job
                printManager.Print("Receipt-" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss"), printAdapter, null);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Platform.CurrentActivity, $"Print failed: {ex.Message}", ToastLength.Short).Show();
            }
        }

      
        private class ReceiptWebViewClient : WebViewClient
        {
            private readonly AndroidReceiptPrintService _service;

            public ReceiptWebViewClient(AndroidReceiptPrintService service)
            {
                _service = service;
            }

            public override void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);

                // Wait for page to render, then print
                new Handler(Looper.MainLooper).PostDelayed(() =>
                {
                    _service.StartPrintJob();
                }, 500);
            }
        }
    }
}
#endif