using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Fragments;
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "BudgetActivity")]
    public class BudgetActivity : ItemActivity<Budget>
    {
        public const string ExtraBudgetId = "BudgetActivity.ExtraBudgetId";
        
        protected override Fragment GetFragment(int id)
        {
            return new BudgetFragment(id);
        }

        protected override string GetExtraId()
        {
            return ExtraBudgetId;
        }
    }
}