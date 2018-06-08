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
    public class MilestoneActivity : ItemActivity<Milestone>
    {
        public const string ExtraMilestoneId = "MilestoneActivity.ExtraMilestoneId";

        protected override Fragment GetFragment(int id)
        {
            return new MilestoneFragment(id);
        }

        protected override string GetExtraId()
        {
            return ExtraMilestoneId;
        }
    }
}