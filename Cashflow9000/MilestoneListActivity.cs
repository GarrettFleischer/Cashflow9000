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
using Cashflow9000.Fragments;
using Cashflow9000.Models;
using ListFragment = Cashflow9000.Fragments.ListFragment;

namespace Cashflow9000
{
    [Activity(Label = "MilestoneListFragmentActivity")]
    public class MilestoneListFragmentActivity : ListFragmentActivity<MilestoneActivity, Milestone>
    {
        protected override int GetTitleId()
        {
            return Resource.String.milestone;
        }

        protected override IListAdapter GetListAdapter()
        {
            return new MilestoneAdapter(this, false);
        }

        protected override Fragment GetItemFragment(int id = -1)
        {
            return new MilestoneFragment(id);
        }

        protected override string GetExtraId()
        {
            return MilestoneActivity.ExtraMilestoneId;
        }
    }
}