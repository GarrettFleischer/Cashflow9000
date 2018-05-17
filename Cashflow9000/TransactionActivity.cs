using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Models;
using Newtonsoft.Json;

namespace Cashflow9000
{
    [Activity(Label = "Transaction")]
    public class TransactionActivity : Activity
    {
        private Button ButtonSave;
        private EditCurrency EditAmount;
        private ToggleButton ToggleType;
        private Spinner SpinCategory;
        private Spinner SpinMilestone;
        private EditText EditNote;

        private Transaction Transaction;

        //private ICollection<Category> Categories;

        public const string ExtraTransactionId = "TransactionActivity.TransactionId";

        // use case for both category and milestone is paying off a mortgage, it is both a recurring budget item and a long term goal

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Init view
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Transaction);
            
            // Load data from intent
            int id = Intent.GetIntExtra(ExtraTransactionId, -1);
            Transaction = ((id == -1) ? new Transaction() : CashflowData.Transaction(id));

            // Find UI views
            ButtonSave = FindViewById<Button>(Resource.Id.buttonSave);
            EditAmount = FindViewById<EditCurrency>(Resource.Id.editValue);
            ToggleType = FindViewById<ToggleButton>(Resource.Id.toggleType);
            SpinCategory = FindViewById<Spinner>(Resource.Id.spinCategory);
            SpinMilestone = FindViewById<Spinner>(Resource.Id.spinMilestone);
            EditNote = FindViewById<EditText>(Resource.Id.editNote);

            // View logic
            ButtonSave.Click += ButtonSaveOnClick;

            EditAmount.Text = Transaction.Amount.ToString(CultureInfo.CurrentCulture);
            EditAmount.AfterTextChanged += EditAmountOnAfterTextChanged;

            ToggleType.Checked = Transaction.Type == TransactionType.Income;
            ToggleType.CheckedChange += ToggleTypeOnCheckedChange;

            SpinCategory.Adapter = new CategoryAdapter(this, GetCategories());
            SpinCategory.ItemSelected += SpinCategoryOnItemSelected;

            SpinMilestone.Adapter = new MilestoneAdapter(this, GetMilestones());
            SpinMilestone.ItemSelected += SpinMilestoneOnItemSelected;

            EditNote.TextChanged += EditNoteOnTextChanged;
            
            UpdateUI();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (data == null) return;
            //Cheater = data.GetBooleanExtra(CheatActivity.ExtraAnswerShown, false);
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

        private void ButtonSaveOnClick(object sender, EventArgs eventArgs)
        {
            CashflowData.InsertOrReplace(Transaction);
            Finish();
        }

        private void EditAmountOnAfterTextChanged(object sender, AfterTextChangedEventArgs afterTextChangedEventArgs)
        {
            Transaction.Amount = EditAmount.Value;
        }

        private void ToggleTypeOnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs checkedChangeEventArgs)
        {
            Transaction.Type = ToggleType.Checked ? TransactionType.Income : TransactionType.Expense;
            SpinCategory.Adapter = SpinCategory.Adapter = new CategoryAdapter(this, GetCategories());
            Transaction.Category = null;
            UpdateUI();
        }

        private void UpdateUI()
        {
            ToggleType.SetBackgroundColor(ToggleType.Checked ? Color.DarkGreen : Color.DarkRed);
            //ButtonSave.Enabled = Transaction.Category != null;
        }

        private List<Category> GetCategories()
        {
            TransactionType type = ToggleType.Checked ? TransactionType.Income : TransactionType.Expense;
            return CashflowData.Categories.Where(e => e.Type == type).OrderBy(x => x.Name).ToList();
        }

        private static List<Milestone> GetMilestones()
        {
            return CashflowData.Milestones.OrderBy(x => x.Name).ToList();
        }
    }
}