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

namespace Cashflow9000
{
    [Activity(Label = "BudgetListActivity")]
    public class BudgetListActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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

            ListAdapter = new BudgetAdapter(this);
        }
        protected override void OnResume()
        {
            base.OnResume();
            ListAdapter = new BudgetAdapter(this);
        }

        private void AddButtonOnClick(object sender, EventArgs eventArgs)
        {
            StartActivity(new Intent(this, typeof(BudgetActivity)));
        }

        private void ListViewOnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent i = new Intent(this, typeof(BudgetActivity));
            i.PutExtra(BudgetActivity.ExtraBudgetId, (int)e.Id);
            StartActivity(i);
        }
    }
}