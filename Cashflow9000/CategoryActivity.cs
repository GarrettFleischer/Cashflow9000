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
    public class CategoryActivity : ItemActivity<Category>
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

        public void ItemSaved(Category item)
        {
            CashflowData.InsertOrReplace(item);
            Finish();
        }

        public void ItemDeleted(Category item)
        {
            CashflowData.Delete(item);
            Finish();
        }
    }
}