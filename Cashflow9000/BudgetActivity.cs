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
        private EditText EditName;
        private EditCurrency EditAmount;
        private Spinner SpinCategory;
        private Spinner SpinRecurrence;

        private Budget Budget;


        public const string ExtraBudgetId = "BudgetActivity.ExtraBudgetId";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Init view
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Budget);

            // Load data from intent
            int id = Intent.GetIntExtra(ExtraBudgetId, -1);
            Budget = ((id == -1) ? new Budget() : CashflowData.Budget(id));

            // Find UI views
            ButtonSave = FindViewById<Button>(Resource.Id.buttonSave);
            EditName = FindViewById<EditText>(Resource.Id.editName);
            EditAmount = FindViewById<EditCurrency>(Resource.Id.editAmount);
            SpinCategory = FindViewById<Spinner>(Resource.Id.spinCategory);
            SpinRecurrence = FindViewById<Spinner>(Resource.Id.spinRecurrence);

            // View logic
            ButtonSave.Click += ButtonSaveOnClick;

            EditName.Text = Budget.Name;
            EditName.TextChanged += EditNameOnTextChanged;

            EditAmount.Text = Budget.Amount.ToString(CultureInfo.CurrentCulture);
            EditAmount.AfterTextChanged += EditAmountOnAfterTextChanged;

            SpinCategory.Adapter = new CategoryAdapter(this, TransactionType.Expense);
            SpinCategory.ItemSelected += SpinCategoryOnItemSelected;

            SpinRecurrence.Adapter = new RecurrenceAdapter(this);
            SpinRecurrence.ItemSelected += SpinRecurrenceOnItemSelected;
        }

        private void EditNameOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Budget.Name = EditName.Text;
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