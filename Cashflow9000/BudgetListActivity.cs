using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Fragments;
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "BudgetListActivity")]
    public class BudgetListActivity : ListActivity<BudgetActivity>, BudgetFragment.IBudgetFragmentListener
    {
        protected override int GetTitleId()
        {
            return Resource.String.budget;
        }

        protected override IListAdapter GetListAdapter()
        {
            return new BudgetAdapter(this, CashflowData.Recurrences.Single(r => r.Type == RecurrenceType.Monthly));
        }

        protected override Fragment GetItemFragment(int id = -1)
        {
            return new BudgetFragment(id);
        }

        protected override string GetExtraId()
        {
            return BudgetActivity.ExtraBudgetId;
        }

        public void BudgetSaved(Budget budget)
        {
            CashflowData.InsertOrReplace(budget);
            UpdateListAdapter();
        }
    }
}