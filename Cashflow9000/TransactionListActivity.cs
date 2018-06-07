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
    [Activity(Label = "TransactionListFragmentActivity")]
    public class TransactionListFragmentActivity : ListFragmentActivity<TransactionActivity>, TransactionFragment.ITransactionFragmentListener
    {
        protected override int GetTitleId()
        {
            return Resource.String.transaction;
        }

        protected override IListAdapter GetListAdapter()
        {
            return new TransactionAdapter(this, CashflowData.Transactions.OrderBy(t => t.Date).Reverse().ToList());
        }

        protected override Fragment GetItemFragment(int id = -1)
        {
            return new TransactionFragment(id);
        }

        protected override string GetExtraId()
        {
            return TransactionActivity.ExtraTransactionId;
        }

        public void TransactionSaved(Transaction transaction)
        {
            CashflowData.InsertOrReplace(transaction);
            UpdateListAdapter();
        }

        public void TransactionDeleted(Transaction transaction)
        {
            CashflowData.Delete(transaction);
            UpdateListAdapter();
        }
    }
}