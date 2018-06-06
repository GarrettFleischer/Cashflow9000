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
    public class TransactionFragment : Fragment
    {
        public interface ITransactionFragmentListener
        {
            void TransactionSaved(Transaction transaction);
        }

        private Button ButtonSave;
        private EditCurrency EditAmount;
        private ToggleButton ToggleType;
        private Spinner SpinCategory;
        private Spinner SpinMilestone;
        private EditText EditNote;
        private DatePicker DatePicker;

        protected Transaction Transaction;

        // use case for both category and milestone is paying off a mortgage, it is both a recurring budget item and a long term goal

        public TransactionFragment() : this(-1) {}

        public TransactionFragment(int transactionId = -1)
        {
            Transaction = ((transactionId == -1) ? new Transaction() : CashflowData.Transaction(transactionId));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Transaction, container, false);
            ButtonSave = view.FindViewById<Button>(Resource.Id.buttonSave);
            EditAmount = view.FindViewById<EditCurrency>(Resource.Id.editValue);
            ToggleType = view.FindViewById<ToggleButton>(Resource.Id.toggleType);
            SpinCategory = view.FindViewById<Spinner>(Resource.Id.spinCategory);
            SpinMilestone = view.FindViewById<Spinner>(Resource.Id.spinMilestone);
            DatePicker = view.FindViewById<DatePicker>(Resource.Id.datePicker);
            EditNote = view.FindViewById<EditText>(Resource.Id.editNote);

            view.FindViewById<Spinner>(Resource.Id.spinRecurrence).Visibility = ViewStates.Invisible;
            view.FindViewById<TextView>(Resource.Id.textRecurrence).Visibility = ViewStates.Invisible;

            ButtonSave.Click += ButtonSaveOnClick;

            EditAmount.Value = Transaction.Amount;
            EditAmount.ValueChanged += EditAmountOnValueChanged;

            ToggleType.Checked = Transaction.Type == TransactionType.Income;
            ToggleType.CheckedChange += ToggleTypeOnCheckedChange;

            CategoryAdapter categoryAdapter = new CategoryAdapter(Activity, GetTransactionType(), true);
            SpinCategory.Adapter = categoryAdapter;
            SpinCategory.SetSelection(categoryAdapter.Categories.FindIndex(c => c?.Id == Transaction.CategoryId));
            SpinCategory.ItemSelected += SpinCategoryOnItemSelected;

            MilestoneAdapter milestoneAdapter = new MilestoneAdapter(Activity, true);
            SpinMilestone.Adapter = milestoneAdapter;
            SpinMilestone.SetSelection(milestoneAdapter.Milestones.FindIndex(c => c?.Id == Transaction.MilestoneId));
            SpinMilestone.ItemSelected += SpinMilestoneOnItemSelected;

            if (Transaction.Date == new DateTime()) Transaction.Date = DateTime.Now;
            DatePicker.Date = Transaction.Date;
            DatePicker.DateChanged += DatePickerOnDateChanged;

            EditNote.Text = Transaction.Note;
            EditNote.TextChanged += EditNoteOnTextChanged;

            UpdateUI();

            return view;
        }

        private void DatePickerOnDateChanged(object sender, EventArgs eventArgs)
        {
            Transaction.Date = DatePicker.Date;
        }

        private void EditAmountOnValueChanged(object sender, EventArgs eventArgs)
        {
            Transaction.Amount = EditAmount.Value;
        }

        private void EditNoteOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Transaction.Note = EditNote.Text;
        }

        private void SpinMilestoneOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Transaction.Milestone = ((MilestoneAdapter)SpinMilestone.Adapter)[e.Position];
        }

        private void SpinCategoryOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Transaction.Category = ((CategoryAdapter)SpinCategory.Adapter)[e.Position];
        }

        protected virtual void ButtonSaveOnClick(object sender, EventArgs eventArgs)
        {
            (Activity as ITransactionFragmentListener)?.TransactionSaved(Transaction);
        }

        private void ToggleTypeOnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs checkedChangeEventArgs)
        {
            Transaction.Type = ToggleType.Checked ? TransactionType.Income : TransactionType.Expense;
            SpinCategory.Adapter = SpinCategory.Adapter = new CategoryAdapter(Activity, GetTransactionType(), true);
            Transaction.Category = null;
            UpdateUI();
        }

        private void UpdateUI()
        {
            ToggleType.SetBackgroundColor(ToggleType.Checked ? Color.DarkGreen : Color.DarkRed);
        }

        private TransactionType GetTransactionType() => ToggleType.Checked ? TransactionType.Income : TransactionType.Expense;
    }
}