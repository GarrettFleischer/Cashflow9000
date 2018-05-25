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
    public class RecurrenceAdapter : BaseAdapter<Recurrence>
    {
        private readonly Activity Context;
        public List<Recurrence> Recurrences { get; }

        public RecurrenceAdapter(Activity context, bool spinner)
        {
            Context = context;
            Recurrences = CashflowData.Recurrences;
            if (spinner) Recurrences.Insert(0, null);
        }

        public override Recurrence this[int position] => Recurrences[position];
        public override long GetItemId(int position) => Recurrences[position].Id ?? -1;
        public override int Count => Recurrences.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView view = (convertView ??
                        Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false)) as TextView;

            view?.SetText(Context.Resources.GetString(Recurrences[position]?.StringId ?? -1), TextView.BufferType.Normal);

            return view;
        }
    }
}