using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace Cashflow9000
{
    [Activity(Label = "Cashflow9000", MainLauncher = true, Theme = "@style/MyTheme")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            CashflowData.Initialize();

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "My Toolbar";

            Button transaction = FindViewById<Button>(Resource.Id.buttonTransaction);
            transaction.Click += delegate
            {
                Intent i = new Intent(this, typeof(TransactionListActivity));
                StartActivity(i);
            };

            Button milestone = FindViewById<Button>(Resource.Id.buttonMilestone);
            milestone.Click += delegate
            {
                Intent i = new Intent(this, typeof(MilestoneListActivity));
                StartActivity(i);
            };

            Button categories = FindViewById<Button>(Resource.Id.buttonCategories);
            categories.Click += delegate
            {
                Intent i = new Intent(this, typeof(CategoryListActivity));
                StartActivity(i);
            };

            Button budgets = FindViewById<Button>(Resource.Id.buttonBudgets);
            budgets.Click += delegate
            {
                Intent i = new Intent(this, typeof(BudgetListActivity));
                StartActivity(i);
            };
        }
    }
}

