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
using Android.Util;
using Android.Views;
using Android.Widget;
using Cashflow9000.Models;

namespace Cashflow9000.Fragments
{
    public class MilestoneFragment : ItemHandlerFragment<Milestone>
    {
        private Button ButtonSave;
        private Button ButtonDelete;
        private EditText EditName;
        private EditCurrency EditAmount;

        public MilestoneFragment() : this(-1) {}

        public MilestoneFragment(int milestoneId)
        {
            Item = ((milestoneId == -1) ? new Milestone() : CashflowData.Milestone(milestoneId));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Milestone, container, false);

            // Find UI views
            ButtonSave = view.FindViewById<Button>(Resource.Id.buttonSave);
            ButtonDelete = view.FindViewById<Button>(Resource.Id.buttonDelete);
            EditName = view.FindViewById<EditText>(Resource.Id.editName);
            EditAmount = view.FindViewById<EditCurrency>(Resource.Id.editAmount);

            ButtonSave.Click += SaveItem;
            ButtonDelete.Click += DeleteItem;

            EditName.Text = Item.Name;
            EditName.TextChanged += EditNameOnTextChanged;

            EditAmount.Value = Item.Amount;
            EditAmount.TextChanged += EditAmountOnTextChanged;
            
            return view;
        }

        private void EditAmountOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Item.Amount = EditAmount.Value;
        }

        private void EditNameOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Item.Name = EditName.Text;
        }

    }
}