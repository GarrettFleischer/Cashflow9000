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
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "CategoryListActivity")]
    public class CategoryListActivity : ListActivity
    {
        private long Selected = -1;
        private bool SelectionEnabled;

        public const string ExtraEnableSelection = "CategoryListActivity.ExtraEnableSelection";
        public const string ExtraInitialSelectionId = "CategoryListActivity.ExtraInitialSelectionId";
        public const string ResultSelected = "CategoryListActivity.ResultSelected";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SelectionEnabled = Intent.GetBooleanExtra(ExtraEnableSelection, false);
            int initialSelection = Intent.GetIntExtra(ExtraInitialSelectionId, -1);

            if (SelectionEnabled)
            {
                var saveButton = new Button(this)
                {
                    LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent)
                };
                saveButton.SetText(Resource.String.save);
                saveButton.Click += SaveButtonOnClick;

                var layout = new LinearLayout(this) {Orientation = Orientation.Vertical};
                layout.SetGravity(GravityFlags.Right);
                layout.AddView(saveButton);

                ListView.AddHeaderView(layout);
                ListView.Selector = new ColorDrawable(Color.Gray);
            }

            ListView.ItemClick += ListViewOnItemClick;
            var categories = CashflowData.Categories.OrderBy(x => x.Name).ToList();
            ListAdapter = new CategoryAdapter(this, categories);
            ListView.SetSelection(categories.FindIndex(c => c.Id == initialSelection));
        }

        private void SaveButtonOnClick(object sender, EventArgs eventArgs)
        {
            var data = new Intent();
            data.PutExtra(ResultSelected, Selected);
            SetResult(Result.Ok, data);
            Finish();
        }

        private void ListViewOnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (SelectionEnabled)
                Selected = e.Id;
            else
            {
                var i = new Intent(this, typeof(CategoryActivity));
                i.PutExtra(CategoryActivity.ExtraCategoryId, (int)e.Id);
                StartActivity(i);
            }
        }
    }
}