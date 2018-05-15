using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Cashflow9000.Models;

namespace Cashflow9000
{
    [Activity(Label = "MilestoneActivity")]
    public class MilestoneActivity : Activity
    {
        private Button ButtonSave;
        private EditText EditName;
        private EditCurrency EditAmount;

        private Milestone Milestone;


        public const string ExtraMilestone = "MilestoneActivity.ExtraMilestone";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Milestone);

            // Load data from intent
            int id = Intent.GetIntExtra(ExtraMilestone, -1);
            Milestone = (id == -1) ? new Milestone() : CashflowData.Milestone(id);

            // Find UI views
            ButtonSave = FindViewById<Button>(Resource.Id.buttonSave);
            EditName = FindViewById<EditText>(Resource.Id.editName);
            EditAmount = FindViewById<EditCurrency>(Resource.Id.editAmount);

            ButtonSave.Click += ButtonSaveOnClick;

            EditName.Text = Milestone.Name;
            EditName.TextChanged += EditNameOnTextChanged;

            EditAmount.Text = Milestone.Amount.ToString(CultureInfo.CurrentCulture);
            EditAmount.TextChanged += EditAmountOnTextChanged;

            UpdateUI();
        }

        private void ButtonSaveOnClick(object sender, EventArgs eventArgs)
        {
            CashflowData.InsertOrReplace(Milestone);
            Finish();
        }

        private void EditAmountOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Milestone.Amount = EditAmount.Value;
        }

        private void EditNameOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Milestone.Name = EditName.Text;
            UpdateUI();
        }

        private void UpdateUI()
        {
            ButtonSave.Enabled = Milestone.Name.Length > 0;
        }
    }
}