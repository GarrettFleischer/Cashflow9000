using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;

namespace Cashflow9000
{
    [Activity(Label = "Transaction")]
    public class TransactionActivity : Activity
    {
        private Button ButtonSave;
        private EditText EditValue;
        private ToggleButton ToggleType;
        private Spinner SpinCategory;
        private EditText EditNote;

        private Transaction Transaction;

        //private ICollection<Category> Categories;

        public const string ExtraTransaction = "TransactionActivity.Transaction";

        // use case for both category and milestone is paying off a mortgage, it is both a recurring budget item and a long term goal

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Init view
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Transaction);

            // Load data from intent
            Transaction =  JsonConvert.DeserializeObject<Transaction>(Intent.GetStringExtra(ExtraTransaction) ?? "") ?? new Transaction();

            // Find UI views
            ButtonSave = FindViewById<Button>(Resource.Id.buttonSave);
            EditValue = FindViewById<EditText>(Resource.Id.editValue);
            ToggleType = FindViewById<ToggleButton>(Resource.Id.toggleType);
            SpinCategory = FindViewById<Spinner>(Resource.Id.spinCategory);
            EditNote = FindViewById<EditText>(Resource.Id.editNote);


            // View logic
            EditValue.AfterTextChanged += EditValueOnAfterTextChanged;

            ToggleType.CheckedChange += ToggleTypeOnCheckedChange;
            UpdateToggleTypeColor();
            
            //_SpinCategory.Adapter = new ArrayAdapter<Category>(this, Resource.Layout.);
        }

        private void EditValueOnAfterTextChanged(object sender, AfterTextChangedEventArgs afterTextChangedEventArgs)
        {

        }

        private void ToggleTypeOnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs checkedChangeEventArgs)
        {
            UpdateToggleTypeColor();
        }

        private void UpdateToggleTypeColor()
        {
            ToggleType.SetBackgroundColor(ToggleType.Checked ? Color.DarkGreen : Color.DarkRed);
        }

        private ICollection<Category> GetCategories()
        {
            var list = new List<Category>
            {
                new Category {Name = "Rent", Type = TransactionType.Expense},
                new Category {Name = "Groceries", Type = TransactionType.Expense},
                new Category {Name = "Fuel", Type = TransactionType.Expense},
                new Category {Name = "Utilities", Type = TransactionType.Expense},
                new Category {Name = "Wages", Type = TransactionType.Income},
                new Category {Name = "Investments", Type = TransactionType.Income},
            };

            var type = ToggleType.Checked ? TransactionType.Income : TransactionType.Expense;
            return list.Where(e => e.Type == type).OrderBy(x => x.Name).ToList();
        }
    }
}