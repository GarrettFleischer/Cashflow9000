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
    public class BudgetFragment : ItemHandlerFragment<Budget>
    {
        private Button ButtonSave;
        private Button ButtonDelete;
        private EditText EditName;
        private EditCurrency EditAmount;
        private Spinner SpinCategory;
        private Spinner SpinRecurrence;
        
        public BudgetFragment() : this(-1) {}

        public BudgetFragment(int id)
        {
            Item = ((id == -1) ? new Budget() : CashflowData.Budget(id));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Budget, container, false);

            // Find UI views
            ButtonSave = view.FindViewById<Button>(Resource.Id.buttonSave);
            ButtonDelete = view.FindViewById<Button>(Resource.Id.buttonDelete);
            EditName = view.FindViewById<EditText>(Resource.Id.editName);
            EditAmount = view.FindViewById<EditCurrency>(Resource.Id.editAmount);
            SpinCategory = view.FindViewById<Spinner>(Resource.Id.spinCategory);
            SpinRecurrence = view.FindViewById<Spinner>(Resource.Id.spinRecurrence);

            // View logic
            ButtonSave.Click += SaveItem;
            ButtonDelete.Click += DeleteItem;

            EditName.Text = Item.Name;
            EditName.TextChanged += EditNameOnTextChanged;

            EditAmount.Value = Item.Amount;
            EditAmount.AfterTextChanged += EditAmountOnAfterTextChanged;

            CategoryAdapter categoryAdapter = new CategoryAdapter(Activity, TransactionType.Expense, true);
            SpinCategory.Adapter = categoryAdapter;
            SpinCategory.SetSelection(categoryAdapter.Categories.FindIndex(c => c?.Id == Item.CategoryId));
            SpinCategory.ItemSelected += SpinCategoryOnItemSelected;

            RecurrenceAdapter recurrenceAdapter = new RecurrenceAdapter(Activity, true);
            SpinRecurrence.Adapter = recurrenceAdapter;
            SpinRecurrence.SetSelection(recurrenceAdapter.Recurrences.FindIndex(c => c?.Id == Item.RecurrenceId));
            SpinRecurrence.ItemSelected += SpinRecurrenceOnItemSelected;

            return view;
        }

        private void EditNameOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Item.Name = EditName.Text;
        }

        private void SpinCategoryOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Item.Category = ((CategoryAdapter)SpinCategory.Adapter)[e.Position];
        }
        private void SpinRecurrenceOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Item.Recurrence = ((RecurrenceAdapter)SpinRecurrence.Adapter)[e.Position];
        }
        
        private void EditAmountOnAfterTextChanged(object sender, AfterTextChangedEventArgs afterTextChangedEventArgs)
        {
            Item.Amount = EditAmount.Value;
        }
    }
}