﻿using System;
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
using SQLiteNetExtensions.Extensions;

namespace Cashflow9000
{
    public static class CashflowData
    {
        private static readonly string DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cashflow.db3");
        private static readonly SQLiteConnection DB = new SQLiteConnection(DBPath);

        public static bool TableExists<T>(SQLiteConnection connection)
        {
            const string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name=?";
            SQLiteCommand cmd = connection.CreateCommand(cmdText, typeof(T).Name);
            return cmd.ExecuteScalar<string>() != null;
        }

        public static void Initialize()
        {
            if (!TableExists<Budget>(DB)) DB.CreateTable<Budget>();
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

        // Generic modifiers
        public static void InsertOrReplace<T>(T item) => DB.InsertOrReplaceWithChildren(item);
        public static void Update<T>(T item) => DB.UpdateWithChildren(item);
        public static int Delete<T>(T item) => DB.Delete(item);


        // Specific getters
        public static List<Transaction> Transactions => DB.GetAllWithChildren<Transaction>(null, true);
        public static Transaction Transaction(int id) => DB.GetWithChildren<Transaction>(id, true);

        public static List<Category> Categories => DB.GetAllWithChildren<Category>(null, true);
        public static Category Category(int id) => DB.GetWithChildren<Category>(id, true);

        public static List<Milestone> Milestones => DB.GetAllWithChildren<Milestone>(null, true);
        public static Milestone Milestone(int id) => DB.GetWithChildren<Milestone>(id, true);

        public static List<Budget> Budgets => DB.GetAllWithChildren<Budget>(null, true);
        public static Budget Budget(int id) => DB.GetWithChildren<Budget>(id, true);
    }
}