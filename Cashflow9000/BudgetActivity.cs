using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "BudgetActivity")]
    class BudgetActivity : Activity
    {
        private Button ButtonSave;
        private EditCurrency EditAmount;
        private Spinner SpinCategory;
        private Spinner SpinRecurrence;

        private Budget Budget;

        //private ICollection<Category> Categories;

        public const string ExtraTransactionId = "TransactionActivity.TransactionId";

        // use case for both category and milestone is paying off a mortgage, it is both a recurring budget item and a long term goal

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Init view
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Budget);

            // Load data from intent
            int id = Intent.GetIntExtra(ExtraTransactionId, -1);
            Budget = ((id == -1) ? new Budget() : CashflowData.Budget(id));

            // Find UI views
            ButtonSave = FindViewById<Button>(Resource.Id.buttonSave);
            EditAmount = FindViewById<EditCurrency>(Resource.Id.editValue);
            SpinCategory = FindViewById<Spinner>(Resource.Id.spinCategory);
            SpinRecurrence = FindViewById<Spinner>(Resource.Id.spinRecurrence);

            // View logic
            ButtonSave.Click += ButtonSaveOnClick;

            EditAmount.Text = Budget.Amount.ToString(CultureInfo.CurrentCulture);
            EditAmount.AfterTextChanged += EditAmountOnAfterTextChanged;

            SpinCategory.Adapter = new CategoryAdapter(this, TransactionType.Expense);
            SpinCategory.ItemSelected += SpinCategoryOnItemSelected;

            SpinRecurrence.Adapter = new RecurrenceAdapter(this);
            SpinRecurrence.ItemSelected += SpinRecurrenceOnItemSelected;
        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (data == null) return;
            //Cheater = data.GetBooleanExtra(CheatActivity.ExtraAnswerShown, false);
        }

        private void SpinCategoryOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Budget.Category = ((CategoryAdapter)SpinCategory.Adapter)[e.Position];
        }
        private void SpinRecurrenceOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Budget.Recurrence = ((RecurrenceAdapter)SpinRecurrence.Adapter)[e.Position];
        }

        private void ButtonSaveOnClick(object sender, EventArgs eventArgs)
        {
            CashflowData.InsertOrReplace(Budget);
            Finish();
        }

        private void EditAmountOnAfterTextChanged(object sender, AfterTextChangedEventArgs afterTextChangedEventArgs)
        {
            Budget.Amount = EditAmount.Value;
        }
    }
}