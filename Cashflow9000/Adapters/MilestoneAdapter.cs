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
    public class MilestoneAdapter : BaseAdapter<Milestone>
    {
        private readonly Activity Context;
        public List<Milestone> Milestones { get; }

        private readonly bool Spinner;

        private readonly IEnumerable<Transaction> Transactions;

        public MilestoneAdapter(Activity context, bool spinner)
        {
            Context = context;
            Spinner = spinner;
            Transactions = CashflowData.Transactions;
            Milestones = CashflowData.Milestones.OrderBy(x => x.Name).ToList();
            if (Spinner) Milestones.Insert(0, null);
        }

        public override Milestone this[int position] => Milestones[position];
        public override long GetItemId(int position) => Milestones[position]?.Id ?? -1;
        public override int Count => Milestones.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Milestone item = Milestones[position];
            int layoutId = Spinner ? Android.Resource.Layout.SimpleListItem1 : Resource.Layout.RatioListItem;
            View view = convertView ??
                        Context.LayoutInflater.Inflate(layoutId, parent, false);

            if (Spinner)
            {
                (view as TextView)?.SetText(item?.ToString(), TextView.BufferType.Normal);
            }
            else
            {
                if (item == null) return view;

                double total = (double)item.Amount;
                double balance = -(double)Transactions.Where(t => t.MilestoneId == item.Id).Sum(t => t.Value);

                TextView textName = view.FindViewById<TextView>(Resource.Id.textName);
                TextView textRatio = view.FindViewById<TextView>(Resource.Id.textRatio);
                ProgressBar progressTotal = view.FindViewById<ProgressBar>(Resource.Id.progressTotal);

                textName.Text = item.Name;
                textRatio.Text =
                    $"{NumberFormat.CurrencyInstance.Format(balance)}/{NumberFormat.CurrencyInstance.Format(total)}";
                progressTotal.Progress = (int)((balance / total) * 100);
            }

            return view;
        }
    }
}