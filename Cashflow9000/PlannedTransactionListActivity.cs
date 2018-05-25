using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Cashflow9000.Adapters;
using ListFragment = Cashflow9000.Fragments.ListFragment;

namespace Cashflow9000
{
    [Activity(Label = "PlannedTransactionListActivity")]
    public class PlannedTransactionListActivity : Activity, ListFragment.IListListener
    {
        private ListFragment Fragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Fragment = new ListFragment(Resource.String.plannedTransaction, new PlannedTransactionAdapter(this));
            FragmentUtil.LoadFragment(this, LayoutId.PlannedTransactionListActivity, Fragment);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Fragment.SetAdapter(new PlannedTransactionAdapter(this));
        }

        public void OnAdd()
        {
            StartActivity(new Intent(this, typeof(PlannedTransactionActivity)));
        }

        public void OnSelect(long id)
        {
            Intent i = new Intent(this, typeof(PlannedTransactionActivity));
            i.PutExtra(PlannedTransactionActivity.ExtraPlannedPaymentId, (int)id);
            StartActivity(i);
        }
    }
}