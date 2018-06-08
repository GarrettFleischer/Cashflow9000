﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Cashflow9000.Fragments
{
    public abstract class ItemHandlerFragment<T> : Fragment, IDialogInterfaceOnClickListener
    {
        protected T Item;

        protected void DeleteItem(object sender, EventArgs eventArgs)
        {
            AlertDialog dlg = new AlertDialog.Builder(Activity).Create();
            dlg.SetTitle("Delete?");
            dlg.SetMessage("Are you sure you wish to delete?");
            dlg.SetButton(-1, "Yes", this);
            dlg.SetButton(-2, "No", this);
            dlg.Show();
        }

        protected void SaveItem(object sender, EventArgs eventArgs)
        {
            (Activity as IItemFragmentListener<T>)?.ItemSaved(Item);
            OnSave();
        }

        protected virtual void OnDelete() { }
        protected virtual void OnSave() { }

        public void OnClick(IDialogInterface dialog, int which)
        {
            dialog.Dismiss();
            if (which == -1)
            {
                (Activity as IItemFragmentListener<T>)?.ItemDeleted(Item);
                OnDelete();
            }
        }
    }
}