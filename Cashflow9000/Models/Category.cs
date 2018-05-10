using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Cashflow9000
{
    public class Category
    {
        public string Name { get; set; }
        public TransactionType Type { get; set; }
    }
}