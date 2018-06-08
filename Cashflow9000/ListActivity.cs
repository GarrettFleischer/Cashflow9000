using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Fragments;
using ListFragment = Cashflow9000.Fragments.ListFragment;

namespace Cashflow9000
{
    public abstract class ListFragmentActivity<TActivity, TItem> : Activity, ListFragment.IListFragmentListener, IItemFragmentListener<TItem>
    {
        private ListFragment ListFragment;
        private bool Tablet;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListActivity);

            Tablet = FindViewById(Resource.Id.containerItem) != null;

            ListFragment = new ListFragment(GetTitleId(), GetListAdapter());
            FragmentUtil.LoadFragment(this, Resource.Id.containerList, ListFragment);
        }

        public override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            ListFragment.SetHeaderFragment(GetHeaderFragment());
        }

        protected override void OnResume()
        {
            base.OnResume();
            ListFragment.SetAdapter(GetListAdapter());
        }

        public void OnAdd()
        {
            if (Tablet)
                FragmentUtil.LoadFragment(this, Resource.Id.containerItem, GetItemFragment());
            else
                StartActivity(typeof(TActivity));
        }

        public void OnSelect(long id)
        {
            if (Tablet)
            {
                FragmentUtil.LoadFragment(this, Resource.Id.containerItem, GetItemFragment((int)id));
            }
            else
            {
                Intent i = new Intent(this, typeof(TActivity));
                i.PutExtra(GetExtraId(), (int)id);
                StartActivity(i);
            }
        }

        protected void UpdateListAdapter()
        {
           ListFragment.SetAdapter(GetListAdapter());
        }

        protected virtual Fragment GetHeaderFragment()
        {
            return null;
        }

        protected abstract int GetTitleId();

        protected abstract IListAdapter GetListAdapter();

        protected abstract Fragment GetItemFragment(int id = -1);

        protected abstract string GetExtraId();

        public void ItemSaved(TItem item)
        {
            CashflowData.InsertOrReplace(item);
            UpdateListAdapter();
        }

        public void ItemDeleted(TItem item)
        {
            CashflowData.Delete(item);
            UpdateListAdapter();
        }
    }
}