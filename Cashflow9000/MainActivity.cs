﻿using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace Cashflow9000
{
    [Activity(Label = "Cashflow9000", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button transaction = FindViewById<Button>(Resource.Id.buttonTransaction);
            transaction.Click += delegate
            {
                Intent i = new Intent(this, typeof(TransactionActivity));
                StartActivity(i);
            };
        }
    }
}

