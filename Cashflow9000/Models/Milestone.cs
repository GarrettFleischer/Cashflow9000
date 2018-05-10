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
using TeixeiraSoftware.Finance;

namespace Cashflow9000.Models
{
    public class Milestone
    {
        public string Name { get; set; }
        public Money Amount { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}