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
using Cashflow9000.Adapters;

namespace Cashflow9000
{
    public enum LayoutId
    {
        BudgetActivity,
        BudgetListActivity,

        CategoryActivity,
        CategoryListActivity,

        MilestoneActivity,
        MilestoneListActivity,

        PlannedTransactionActivity,
        PlannedTransactionListActivity,

        TransactionActivity,
        TransactionListActivity
    }

    public static class FragmentUtil
    {
        public static void LoadFragment(Activity activity, int id, Fragment fragment)
        {
            FragmentTransaction ft = activity.FragmentManager.BeginTransaction();
            ft.Replace(id, fragment);
            ft.SetTransition(FragmentTransit.FragmentFade);
            ft.Commit();
        }
    }
}