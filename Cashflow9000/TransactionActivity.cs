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
    public class TransactionActivity : ItemActivity, TransactionFragment.ITransactionFragmentListener
    {
        public const string ExtraTransactionId = "TransactionActivity.TransactionId";
        
        protected override Fragment GetFragment(int id)
        {
            return new TransactionFragment(id);
        }

        protected override string GetExtraId()
        {
            return ExtraTransactionId;
        }

        public void TransactionSaved(Transaction transaction)
        {
            CashflowData.InsertOrReplace(transaction);
            Finish();
        }
    }
}