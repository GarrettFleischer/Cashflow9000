using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Fragments;
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "CategoryActivity")]
    public class CategoryActivity : ItemActivity, CategoryFragment.ICategoryFragmentListener
    {
        public const string ExtraCategoryId = "CategoryActivity.ExtraCategoryId";
        
        protected override Fragment GetFragment(int id)
        {
            return new CategoryFragment(id);
        }

        protected override string GetExtraId()
        {
            return ExtraCategoryId;
        }

        public void CategorySaved(Category category)
        {
            CashflowData.InsertOrReplace(category);
            Finish();
        }
    }
}