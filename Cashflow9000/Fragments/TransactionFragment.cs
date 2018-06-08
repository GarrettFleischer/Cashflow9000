using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Models;
using DatePicker = Cashflow9000.Views.DatePicker;

namespace Cashflow9000.Fragments
{
    public class TransactionFragment : ItemHandlerFragment<Transaction>
    {
        private Button ButtonSave;
        private Button ButtonDelete;
        private EditCurrency EditAmount;
        private ToggleButton ToggleType;
        private Spinner SpinCategory;
        private Spinner SpinMilestone;
        private EditText EditNote;
        private DatePicker DatePicker;
        
        // use case for both category and milestone is paying off a mortgage, it is both a recurring budget item and a long term goal

        public TransactionFragment() : this(-1) {}

        public TransactionFragment(int transactionId = -1)
        {
            Item = ((transactionId == -1) ? new Transaction() : CashflowData.Transaction(transactionId));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Transaction, container, false);
            ButtonSave = view.FindViewById<Button>(Resource.Id.buttonSave);
            ButtonDelete = view.FindViewById<Button>(Resource.Id.buttonDelete);
            EditAmount = view.FindViewById<EditCurrency>(Resource.Id.editValue);
            ToggleType = view.FindViewById<ToggleButton>(Resource.Id.toggleType);
            SpinCategory = view.FindViewById<Spinner>(Resource.Id.spinCategory);
            SpinMilestone = view.FindViewById<Spinner>(Resource.Id.spinMilestone);
            DatePicker = view.FindViewById<DatePicker>(Resource.Id.datePicker);
            EditNote = view.FindViewById<EditText>(Resource.Id.editNote);

            view.FindViewById<Spinner>(Resource.Id.spinRecurrence).Visibility = ViewStates.Invisible;
            view.FindViewById<TextView>(Resource.Id.textRecurrence).Visibility = ViewStates.Invisible;

            ButtonSave.Click += SaveItem;
            ButtonDelete.Click += DeleteItem;

            EditAmount.Value = Item.Amount;
            EditAmount.ValueChanged += EditAmountOnValueChanged;

            ToggleType.Checked = Item.Type == TransactionType.Income;
            ToggleType.CheckedChange += ToggleTypeOnCheckedChange;

            CategoryAdapter categoryAdapter = new CategoryAdapter(Activity, GetTransactionType(), true);
            SpinCategory.Adapter = categoryAdapter;
            SpinCategory.SetSelection(categoryAdapter.Categories.FindIndex(c => c?.Id == Item.CategoryId));
            SpinCategory.ItemSelected += SpinCategoryOnItemSelected;

            MilestoneAdapter milestoneAdapter = new MilestoneAdapter(Activity, true);
            SpinMilestone.Adapter = milestoneAdapter;
            SpinMilestone.SetSelection(milestoneAdapter.Milestones.FindIndex(c => c?.Id == Item.MilestoneId));
            SpinMilestone.ItemSelected += SpinMilestoneOnItemSelected;

            if (Item.Date == new DateTime()) Item.Date = DateTime.Now;
            DatePicker.Date = Item.Date;
            DatePicker.DateChanged += DatePickerOnDateChanged;

            EditNote.Text = Item.Note;
            EditNote.TextChanged += EditNoteOnTextChanged;

            UpdateUI();

            return view;
        }

        private void DatePickerOnDateChanged(object sender, EventArgs eventArgs)
        {
            Item.Date = DatePicker.Date;
        }

        private void EditAmountOnValueChanged(object sender, EventArgs eventArgs)
        {
            Item.Amount = EditAmount.Value;
        }

        private void EditNoteOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Item.Note = EditNote.Text;
        }

        private void SpinMilestoneOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Item.Milestone = ((MilestoneAdapter)SpinMilestone.Adapter)[e.Position];
        }

        private void SpinCategoryOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Item.Category = ((CategoryAdapter)SpinCategory.Adapter)[e.Position];
        }
        
        private void ToggleTypeOnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs checkedChangeEventArgs)
        {
            Item.Type = ToggleType.Checked ? TransactionType.Income : TransactionType.Expense;
            SpinCategory.Adapter = SpinCategory.Adapter = new CategoryAdapter(Activity, GetTransactionType(), true);
            Item.Category = null;
            UpdateUI();
        }

        private void UpdateUI()
        {
            ToggleType.SetBackgroundColor(ToggleType.Checked ? Color.DarkGreen : Color.DarkRed);
        }

        private TransactionType GetTransactionType() => ToggleType.Checked ? TransactionType.Income : TransactionType.Expense;

    }
}