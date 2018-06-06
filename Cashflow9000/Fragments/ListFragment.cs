using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Cashflow9000.Fragments
{
    public class ListFragment : Fragment
    {
        public interface IListFragmentListener
        {
            void OnAdd();
            void OnSelect(long id);
        }

        private TextView TextTitle;
        private Button ButtonAdd;
        private ListView ListView;

        private readonly int TitleId;
        private readonly IListAdapter Adapter;

        public ListFragment() : this(-1, null) { }

        public ListFragment(int titleId, IListAdapter adapter)
        {
            TitleId = titleId;
            Adapter = adapter;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ModelList, container, false);
            TextTitle = view.FindViewById<TextView>(Resource.Id.textTitle);
            ButtonAdd = view.FindViewById<Button>(Resource.Id.buttonAdd);
            ListView = view.FindViewById<ListView>(Resource.Id.listView);

            if (TitleId == -1) return view;

            TextTitle.SetText(TitleId);
            ButtonAdd.Click += ButtonAddOnClick;
            ListView.Adapter = Adapter;
            ListView.ItemClick += ListViewOnItemClick;

            return view;
        }

        public void SetHeaderFragment(Fragment header)
        {
            if (header != null) FragmentUtil.LoadFragment(Activity, Resource.Id.containerHeader, header);
        }

        public void SetAdapter(IListAdapter adapter)
        {
            ListView.Adapter = adapter;
        }
        
        private void ButtonAddOnClick(object sender, EventArgs eventArgs)
        {
            (Activity as IListFragmentListener)?.OnAdd();
        }

        private void ListViewOnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            (Activity as IListFragmentListener)?.OnSelect(e.Id);
        }
    }
}