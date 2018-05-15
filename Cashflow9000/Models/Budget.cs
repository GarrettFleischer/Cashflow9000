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

namespace Cashflow9000.Models
{
    public enum Recurrance { None, Daily, Weekly, Biweekly, Monthly, Quarterly, Annually}

    public class Budget
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int? Id { get; set; }

        public string Name { get; set; }

        public Recurrance Recurrance { get; set; }
        
        [ForeignKey(typeof(Category))]
        private int? CategoryId { get; set; }

        [ManyToOne]
        public Category Category { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}