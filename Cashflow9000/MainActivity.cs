using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace Cashflow9000
{
    [Activity(Label = "Cashflow9000", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            CashflowData.Initialize();

            //Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //SetActionBar(toolbar);
            //ActionBar.Title = "My Toolbar";

            UpdateBalance();

            Button transaction = FindViewById<Button>(Resource.Id.buttonTransaction);
            transaction.Click += delegate { StartActivityType(typeof(TransactionListFragmentActivity)); };

            Button plannedTransactions = FindViewById<Button>(Resource.Id.buttonPlannedTransactions);
            plannedTransactions.Click += delegate { StartActivityType(typeof(PlannedTransactionListFragmentActivity)); };

            Button budgets = FindViewById<Button>(Resource.Id.buttonBudgets);
            budgets.Click += delegate { StartActivityType(typeof(BudgetListFragmentActivity)); };

            Button milestone = FindViewById<Button>(Resource.Id.buttonMilestone);
            milestone.Click += delegate { StartActivityType(typeof(MilestoneListFragmentActivity)); };

            Button categories = FindViewById<Button>(Resource.Id.buttonCategories);
            categories.Click += delegate { StartActivityType(typeof(CategoryListFragmentActivity)); };
        }

        void StartActivityType(Type type)
        {
            Intent i = new Intent(this, type);
            StartActivity(i);
        }

        protected override void OnResume()
        {
            base.OnResume();
            UpdateBalance();
        }

        void UpdateBalance()
        {
            TextView balance = FindViewById<TextView>(Resource.Id.textBalance);
            balance.Text = NumberFormat.CurrencyInstance.Format((double)CashflowData.Transactions.Sum(x => x.Value));
        }
    }
}

