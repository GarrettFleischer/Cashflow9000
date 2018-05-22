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
using Cashflow9000.Models;

namespace Cashflow9000.Adapters
{
    public class TransactionAdapter : BaseAdapter<Transaction>
    {
        private readonly Activity Context;
        private readonly List<Transaction> Transactions;

        public TransactionAdapter(Activity context, List<Transaction> transactions)
        {
            Context = context;
            Transactions = transactions;
        }

        public override Transaction this[int position] => Transactions[position];
        public override long GetItemId(int position) => Transactions[position].Id ?? -1;
        public override int Count => Transactions.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for position
            Transaction item = Transactions[position];

            TextView view = (convertView ??
                        Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false)) as TextView;

            view?.SetText(item.ToString(), TextView.BufferType.Normal);

            //Finally return the view
            return view;
        }
    }
}