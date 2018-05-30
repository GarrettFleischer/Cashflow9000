using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using ListFragment = Cashflow9000.Fragments.ListFragment;

namespace Cashflow9000
{
    [Activity(Label = "MilestoneListActivity")]
    public class MilestoneListActivity : Activity, ListFragment.IListListener
    {
        private ListFragment Fragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Fragment = new ListFragment(Resource.String.milestone, new MilestoneAdapter(this,false));
            //FragmentUtil.LoadFragment(this, LayoutId.MilestoneListActivity, Fragment);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Fragment.SetAdapter(new MilestoneAdapter(this, false));
        }

        public void OnAdd()
        {
            StartActivity(new Intent(this, typeof(MilestoneActivity)));
        }

        public void OnSelect(long id)
        {
            Intent i = new Intent(this, typeof(MilestoneActivity));
            i.PutExtra(MilestoneActivity.ExtraMilestoneId, (int)id);
            StartActivity(i);
        }
    }
}