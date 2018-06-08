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
using Cashflow9000.Fragments;
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "PlannedTransactionActivity")]
    public class PlannedTransactionActivity : ItemActivity<PlannedTransaction>
    {
        public const string ExtraPlannedPaymentId = "PlannedTransactionActivity.ExtraPlannedPaymentId";

        protected override Fragment GetFragment(int id)
        {
            return new PlannedTransactionFragment(id);
        }

        protected override string GetExtraId()
        {
            return ExtraPlannedPaymentId;
        }
    }
}