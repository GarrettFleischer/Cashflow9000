using System;
using Android.Icu.Text;
using Java.Util.Jar;
using SQLite;
using SQLiteNetExtensions.Attributes;
using TeixeiraSoftware.Finance;

namespace Cashflow9000.Models
{
    public enum TransactionType { Expense, Income }
    
    public class Transaction
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }

        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }

        [ManyToOne]
        public Category Category { get; set; }

        [ForeignKey(typeof(Milestone))]
        public int MilestoneId { get; set; }

        [ManyToOne]
        public Milestone Milestone { get; set; }

        public string Note { get; set; }


        public override string ToString()
        {
            return $"{NumberFormat.CurrencyInstance.Format((double) Amount)} : {Note}";
        }
    }
}