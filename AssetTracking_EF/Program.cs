using System;

namespace AssetTracking_EF
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseMethods databaseOperations = new DatabaseMethods();
            // Create
            Console.WriteLine("\nCREATE: Assets are added by the program");
            databaseOperations.Create();

            // Read and Report
            Console.WriteLine("\nREAD - 1: (Prices in Dollars) \nOrder by Type");
            databaseOperations.ReadAndReportLevel2();

            // Read and Report
            Console.WriteLine("\nREAD - 2: (Prices in Location Currency) \nOrder by Office");
            databaseOperations.ReadAndReportLevel3();

            // Update, Read and Report
            Console.WriteLine("\nUPDATE: Office for MacBook and Rerun the previous query to see changes");
            databaseOperations.Update();
            databaseOperations.ReadAndReportLevel3();

            // Delete, Read and Report
            Console.WriteLine("\nDELETE:  Asset called Nokia and Rerun the previous query to see changes");
            databaseOperations.Delete();
            databaseOperations.ReadAndReportLevel3();

            Console.ReadLine();
    }   }
}
