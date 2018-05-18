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
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "CategoryActivity")]
    public class CategoryActivity : Activity
    {
        private Button ButtonSave;
        private EditText EditName;

        private Category Category;

        public const string ExtraCategoryId = "TransactionActivity.TransactionId";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Category);

            // Load data from intent
            int id = Intent.GetIntExtra(ExtraCategoryId, -1);
            Category = ((id == -1) ? new Category() : CashflowData.Category(id));

            // Find UI views
            ButtonSave = FindViewById<Button>(Resource.Id.buttonSave);
            EditName = FindViewById<EditText>(Resource.Id.editName);

            // View logic
            ButtonSave.Click += ButtonSaveOnClick;

            EditName.Text = Category.Name;
            EditName.TextChanged += EditNameOnTextChanged;

            UpdateUI();
        }

        private void ButtonSaveOnClick(object sender, EventArgs eventArgs)
        {
            CashflowData.InsertOrReplace(Category);
            Finish();
        }

        private void EditNameOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Category.Name = EditName.Text;
            UpdateUI();
        }

        private void UpdateUI()
        {
            ButtonSave.Enabled = EditName.Text.Length >= 0;
        }
    }
}