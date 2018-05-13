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
using SQLite;
using SQLiteNetExtensions.Attributes;
using TeixeiraSoftware.Finance;

namespace Cashflow9000.Models
{
    public class Milestone
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        [OneToMany]
        public List<Transaction> Transactions { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}