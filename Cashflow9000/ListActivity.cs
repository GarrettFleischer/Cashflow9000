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
using ListFragment = Cashflow9000.Fragments.ListFragment;

namespace Cashflow9000
{
    public abstract class ListActivity<T> : Activity, ListFragment.IListListener
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
                StartActivity(typeof(T));
        }

        public void OnSelect(long id)
        {
            if (Tablet)
            {
                FragmentUtil.LoadFragment(this, Resource.Id.containerItem, GetItemFragment((int)id));
            }
            else
            {
                Intent i = new Intent(this, typeof(T));
                i.PutExtra(GetExtraId(), (int)id);
                StartActivity(i);
            }
        }

        protected void UpdateListAdapter()
        {
           ListFragment.SetAdapter(GetListAdapter());
        }

        protected abstract int GetTitleId();

        protected abstract IListAdapter GetListAdapter();

        protected abstract Fragment GetItemFragment(int id = -1);

        protected abstract string GetExtraId();
    }
}