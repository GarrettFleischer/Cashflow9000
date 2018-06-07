using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cashflow9000.Models;
using Debug = System.Diagnostics.Debug;

namespace Cashflow9000.Adapters
{
    public class TransactionAdapter : BaseAdapter<Transaction>
    {
        private readonly Activity Context;
        private readonly List<Transaction> Transactions;
        private readonly List<int?> WithSeparators;
        private readonly int? SeparatorId = Int32.MinValue;

        public TransactionAdapter(Activity context, List<Transaction> transactions)
        {
            Context = context;
            Transactions = transactions;
            WithSeparators = new List<int?>();
            for (int i = 0; i < Transactions.Count; ++i)
            {
                if (i == 0)
                {
                    WithSeparators.Add(SeparatorId);
                }
                else if (i < Transactions.Count)
                {
                    DateTime before = Transactions[i - 1].Date.Date;
                    DateTime after = Transactions[i].Date.Date;
                    if (before != after)
                        WithSeparators.Add(SeparatorId);
                }

                WithSeparators.Add(Transactions[i].Id);
            }
        }

        private bool IsSeparator(int position) => WithSeparators[position] == SeparatorId;
        private Transaction GetTransaction(int position) => IsSeparator(position) ? null : Transactions.Single(t => t.Id == WithSeparators[position]);

        public override Transaction this[int position] => GetTransaction(position);
        public override long GetItemId(int position) => GetTransaction(position)?.Id ?? -1;
        public override int Count => WithSeparators.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView view = (convertView ??
                             Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false)) as TextView;

            Debug.Assert(view != null, nameof(view) + " != null");

            Transaction item = GetTransaction(position);

            if (item == null)
            {
                item = GetTransaction(position + 1);
                view.Text = item?.Date.ToShortDateString() ?? "";
                view.SetTextColor(Color.White);
            }
            else
            {
                view.Text = item.ToString();
                view.SetTextColor(item.Type == TransactionType.Income ? Color.Green : Color.Red);
            }

            return view;
        }
    }
}