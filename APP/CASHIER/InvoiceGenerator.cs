using System;
using System.Data;
using System.IO;
using System.Text;

namespace CASHIER
{
    /// <summary>
    /// Tạo hóa đơn HTML để in/xuất PDF
    /// </summary>
    public static class InvoiceGenerator
    {
        /// <summary>
        /// Tạo file HTML hóa đơn và mở trong trình duyệt
        /// </summary>
        public static string GenerateInvoiceHtml(
            string invoiceId,
            string orderId,
            string customerName,
            string customerPhone,
            string membershipLevel,
            DataTable orderDetails,
            decimal totalAmount,
            decimal discountedAmount,
            double discountRate,
            string discountName,
            string paymentMethod,
            decimal paymentMoney,
            string cashierName)
        {
            decimal discountValue = totalAmount - discountedAmount;
            decimal changeAmount = paymentMoney - discountedAmount;

            StringBuilder html = new StringBuilder();
            html.AppendLine(@"<!DOCTYPE html>
<html lang='vi'>
<head>
    <meta charset='UTF-8'>
    <title>Hóa đơn - " + invoiceId + @"</title>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Be+Vietnam+Pro:wght@400;500;600;700&display=swap');
        
        * { margin: 0; padding: 0; box-sizing: border-box; }
        
        body {
            font-family: 'Be Vietnam Pro', Arial, sans-serif;
            background: #f5f5f5;
            padding: 20px;
        }
        
        .invoice {
            max-width: 400px;
            margin: 0 auto;
            background: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.1);
        }
        
        .header {
            text-align: center;
            border-bottom: 2px dashed #ddd;
            padding-bottom: 20px;
            margin-bottom: 20px;
        }
        
        .logo {
            font-size: 28px;
            font-weight: 700;
            color: #2d3748;
            margin-bottom: 5px;
        }
        
        .logo span { color: #38a169; }
        
        .subtitle {
            color: #718096;
            font-size: 12px;
        }
        
        .invoice-id {
            background: #f7fafc;
            padding: 10px;
            border-radius: 5px;
            text-align: center;
            margin-bottom: 20px;
        }
        
        .invoice-id strong {
            color: #2d3748;
            font-size: 14px;
        }
        
        .section {
            margin-bottom: 15px;
        }
        
        .section-title {
            font-size: 11px;
            color: #a0aec0;
            text-transform: uppercase;
            letter-spacing: 1px;
            margin-bottom: 8px;
        }
        
        .info-row {
            display: flex;
            justify-content: space-between;
            padding: 5px 0;
            font-size: 13px;
        }
        
        .info-row .label { color: #718096; }
        .info-row .value { color: #2d3748; font-weight: 500; }
        
        .items {
            border-top: 1px solid #e2e8f0;
            border-bottom: 1px solid #e2e8f0;
            padding: 15px 0;
            margin: 15px 0;
        }
        
        .item {
            display: flex;
            justify-content: space-between;
            padding: 8px 0;
            font-size: 12px;
        }
        
        .item-name {
            flex: 1;
            color: #2d3748;
        }
        
        .item-qty {
            width: 40px;
            text-align: center;
            color: #718096;
        }
        
        .item-price {
            width: 100px;
            text-align: right;
            color: #2d3748;
            font-weight: 500;
        }
        
        .totals {
            padding: 15px 0;
        }
        
        .total-row {
            display: flex;
            justify-content: space-between;
            padding: 5px 0;
            font-size: 13px;
        }
        
        .total-row .label { color: #718096; }
        .total-row .value { color: #2d3748; }
        
        .total-row.discount .value {
            color: #ed8936;
        }
        
        .total-row.final {
            border-top: 2px solid #2d3748;
            padding-top: 12px;
            margin-top: 10px;
        }
        
        .total-row.final .label {
            font-size: 16px;
            font-weight: 600;
            color: #2d3748;
        }
        
        .total-row.final .value {
            font-size: 18px;
            font-weight: 700;
            color: #38a169;
        }
        
        .payment-info {
            background: #f0fff4;
            padding: 15px;
            border-radius: 8px;
            margin: 15px 0;
        }
        
        .payment-row {
            display: flex;
            justify-content: space-between;
            padding: 3px 0;
            font-size: 12px;
        }
        
        .payment-row .label { color: #276749; }
        .payment-row .value { color: #22543d; font-weight: 600; }
        
        .footer {
            text-align: center;
            padding-top: 20px;
            border-top: 2px dashed #ddd;
            margin-top: 20px;
        }
        
        .footer p {
            color: #a0aec0;
            font-size: 11px;
            margin: 3px 0;
        }
        
        .thank-you {
            font-size: 14px;
            color: #38a169;
            font-weight: 600;
            margin-bottom: 10px;
        }
        
        .print-btn {
            display: block;
            width: 100%;
            padding: 12px;
            background: #38a169;
            color: white;
            border: none;
            border-radius: 8px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            margin-top: 20px;
        }
        
        .print-btn:hover { background: #2f855a; }
        
        @media print {
            body { background: white; padding: 0; }
            .invoice { box-shadow: none; }
            .print-btn { display: none; }
        }
    </style>
</head>
<body>
    <div class='invoice'>
        <div class='header'>
            <div class='logo'>Pet<span>Care</span>X</div>
            <div class='subtitle'>Hệ thống chăm sóc thú cưng</div>
        </div>
        
        <div class='invoice-id'>
            <strong>HÓA ĐƠN: " + invoiceId + @"</strong>
        </div>
        
        <div class='section'>
            <div class='section-title'>Thông tin khách hàng</div>
            <div class='info-row'>
                <span class='label'>Khách hàng:</span>
                <span class='value'>" + customerName + @"</span>
            </div>
            <div class='info-row'>
                <span class='label'>Số điện thoại:</span>
                <span class='value'>" + customerPhone + @"</span>
            </div>
            <div class='info-row'>
                <span class='label'>Hạng thành viên:</span>
                <span class='value'>" + membershipLevel + @"</span>
            </div>
        </div>
        
        <div class='section'>
            <div class='section-title'>Chi tiết đơn hàng #" + orderId + @"</div>
            <div class='items'>");

            // Add order items
            if (orderDetails != null)
            {
                foreach (DataRow row in orderDetails.Rows)
                {
                    string name = row["Tên Sản Phẩm"].ToString();
                    string qty = row["Số Lượng"].ToString();
                    decimal price = Convert.ToDecimal(row["Thành Tiền (Tạm tính)"]);
                    
                    html.AppendLine($@"
                <div class='item'>
                    <span class='item-name'>{name}</span>
                    <span class='item-qty'>x{qty}</span>
                    <span class='item-price'>{price:N0}đ</span>
                </div>");
                }
            }

            html.AppendLine(@"
            </div>
        </div>
        
        <div class='totals'>
            <div class='total-row'>
                <span class='label'>Tổng tiền hàng:</span>
                <span class='value'>" + totalAmount.ToString("N0") + @"đ</span>
            </div>");

            if (discountRate > 0)
            {
                html.AppendLine($@"
            <div class='total-row discount'>
                <span class='label'>Giảm giá ({discountRate * 100:0}%):</span>
                <span class='value'>-{discountValue:N0}đ</span>
            </div>");
            }

            html.AppendLine(@"
            <div class='total-row final'>
                <span class='label'>THANH TOÁN:</span>
                <span class='value'>" + discountedAmount.ToString("N0") + @"đ</span>
            </div>
        </div>
        
        <div class='payment-info'>
            <div class='payment-row'>
                <span class='label'>Phương thức:</span>
                <span class='value'>" + paymentMethod + @"</span>
            </div>
            <div class='payment-row'>
                <span class='label'>Tiền khách đưa:</span>
                <span class='value'>" + paymentMoney.ToString("N0") + @"đ</span>
            </div>
            <div class='payment-row'>
                <span class='label'>Tiền thối:</span>
                <span class='value'>" + changeAmount.ToString("N0") + @"đ</span>
            </div>
        </div>
        
        <div class='footer'>
            <p class='thank-you'>✨ Cảm ơn quý khách! ✨</p>
            <p>Ngày: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + @"</p>
            <p>Thu ngân: " + cashierName + @"</p>
            <p>---</p>
            <p>Hotline: 1900-xxxx | www.petcarex.vn</p>
        </div>
        
        <button class='print-btn' onclick='window.print()'>🖨️ In hóa đơn / Lưu PDF</button>
    </div>
</body>
</html>");

            // Save to temp file
            string tempPath = Path.Combine(Path.GetTempPath(), $"Invoice_{invoiceId}.html");
            File.WriteAllText(tempPath, html.ToString(), Encoding.UTF8);
            
            return tempPath;
        }

        /// <summary>
        /// Mở hóa đơn trong trình duyệt
        /// </summary>
        public static void OpenInvoice(string filePath)
        {
            try
            {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể mở hóa đơn: " + ex.Message);
            }
        }
    }
}
