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
        private readonly List<Milestone> Milestones;

        public MilestoneAdapter(Activity context, List<Milestone> milestones)
        {
            Context = context;
            Milestones = milestones;
        }

        public override Milestone this[int position] => Milestones[position];
        public override long GetItemId(int position) => Milestones[position].Id ?? -1;
        public override int Count => Milestones.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get our object for position
            Milestone item = Milestones[position];

            var view = (convertView ??
                        Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false)) as TextView;

            view?.SetText(item.ToString(), TextView.BufferType.Normal);

            //Finally return the view
            return view;
        }
    }
}