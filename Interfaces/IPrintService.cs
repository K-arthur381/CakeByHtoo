using CakeByHtoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Interfaces
{
    public interface IPrintService
    {
        void PrintReceipt(OrderData orderdata);
        string GenerateReceiptHtml(OrderData orderdata);
    }
}
