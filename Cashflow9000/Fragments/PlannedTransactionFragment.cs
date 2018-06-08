using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Models;

namespace Cashflow9000.Fragments
{
    public class PlannedTransactionFragment : TransactionFragment
    {
        public interface IPlannedTransactionListener
        {
            void PlannedPaymentSaved(PlannedTransaction plannedTransaction);
        }

        private Spinner SpinRecurrence;

        public PlannedTransactionFragment() : this(-1) { }

        public PlannedTransactionFragment(int plannedPaymentId)
        {
            Item = ((plannedPaymentId == -1) ? new PlannedTransaction() : CashflowData.PlannedTransaction(plannedPaymentId));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            view.FindViewById<TextView>(Resource.Id.textTitle).SetText(Resource.String.plannedTransaction);
            view.FindViewById<TextView>(Resource.Id.textRecurrence).Visibility = ViewStates.Visible;

            RecurrenceAdapter recurrenceAdapter = new RecurrenceAdapter(Activity, false);
            SpinRecurrence = view.FindViewById<Spinner>(Resource.Id.spinRecurrence);
            SpinRecurrence.Visibility = ViewStates.Visible;
            SpinRecurrence.Adapter = recurrenceAdapter;
            int index = recurrenceAdapter.Recurrences.FindIndex(c => c.Id == ((PlannedTransaction)Item).RecurrenceId);
            SpinRecurrence.SetSelection(index);
            SpinRecurrence.ItemSelected += SpinRecurrenceOnItemSelected;

            return view;
        }

        protected void SpinRecurrenceOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ((PlannedTransaction)Item).Recurrence = ((RecurrenceAdapter)SpinRecurrence.Adapter)[e.Position];
        }
    }
}