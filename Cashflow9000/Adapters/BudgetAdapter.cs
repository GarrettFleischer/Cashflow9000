using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Icu.Text;
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

        public BudgetAdapter(Activity context, RecurrenceType type, DateTime date)
        {
            Context = context;

            Budgets = CashflowData.Budgets.Where(b => b.Recurrence?.Type == type).ToList();

            switch (type)
            {
                case RecurrenceType.Daily:
                    Transactions = CashflowData.Transactions.Where(t => t.Date.Day == date.Day);
                    break;
                case RecurrenceType.Weekly:
                case RecurrenceType.Biweekly: // TODO fix this to actually be biweekly
                    Transactions = CashflowData.Transactions.Where(t => AreFallingInSameWeek(t.Date, date, DayOfWeek.Monday));
                    break;
                case RecurrenceType.Monthly:
                    Transactions = CashflowData.Transactions.Where(t => t.Date.Month == date.Month);
                    break;
                case RecurrenceType.Quarterly:
                    Transactions = CashflowData.Transactions.Where(t => GetQuarter(t.Date) == GetQuarter(date));
                    break;
                case RecurrenceType.Annually:
                    Transactions = CashflowData.Transactions.Where(t => t.Date.Year == date.Year);
                    break;
                default:
                    Transactions = CashflowData.Transactions;
                    break;
            }
            
        }

        private static bool AreFallingInSameWeek(DateTime date1, DateTime date2, DayOfWeek weekStartsOn)
        {
            return date1.AddDays(-GetOffsetedDayofWeek(date1.DayOfWeek, (int)weekStartsOn)) == date2.AddDays(-GetOffsetedDayofWeek(date2.DayOfWeek, (int)weekStartsOn));
        }

        private static int GetOffsetedDayofWeek(DayOfWeek dayOfWeek, int offsetBy)
        {
            return (((int) dayOfWeek - offsetBy + 7) % 7);
        }

        private static int GetQuarter(DateTime date)
        {
            if (date.Month >= 4 && date.Month <= 6)
                return 1;
            if (date.Month >= 7 && date.Month <= 9)
                return 2;
            if (date.Month >= 10 && date.Month <= 12)
                return 3;
            return 4;
        }

        public override Budget this[int position] => Budgets[position];
        public override long GetItemId(int position) => Budgets[position].Id ?? -1;
        public override int Count => Budgets.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for position
            Budget item = Budgets[position];
            double total = (double)item.Amount;
            double balance = -(double)Transactions.Where(t => t.CategoryId == item.CategoryId).Sum(t => t.Value);

            View view = convertView ??
                        Context.LayoutInflater.Inflate(Resource.Layout.RatioListItem, parent, false);

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