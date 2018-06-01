using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cashflow9000.Models;

namespace Cashflow9000.Adapters
{
    public class BudgetAdapter : BaseAdapter<Budget>
    {

        private readonly Activity Context;
        public List<Budget> Budgets { get; }
        private readonly IEnumerable<Transaction> Transactions;

        public BudgetAdapter(Activity context, Recurrence recurrence)
        {
            Context = context;

            // TODO switch based on recurrence type
            Budgets = CashflowData.Budgets;
            Transactions = CashflowData.Transactions.Where(t => t.Date.Month == DateTime.Today.Month);
        }

        public override Budget this[int position] => Budgets[position];
        public override long GetItemId(int position) => Budgets[position].Id ?? -1;
        public override int Count => Budgets.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for position
            Budget item = Budgets[position];
            double total = (double) item.Amount;
            var transactions = Transactions.Where(t => t.CategoryId == item.CategoryId);

            double balance = (double)transactions.Sum(t => t.Amount);

            View view = convertView ??
                        Context.LayoutInflater.Inflate(Resource.Layout.BudgetListItem, parent, false);

            TextView textName = view.FindViewById<TextView>(Resource.Id.textName);
            TextView textRatio = view.FindViewById<TextView>(Resource.Id.textRatio);
            ProgressBar progressTotal = view.FindViewById<ProgressBar>(Resource.Id.progressTotal);

            textName.Text = item.Name;
            textRatio.Text = $"{NumberFormat.CurrencyInstance.Format(balance)}/{NumberFormat.CurrencyInstance.Format(total)}";
            progressTotal.Progress = (int)((balance / total) * 100);

            return view;
        }
    }

}