using System;
using System.Diagnostics;
using System.IO;

public static class Constants
{
    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, "coffeenotesdb.sqlite3");

    public static void LogDatabasePath()
    {
        Debug.WriteLine($"Database Path: {DatabasePath}");
    }
}
