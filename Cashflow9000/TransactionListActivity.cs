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
using ListFragment = Cashflow9000.Fragments.ListFragment;

namespace Cashflow9000
{
    [Activity(Label = "TransactionListActivity")]
    public class TransactionListActivity : Activity, ListFragment.IListListener
    {
        private ListFragment Fragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Fragment = new ListFragment(Resource.String.transaction, new TransactionAdapter(this, CashflowData.Transactions));
            FragmentUtil.LoadFragment(this, LayoutId.TransactionListActivity, Fragment);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Fragment.SetAdapter(new TransactionAdapter(this, CashflowData.Transactions));
        }
        
        public void OnAdd()
        {
            StartActivity(new Intent(this, typeof(TransactionActivity)));
        }

        public void OnSelect(long id)
        {
            Intent i = new Intent(this, typeof(TransactionActivity));
            i.PutExtra(TransactionActivity.ExtraTransactionId, (int)id);
            StartActivity(i);
        }
    }
}