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

namespace Cashflow9000
{
    [Activity(Label = "CategoryActivity")]
    public class CategoryActivity : Activity
    {
        private Category Category;

        public const string ExtraTransactionId = "TransactionActivity.TransactionId";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Load data from intent
            int id = Intent.GetIntExtra(ExtraTransactionId, -1);
            Category = ((id == -1) ? new Category() : CashflowData.Category(id));

            // Find UI views
        }
    }
}