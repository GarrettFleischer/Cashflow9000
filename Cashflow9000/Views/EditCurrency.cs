using System;
using System.Text.RegularExpressions;
using Android.Content;
using Android.Icu.Text;
using Android.Text;
using Android.Util;
using Android.Widget;

namespace Cashflow9000
{
    public class EditCurrency : EditText
    {
        public EditCurrency(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public EditCurrency(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        private void Initialize()
        {
            InputType = InputTypes.ClassNumber;
            TextChanged += OnTextChanged;
            Text = "0";
        }

        private void OnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            TextChanged -= OnTextChanged;
            var cleanString = Regex.Replace(Text, "[^0-9a-zA-Z]+", "");
            var parsed = Double.Parse(cleanString);
            var formatted = NumberFormat.CurrencyInstance.Format(parsed / 100);
            Text = formatted;
            TextChanged += OnTextChanged;
            SetSelection(formatted.Length);
        }
    }
}