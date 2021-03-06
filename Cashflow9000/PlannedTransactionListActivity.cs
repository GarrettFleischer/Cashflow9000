﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Fragments;
using Cashflow9000.Models;
using ListFragment = Cashflow9000.Fragments.ListFragment;

namespace Cashflow9000
{
    [Activity(Label = "PlannedTransactionListFragmentActivity")]
    public class PlannedTransactionListFragmentActivity : ListFragmentActivity<PlannedTransactionActivity, PlannedTransaction>
    {
        protected override int GetTitleId()
        {
            return Resource.String.plannedTransaction;
        }

        protected override IListAdapter GetListAdapter()
        {
            return new PlannedTransactionAdapter(this);
        }

        protected override Fragment GetItemFragment(int id = -1)
        {
            return new PlannedTransactionFragment(id);
        }

        protected override string GetExtraId()
        {
            return PlannedTransactionActivity.ExtraPlannedPaymentId;
        }
    }
}