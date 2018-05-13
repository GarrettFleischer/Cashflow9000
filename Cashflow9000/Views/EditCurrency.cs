﻿using System;
using System.Text.RegularExpressions;
using Android.Content;
using Android.Text;
using Android.Util;
using Android.Widget;
using Java.Text;
using NumberFormat = Android.Icu.Text.NumberFormat;

namespace Cashflow9000
{
    public class EditCurrency : EditText
    {
        public decimal Value => (decimal)NumberFormat.CurrencyInstance.ParseCurrency(Text, new ParsePosition(0)).Number.DoubleValue();

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
            var formatted = NumberFormat.CurrencyInstance.Format(Double.Parse(cleanString) / 100);
            Text = formatted;
            TextChanged += OnTextChanged;
            SetSelection(formatted.Length);
        }
    }
}