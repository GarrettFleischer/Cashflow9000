using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cashflow9000.Models;
using SQLite;

namespace Cashflow9000
{
    public static class CashflowData
    {
        private static readonly string DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cashflow.db3");
        private static readonly SQLiteConnection DB = new SQLiteConnection(DBPath);

        public static bool TableExists<T>(SQLiteConnection connection)
        {
            const string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name=?";
            var cmd = connection.CreateCommand(cmdText, typeof(T).Name);
            return cmd.ExecuteScalar<string>() != null;
        }

        public static void Initialize()
        {
            if (!TableExists<Transaction>(DB)) DB.CreateTable<Transaction>();
            if (!TableExists<Milestone>(DB)) DB.CreateTable<Milestone>();

            // ReSharper disable once InvertIf
            if (!TableExists<Category>(DB))
            {
                DB.CreateTable<Category>();
                if (!DB.Table<Category>().Any())
                {
                    DB.InsertAll(new List<Category>
                    {
                        new Category {Name = "Rent", Type = TransactionType.Expense},
                        new Category {Name = "Groceries", Type = TransactionType.Expense},
                        new Category {Name = "Fuel", Type = TransactionType.Expense},
                        new Category {Name = "Utilities", Type = TransactionType.Expense},
                        new Category {Name = "Wages", Type = TransactionType.Income},
                        new Category {Name = "Investments", Type = TransactionType.Income},
                    });
                }
            }
        }

        public static List<Transaction> Transactions => DB.Table<Transaction>().ToList();
        public static Transaction Transaction(int id) => DB.Find<Transaction>(t => t.Id == id);
        public static int Update(Transaction transaction) => DB.Update(transaction);
        public static int Delete(Transaction transaction) => DB.Delete(transaction);

        public static List<Category> Categories => DB.Table<Category>().ToList();
        public static Category Category(int id) => DB.Find<Category>(t => t.Id == id);
        public static int Update(Category category) => DB.Update(category);
        public static int Delete(Category category) => DB.Delete(category);

        public static List<Milestone> Milestones => DB.Table<Milestone>().ToList();
        public static Milestone Milestone(int id) => DB.Find<Milestone>(t => t.Id == id);
        public static int Update(Milestone milestone) => DB.Update(milestone);
        public static int Delete(Milestone milestone) => DB.Delete(milestone);
    }
}