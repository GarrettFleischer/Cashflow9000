using System;
using Android.Icu.Text;
using Java.Util.Jar;
using Javax.Crypto;
using SQLite;
using SQLiteNetExtensions.Attributes;
using TeixeiraSoftware.Finance;

namespace Cashflow9000.Models
{
    public enum TransactionType { Expense, Income, Any }
    
    public class Transaction
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int? Id { get; set; }

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        [Ignore]
        public decimal Value => Amount * (Type == TransactionType.Expense ? -1 : 1);

        public DateTime Date { get; set; }

        public string Note { get; set; }

        [ForeignKey(typeof(Category))]
        public int? CategoryId { get; set; }

        [ManyToOne]
        public Category Category { get; set; }

        [ForeignKey(typeof(Milestone))]
        public int? MilestoneId { get; set; }

        [ManyToOne]
        public Milestone Milestone { get; set; }
        
        public override string ToString()
        {
            string description = $"{Milestone?.Name}";
            if (Category != null && Milestone != null) description = $"{Category.Name}, {Milestone.Name}";
            else if (Category != null) description = $"{Category.Name}";
            return $"{NumberFormat.CurrencyInstance.Format((double)Value)} {description}\n\t{Note}";
        }
    }
}