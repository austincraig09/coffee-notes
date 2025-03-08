using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SQLite;
using SQLitePCL;

namespace CoffeeNotesApp2
{
    public static class CoffeeNoteDatabase
    {
        private static SQLiteAsyncConnection? _database;

        public static async Task InitializeDatabaseAsync()
        {
            if (_database == null)
            {
                Batteries_V2.Init();
                _database = new SQLiteAsyncConnection(
                    Constants.DatabasePath,
                    SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache
                );

                await _database.CreateTableAsync<CoffeeNote>();
            }
        }

        public static async Task<List<CoffeeNote>> GetNotesAsync()
        {
            try
            {
                if (_database == null)
                {
                    throw new InvalidOperationException("Database not initialized.");
                }
                return await _database.Table<CoffeeNote>().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving notes: {ex.Message}");
                return new List<CoffeeNote>();
            }
        }

        public static async Task SaveNoteAsync(CoffeeNote note)
        {
            try
            {
                if (_database == null)
                {
                    throw new InvalidOperationException("Database not initialized.");
                }
                await _database.InsertAsync(note);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving note: {ex.Message}");
            }
        }
    }
}
