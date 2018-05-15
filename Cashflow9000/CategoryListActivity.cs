using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "CategoryListActivity")]
    public class CategoryListActivity : ListActivity
    {
        private long Selected = -1;

        public const string ExtraSelected = "CategoryListActivity.ExtraSelected";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var saveButton = new Button(this);
            saveButton.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            saveButton.SetText(Resource.String.save);
            saveButton.Click += SaveButtonOnClick;

            var layout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            layout.SetGravity(GravityFlags.Right);
            layout.AddView(saveButton);

            ListView.AddHeaderView(layout);

            ListAdapter = new CategoryAdapter(this, CashflowData.Categories.OrderBy(x => x.Name).ToList());
            ListView.ItemClick += ListViewOnItemClick;
        }

        private void SaveButtonOnClick(object sender, EventArgs eventArgs)
        {
            var data = new Intent();
            data.PutExtra(ExtraSelected, Selected);
            SetResult(Result.Ok, data);
            Finish();
        }

        private void ListViewOnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Selected = e.Id;
        }
    }
}