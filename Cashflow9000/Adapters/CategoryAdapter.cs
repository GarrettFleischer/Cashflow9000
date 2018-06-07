using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cashflow9000.Models;
using Object = Java.Lang.Object;

namespace Cashflow9000.Adapters
{
    class CategoryAdapter : BaseAdapter<Category>
    {
        private readonly Activity Context;
        public List<Category> Categories { get; }

        private readonly bool Spinner;

        public CategoryAdapter(Activity context, TransactionType type, bool spinner)
        {
            Context = context;
            Spinner = spinner;
            Categories = CashflowData.Categories
                .Where(x => type == TransactionType.Any || x.Type == type)
                .OrderBy(y => y.Type)
                .ThenBy(z => z.Name).ToList();

            if (spinner) Categories.Insert(0, null);
        }

        public override Category this[int position] => Categories[position];
        public override long GetItemId(int position) => Categories[position]?.Id ?? -1;
        public override int Count => Categories.Count;


        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            return GetCustomView(position, convertView, parent);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return GetCustomView(position, convertView, parent);
        }

        View GetCustomView(int position, View convertView, ViewGroup parent)
        {
            Category item = Categories[position];

            TextView view = (convertView ??
                             Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false)) as TextView;

            view?.SetText(item?.ToString() ?? "", TextView.BufferType.Normal);
            if (!Spinner) view?.SetTextColor(item?.Type == TransactionType.Income ? Color.Green : Color.Red);

            return view;
        }
    }
}