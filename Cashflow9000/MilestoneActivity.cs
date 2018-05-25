using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Fragments;
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "MilestoneActivity")]
    public class MilestoneActivity : Activity, MilestoneFragment.IMilestoneFragmentListener
    {
        public const string ExtraMilestoneId = "MilestoneActivity.ExtraMilestoneId";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            FragmentUtil.LoadFragment(this, LayoutId.MilestoneActivity, new MilestoneFragment(Intent.GetIntExtra(ExtraMilestoneId, -1)));
        }

        public void OnSave(Milestone milestone)
        {
            CashflowData.InsertOrReplace(milestone);
            Finish();
        }
    }
}