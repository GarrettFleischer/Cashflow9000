﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cashflow9000.Fragments;
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "PlannedTransactionActivity")]
    public class PlannedTransactionActivity : Activity, PlannedTransactionFragment.IPlannedTransactionListener
    {
        public const string ExtraPlannedPaymentId = "PlannedTransactionActivity.ExtraPlannedPaymentId";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            FrameLayout layout = new FrameLayout(this);
            layout.Id = LayoutId.PlannedTransactionActivity;
            SetContentView(layout);

            PlannedTransactionFragment fragment = new PlannedTransactionFragment(Intent.GetIntExtra(ExtraPlannedPaymentId, -1));
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            ft.Replace(layout.Id, fragment);
            ft.SetTransition(FragmentTransit.FragmentFade);
            ft.Commit();
        }

        public void PlannedPaymentSaved(PlannedTransaction plannedTransaction)
        {
            CashflowData.InsertOrReplace(plannedTransaction);
            Finish();
        }
    }
}