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
using Cashflow9000.Fragments;

namespace Cashflow9000
{
    public abstract class ItemActivity<T> : Activity, IItemFragmentListener<T>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ItemActivity);

            FragmentUtil.LoadFragment(this, Resource.Id.container, GetFragment(Intent.GetIntExtra(GetExtraId(), -1)));
        }

        protected abstract Fragment GetFragment(int id);

        protected abstract string GetExtraId();
        public void ItemSaved(T item)
        {
            CashflowData.InsertOrReplace(item);
            Finish();
        }

        public void ItemDeleted(T item)
        {
            CashflowData.Delete(item);
            Finish();
        }
    }
}