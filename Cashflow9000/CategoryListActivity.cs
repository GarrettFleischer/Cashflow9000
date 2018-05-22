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
        private bool SelectionEnabled;

        public const string ExtraEnableSelection = "CategoryListActivity.ExtraEnableSelection";
        public const string ExtraInitialSelectionId = "CategoryListActivity.ExtraInitialSelectionId";
        public const string ResultSelected = "CategoryListActivity.ResultSelected";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Get Intent data
            SelectionEnabled = Intent.GetBooleanExtra(ExtraEnableSelection, false);
            int initialSelection = Intent.GetIntExtra(ExtraInitialSelectionId, -1);

            // Create header layout
            LinearLayout layout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            layout.SetGravity(GravityFlags.Right);

            Button buttonAdd = new Button(this);
            buttonAdd.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            buttonAdd.SetText(Resource.String.add);
            buttonAdd.Click += AddButtonOnClick;

            layout.AddView(buttonAdd);

            ListView.AddHeaderView(layout);
            ListView.Selector = new ColorDrawable(Color.Gray);
            ListView.ItemClick += ListViewOnItemClick;

            CategoryAdapter adapter = new CategoryAdapter(this, TransactionType.Any);
            ListAdapter = adapter;
            ListView.SetSelection(adapter.Categories.FindIndex(c => c.Id == initialSelection));
        }

        protected override void OnResume()
        {
            base.OnResume();
            ListAdapter = new CategoryAdapter(this, TransactionType.Any);
        }

        private void AddButtonOnClick(object sender, EventArgs eventArgs)
        {
            StartActivity(new Intent(this, typeof(CategoryActivity)));
        }

        private void ListViewOnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (SelectionEnabled)
            {
                Intent data = new Intent();
                data.PutExtra(ResultSelected, (int)e.Id);
                SetResult(Result.Ok, data);
                Finish();
            }
            else
            {
                Intent i = new Intent(this, typeof(CategoryActivity));
                i.PutExtra(CategoryActivity.ExtraCategoryId, (int)e.Id);
                StartActivity(i);
            }
        }
    }
}