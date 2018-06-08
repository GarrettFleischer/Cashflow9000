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
using Cashflow9000.Models;

namespace Cashflow9000.Fragments
{
    public class CategoryFragment : ItemHandlerFragment<Category>
    {
        private Button ButtonSave;
        private Button ButtonDelete;
        private EditText EditName;
        private Spinner SpinType;
        
        public CategoryFragment() : this(-1) {}

        public CategoryFragment(int categoryId)
        {
            Item = ((categoryId == -1) ? new Category() : CashflowData.Category(categoryId));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Category, container, false);

            // Find UI views
            ButtonSave = view.FindViewById<Button>(Resource.Id.buttonSave);
            ButtonDelete = view.FindViewById<Button>(Resource.Id.buttonDelete);
            EditName = view.FindViewById<EditText>(Resource.Id.editName);
            SpinType = view.FindViewById<Spinner>(Resource.Id.spinType);

            // View logic
            ButtonSave.Click += SaveItem;
            ButtonDelete.Click += DeleteItem;

            EditName.Text = Item.Name;
            EditName.TextChanged += EditNameOnTextChanged;

            TransactionTypeAdapter adapter = new TransactionTypeAdapter(Activity);
            SpinType.Adapter = adapter;
            SpinType.SetSelection(adapter.TransactionTypes.FindIndex(c => c == Item.Type));
            SpinType.ItemSelected += SpinTypeOnItemSelected;

            UpdateUI();

            return view;
        }

        private void SpinTypeOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Item.Type = ((TransactionTypeAdapter)SpinType.Adapter).TransactionTypes[e.Position];
        }

        private void EditNameOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Item.Name = EditName.Text;
            UpdateUI();
        }
        
        private void UpdateUI()
        {
            ButtonSave.Enabled = EditName.Text.Length >= 0;
        }
    }
}