using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Fragments;
using Cashflow9000.Models;
using Newtonsoft.Json;

namespace Cashflow9000
{

    [Activity(Label = "Transaction")]
    public class TransactionActivity : Activity, TransactionFragment.ITransactionFragmentListener
    {
        public const string ExtraTransactionId = "TransactionActivity.TransactionId";

        // use case for both category and milestone is paying off a mortgage, it is both a recurring budget item and a long term goal

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Init view
            base.OnCreate(savedInstanceState);
            FrameLayout layout = new FrameLayout(this);
            layout.Id = LayoutId.TransactionActivity;
            SetContentView(layout);

            TransactionFragment fragment = new TransactionFragment(Intent.GetIntExtra(ExtraTransactionId, -1));
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            ft.Replace(layout.Id, fragment);
            ft.SetTransition(FragmentTransit.FragmentFade);
            ft.Commit();
        }

        public void TransactionSaved(Transaction transaction)
        {
            CashflowData.InsertOrReplace(transaction);
            Finish();
        }
    }
}