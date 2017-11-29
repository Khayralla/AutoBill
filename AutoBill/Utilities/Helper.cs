using AutoBill.Models;

namespace AutoBill.Utilities
{
    public static class Helper
    {
        public static string GetHtml(Customer customer, Car car, Car carTradeWith, Insurance insurance, SaleBill saleBill)
        {
            var head = @"
<!DOCTYPE html>
<html>
<head>
  <title>Bill of Sale</title>
  <style>
    #left {
      width: 50%;
      float: left;
    }
    #right {
      margin-left: 50%
    }
  </style>
</head>
";

            var body =
$@"
<body>
    <div id=\""container\"">
        <div id=\""left\"">
            <table style=\""width: 100 % \"">
                <tr>
                    <td>First Name:</td>
                    <td>{customer.FirstName}</td>
                </tr>
                <tr>
                    <td>Last Name:</td>
                    <td>{customer.LastName}</td>
                </tr>
                <tr>
                    <td>Address:</td>
                    <td>{customer.Address}</td>
                </tr>
                <tr>
                    <td>VIN:</td>
                    <td>{car.VIN}</td>
                </tr>
            </table>
        </div>
        <div id=\""right\"">
            <table style=\""width: 100 % \"">
                <tr>
                    <td>Price:</td>
                    <td>{saleBill.Price}</td>
                </tr>
                <tr>
                    <td>Tax:</td>
                    <td>{saleBill.Tax}</td>
                </tr>
                <tr>
                    <td>Total:</td>
                    <td>{saleBill.Total}</td>
                </tr>
            </table>
        </div>
    </div>
</body>
</ html >
";
            return $"{head} {body}";
        }


        /*
         
Server Name     SMTP Address            Port    SSL
Yahoo!          smtp.mail.yahoo.com     587     Yes
GMail           smtp.gmail.com          587     Yes
Hotmail         smtp.live.com           587     Yes
         
         */

    }
}
