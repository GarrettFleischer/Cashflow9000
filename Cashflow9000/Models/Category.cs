using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Cashflow9000.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public TransactionType Type { get; set; }

        [OneToMany]
        public List<Transaction> Transactions { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}