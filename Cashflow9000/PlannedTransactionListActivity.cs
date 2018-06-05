using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Cashflow9000.Adapters;
using Cashflow9000.Fragments;
using Cashflow9000.Models;
using ListFragment = Cashflow9000.Fragments.ListFragment;

namespace Cashflow9000
{
    [Activity(Label = "PlannedTransactionListActivity")]
    public class PlannedTransactionListActivity : ListActivity<PlannedTransactionActivity>, PlannedTransactionFragment.IPlannedTransactionListener
    {
        protected override IListAdapter GetListAdapter()
        {
            return new PlannedTransactionAdapter(this);
        }

        protected override Fragment GetItemFragment(int id = -1)
        {
            return new PlannedTransactionFragment(id);
        }

        protected override string GetExtraId()
        {
            return PlannedTransactionActivity.ExtraPlannedPaymentId;
        }

        public void PlannedPaymentSaved(PlannedTransaction plannedTransaction)
        {
            CashflowData.InsertOrReplace(plannedTransaction);
            UpdateListAdapter();
        }
    }
}