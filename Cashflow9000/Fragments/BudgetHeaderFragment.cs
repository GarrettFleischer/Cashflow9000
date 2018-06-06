using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Models;
using Java.Sql;
using DatePicker = Cashflow9000.Views.DatePicker;

namespace Cashflow9000.Fragments
{
    public class BudgetHeaderFragment : Fragment
    {
        public interface IBudgetHeaderFragmentListener
        {
            void OnDateChanged();

            void OnRecurrenceChanged();
        }

        private DatePicker DatePicker;
        private Spinner SpinRecurrence;

        public DateTime Date => DatePicker?.Date ?? DateTime.Today;
        public RecurrenceType Type { get; private set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.BudgetHeader, container, false);

            // Find UI views
            DatePicker = view.FindViewById<DatePicker>(Resource.Id.datePicker);
            SpinRecurrence = view.FindViewById<Spinner>(Resource.Id.spinRecurrence);

            // View logic
            DatePicker.ShowTime = false;
            DatePicker.DateChanged += DatePickerOnDateChanged;

            RecurrenceAdapter recurrenceAdapter = new RecurrenceAdapter(Activity, false);
            SpinRecurrence.Adapter = recurrenceAdapter;
            SpinRecurrence.SetSelection(recurrenceAdapter.Recurrences.FindIndex(c => c.Type == RecurrenceType.Monthly));
            SpinRecurrence.ItemSelected += SpinRecurrenceOnItemSelected;

            return view;
        }

        private void SpinRecurrenceOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Type = CashflowData.Recurrence((int)e.Id).Type;
            (Activity as IBudgetHeaderFragmentListener)?.OnDateChanged();
        }

        private void DatePickerOnDateChanged(object sender, EventArgs eventArgs)
        {
            (Activity as IBudgetHeaderFragmentListener)?.OnRecurrenceChanged();
        }
    }
}