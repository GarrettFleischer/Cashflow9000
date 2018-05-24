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
    class PlannedTransactionAdapter : BaseAdapter<PlannedTransaction>
    {
        private readonly Activity Context;
        public List<PlannedTransaction> PlannedTransactions { get; }

        public PlannedTransactionAdapter(Activity context)
        {
            Context = context;
            PlannedTransactions = CashflowData.PlannedTransactions;
        }

        public override PlannedTransaction this[int position] => PlannedTransactions[position];
        public override long GetItemId(int position) => PlannedTransactions[position].Id ?? -1;
        public override int Count => PlannedTransactions.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for position
            PlannedTransaction item = PlannedTransactions[position];

            TextView view = (convertView ??
                             Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false)) as TextView;

            view?.SetText(item.ToString(), TextView.BufferType.Normal);

            //Finally return the view
            return view;
        }
    }
}