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
using SQLiteNetExtensions.Attributes;

namespace Cashflow9000.Models
{

    public class PlannedTransaction : Transaction
    {
        [ForeignKey(typeof(Recurrence))]
        public int? RecurrenceId { get; set; }

        [ManyToOne]
        public Recurrence Recurrence { get; set; }
    }
}