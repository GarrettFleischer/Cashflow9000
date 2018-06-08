using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Fragments;
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "CategoryListFragmentActivity")]
    public class CategoryListFragmentActivity : ListFragmentActivity<CategoryActivity, Category>
    {
        protected override int GetTitleId()
        {
            return Resource.String.category;
        }

        protected override IListAdapter GetListAdapter()
        {
            return new CategoryAdapter(this, TransactionType.Any, false);
        }

        protected override Fragment GetItemFragment(int id = -1)
        {
            return new CategoryFragment(id);
        }

        protected override string GetExtraId()
        {
            return CategoryActivity.ExtraCategoryId;
        }
    }
}