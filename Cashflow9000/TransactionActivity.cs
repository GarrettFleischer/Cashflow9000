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

namespace Cashflow9000
{
    [Activity(Label = "Transaction")]
    public class TransactionActivity : Activity
    {
        private Button _ButtonSave;
        private EditText _TextValue;
        private ToggleButton _ToggleType;
        private Spinner _SpinCategory;
        private EditText _TextNote;

        private ICollection<Category> _Categories;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Transaction);

            _ButtonSave = FindViewById<Button>(Resource.Id.buttonSave);
            _TextValue = FindViewById<EditText>(Resource.Id.textValue);
            _ToggleType = FindViewById<ToggleButton>(Resource.Id.toggleType);
            _SpinCategory = FindViewById<Spinner>(Resource.Id.spinCategory);
            _TextNote = FindViewById<EditText>(Resource.Id.textNote);

            _TextValue.TextChanged += TextValueOnTextChanged;
            _TextValue.Text = "0";

            _ToggleType.CheckedChange += ToggleTypeOnCheckedChange;
            UpdateToggleTypeColor();
            
            //_SpinCategory.Adapter = new ArrayAdapter<Category>(this, Resource.Layout.);
        }

        private void ToggleTypeOnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs checkedChangeEventArgs)
        {
            UpdateToggleTypeColor();
        }

        private void TextValueOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            _TextValue.TextChanged -= TextValueOnTextChanged;
            var cleanString = Regex.Replace(_TextValue.Text, "[^0-9a-zA-Z]+", "");
            var parsed = Double.Parse(cleanString);
            var formatted = NumberFormat.CurrencyInstance.Format((parsed / 100));
            _TextValue.Text = formatted;
            _TextValue.TextChanged += TextValueOnTextChanged;
            _TextValue.SetSelection(formatted.Length);
        }

        private void UpdateToggleTypeColor()
        {
            _ToggleType.SetBackgroundColor(_ToggleType.Checked ? Color.DarkGreen : Color.DarkRed);
        }

        private ICollection<Category> GetCategories()
        {
            var list = new List<Category>
            {
                new Category {Name = "Rent", Type = Category.EType.Expense},
                new Category {Name = "Groceries", Type = Category.EType.Expense},
                new Category {Name = "Fuel", Type = Category.EType.Expense},
                new Category {Name = "Utilities", Type = Category.EType.Expense},
                new Category {Name = "Wages", Type = Category.EType.Income},
                new Category {Name = "Investments", Type = Category.EType.Income},
            };

            var type = _ToggleType.Checked ? Category.EType.Income : Category.EType.Expense;
            return list.Where(e => e.Type == type).OrderBy(x => x.Name).ToList();
        }
    }
}