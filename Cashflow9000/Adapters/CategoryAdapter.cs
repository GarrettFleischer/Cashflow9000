using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
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

        public CategoryAdapter(Activity context, TransactionType type)
        {
            Context = context;
            Categories = CashflowData.Categories
                .Where(x => x.Type == type || type == TransactionType.Any)
                .OrderBy(y => y.Type)
                .ThenBy(z => z.Name).ToList();
        }

        public override Category this[int position] => Categories[position];
        public override long GetItemId(int position) => Categories[position].Id ?? -1;
        public override int Count => Categories.Count;
        

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = (convertView ??
                        Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false)) as TextView;

            // Get our object for position
            Category item = Categories[position];
            view?.SetText(item.ToString(), TextView.BufferType.Normal);

            //Finally return the view
            return view;
        }
    }
}