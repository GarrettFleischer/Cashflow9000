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
using Cashflow9000.Models;
using TeixeiraSoftware.Finance;

namespace Cashflow9000
{
    public enum TransactionType { Expense, Income }

    public class Transaction
    {
        public Money Amount { get; set; }
        public TransactionType Type { get; set; }
        public Category Category { get; set; }
        public Milestone Milestone { get; set; }
        public string Note { get; set; }
    }
}