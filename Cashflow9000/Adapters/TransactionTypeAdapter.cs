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
    class TransactionTypeAdapter : BaseAdapter<TransactionType>
    {
        private readonly Activity Context;
        private static readonly Dictionary<TransactionType, int> Mapping = new Dictionary<TransactionType, int>
        {
            { TransactionType.Expense, Resource.String.expense },
            { TransactionType.Income, Resource.String.income }
        };

        public List<TransactionType> TransactionTypes { get; }


        public TransactionTypeAdapter(Activity context)
        {
            Context = context;
            TransactionTypes = new List<TransactionType> { TransactionType.Income, TransactionType.Expense };
        }

        public override TransactionType this[int position] => TransactionTypes[position];
        public override long GetItemId(int position) => position;
        public override int Count => TransactionTypes.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for position
            TransactionType item = TransactionTypes[position];

            var view = (convertView ??
                        Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false)) as TextView;

            view?.SetText(Mapping[item], TextView.BufferType.Normal);

            //Finally return the view
            return view;
        }
    }
}