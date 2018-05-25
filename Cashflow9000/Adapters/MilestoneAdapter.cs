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
    class MilestoneAdapter : BaseAdapter<Milestone>
    {
        private readonly Activity Context;
        public List<Milestone> Milestones { get; }

        public MilestoneAdapter(Activity context, bool spinner)
        {
            Context = context;
            Milestones = CashflowData.Milestones.OrderBy(x => x.Name).ToList();
            if (spinner) Milestones.Insert(0, null);
        }

        public override Milestone this[int position] => Milestones[position];
        public override long GetItemId(int position) => Milestones[position]?.Id ?? -1;
        public override int Count => Milestones.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView view = (convertView ??
                        Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false)) as TextView;

            view?.SetText(Milestones[position]?.ToString() ?? "", TextView.BufferType.Normal);

            //Finally return the view
            return view;
        }
    }
}