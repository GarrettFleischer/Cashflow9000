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
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Cashflow9000.Models
{

    public class Budget
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int? Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        [ForeignKey(typeof(Recurrence))]
        public int? RecurrenceId { get; set; }

        [ManyToOne]
        public Recurrence Recurrence { get; set; }
        
        [ForeignKey(typeof(Category))]
        public int? CategoryId { get; set; }

        [ManyToOne]
        public Category Category { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}