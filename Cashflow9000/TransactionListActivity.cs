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
    [Activity(Label = "TransactionListActivity")]
    public class TransactionListActivity : Activity, ListFragment.IListListener, TransactionFragment.ITransactionFragmentListener
    {
        private ListFragment Fragment;
        private bool ItemExists;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListActivity);

            Fragment = new ListFragment(Resource.String.transaction, new TransactionAdapter(this, CashflowData.Transactions));
            FragmentUtil.LoadFragment(this, Resource.Id.containerList, Fragment);

            ItemExists = FindViewById(Resource.Id.containerItem) != null;
        }

        protected override void OnResume()
        {
            base.OnResume();
            Fragment.SetAdapter(new TransactionAdapter(this, CashflowData.Transactions));
        }
        
        public void OnAdd()
        {
            if (ItemExists)
            {
                FragmentUtil.LoadFragment(this, Resource.Id.containerItem, new TransactionFragment());
            }
            else
            {
                StartActivity(new Intent(this, typeof(TransactionActivity)));
            }
        }

        public void OnSelect(long id)
        {
            if (ItemExists)
            {
                FragmentUtil.LoadFragment(this, Resource.Id.containerItem, new TransactionFragment((int)id));
            }
            else
            {
                Intent i = new Intent(this, typeof(TransactionActivity));
                i.PutExtra(TransactionActivity.ExtraTransactionId, (int)id);
                StartActivity(i);
            }
        }

        public void TransactionSaved(Transaction transaction)
        {
            CashflowData.InsertOrReplace(transaction);
            Fragment.SetAdapter(new TransactionAdapter(this, CashflowData.Transactions));
        }
    }
}