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

namespace Cashflow9000.Fragments
{
    public interface IModelListener<T>
    {
        void OnChange<T>(T item);
    }
}