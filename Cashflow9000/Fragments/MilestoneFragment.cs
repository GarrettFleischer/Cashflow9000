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
    public class MilestoneFragment : Fragment
    {
        public interface IMilestoneFragmentListener
        {
            void MilestoneSaved(Milestone milestone);
        }

        private Button ButtonSave;
        private EditText EditName;
        private EditCurrency EditAmount;

        private readonly Milestone Milestone;

        public MilestoneFragment() : this(-1) {}

        public MilestoneFragment(int milestoneId)
        {
            Milestone = ((milestoneId == -1) ? new Milestone() : CashflowData.Milestone(milestoneId));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Milestone, container, false);

            // Find UI views
            ButtonSave = view.FindViewById<Button>(Resource.Id.buttonSave);
            EditName = view.FindViewById<EditText>(Resource.Id.editName);
            EditAmount = view.FindViewById<EditCurrency>(Resource.Id.editAmount);

            ButtonSave.Click += ButtonSaveOnClick;

            EditName.Text = Milestone.Name;
            EditName.TextChanged += EditNameOnTextChanged;

            EditAmount.Value = Milestone.Amount;
            EditAmount.TextChanged += EditAmountOnTextChanged;
            
            return view;
        }

        private void EditAmountOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Milestone.Amount = EditAmount.Value;
        }

        private void EditNameOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Milestone.Name = EditName.Text;
        }

        private void ButtonSaveOnClick(object sender, EventArgs eventArgs)
        {
            ((IMilestoneFragmentListener) Activity)?.MilestoneSaved(Milestone);
        }
    }
}