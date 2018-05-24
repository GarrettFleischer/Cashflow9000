using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util.Jar;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Cashflow9000.Models
{
    public enum RecurrenceType { None, Daily, Weekly, Biweekly, Monthly, Quarterly, Annually }

    public class Recurrence
    {
        private static readonly Dictionary<RecurrenceType, int> Mapping = new Dictionary<RecurrenceType, int>
        {
            { RecurrenceType.None, Resource.String.none },
            { RecurrenceType.Daily, Resource.String.daily },
            { RecurrenceType.Weekly, Resource.String.weekly },
            { RecurrenceType.Biweekly, Resource.String.biweekly },
            { RecurrenceType.Monthly, Resource.String.monthly },
            { RecurrenceType.Quarterly, Resource.String.quarterly },
            { RecurrenceType.Annually, Resource.String.annually },
        };

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int? Id { get; set; }

        [Ignore]
        public int StringId => Mapping[Type];

        public RecurrenceType Type { get; set; }

        [OneToMany]
        public List<Budget> Budgets { get; set; }

        [OneToMany]
        public List<PlannedTransaction> PlannedPayments { get; set; }
    }
}