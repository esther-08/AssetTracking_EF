using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace AssetTracking_EF
{
    class Program
    {
        static void Main(string[] args)
        {
            // Level 1
            List<CompanyAsset> myAssets = new List<CompanyAsset>();
            LaptopComputer pc1 = new LaptopComputer("MacBook", 3341, new DateTime(2018, 12, 12), "Uganda");
            LaptopComputer pc2 = new LaptopComputer("Asus", 2950, new DateTime(2018, 1, 12), "Sweden");
            LaptopComputer pc3 = new LaptopComputer("Lenovo", 999, new DateTime(2019, 12, 12), "USA");

            List<MobilePhone> myPhones = new List<MobilePhone>();
            MobilePhone phone1 = new MobilePhone("Iphone", 1099, new DateTime(2011, 12, 12), "Uganda");
            MobilePhone phone2 = new MobilePhone("Samsung", 1299, new DateTime(2018, 3, 12), "USA");
            MobilePhone phone3 = new MobilePhone("Nokia", 699, new DateTime(2018, 4, 12), "Sweden");

            myAssets.Add(pc1);
            myAssets.Add(phone1);
            myAssets.Add(pc3);
            myAssets.Add(phone2);
            myAssets.Add(pc2);
            myAssets.Add(phone3);

            // Level 2
            myAssets = myAssets.OrderBy(myAssets => myAssets.GetType().ToString()).ToList();

            Console.WriteLine("\nLEVEL2 \nOrder by Type");
            Console.WriteLine("ModelName   Price  PurchaseDate Location");
            foreach (CompanyAsset asset in myAssets)
            {
                Console.WriteLine(asset.ModelName.PadRight(10) + asset.Price.ToString().PadLeft(7) + "  " + asset.PurchaseDate.ToString("MM/dd/yyyy").PadRight(10) + "   " + asset.Office.PadRight(10));
            }

            myAssets = myAssets.OrderBy(myAssets => myAssets.PurchaseDate).ToList();

            Console.WriteLine("\nOrder by Purchase Date");
            Console.WriteLine("ModelName   Price  PurchaseDate Location");
            DateTime nowDate = DateTime.Now;
            TimeSpan assetLifeSpan;

            foreach (CompanyAsset asset in myAssets)
            {
                assetLifeSpan = nowDate - asset.PurchaseDate;
                ColorConsole(assetLifeSpan);
                Console.WriteLine(asset.ModelName.PadRight(10) + asset.Price.ToString().PadLeft(7) + "  " + asset.PurchaseDate.ToString("MM/dd/yyyy").PadRight(10) + "   " + asset.Office.PadRight(10));
            }

            // Level 3
            myAssets = myAssets.OrderBy(myAssets => myAssets.Office).ToList();
            TimeSpan soonEoLSpan6 = new TimeSpan(1005, 0, 0, 0, 0);

            Console.WriteLine("\nLEVEL3 \nOrder by Office");
            Console.WriteLine("ModelName        Price     PurchaseDate Location");
            CultureInfo cultureInfo = new CultureInfo("en-US");
            string price;
            foreach (CompanyAsset asset in myAssets)
            {
                assetLifeSpan = nowDate - asset.PurchaseDate;

                if (asset.Office == "Sweden")
                {
                    // Convert dollars to SEK first
                    price = (asset.Price * 8.23).ToString("C", CultureInfo.CreateSpecificCulture("sv-SE"));
                }
                else if (asset.Office == "Uganda")
                {
                    // Convert dollars to Ugx first
                    price = (asset.Price * 3700).ToString("C", CultureInfo.CreateSpecificCulture("en-UG"));
                }
                else
                {
                    // Default price in dollars
                    price = asset.Price.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
                }

                ColorConsole(assetLifeSpan);
                Console.WriteLine(asset.ModelName.PadRight(10) + price.PadLeft(15) + "  " + asset.PurchaseDate.ToString("MM/dd/yyyy").PadRight(10) + "   " + asset.Office.PadRight(10));
            }

            Console.ReadLine();
        }

        static void ColorConsole(TimeSpan assetLifeSpan)
        {
            // 1005 is 2 years and 9 months  = (365*3)-(30*3) days
            TimeSpan soonEoLSpan3 = new TimeSpan(1005, 0, 0, 0, 0);
            // 915 is 2 years and 6 months  = (365*3)-(30*6) days
            TimeSpan soonEoLSpan6 = new TimeSpan(915, 0, 0, 0, 0);
            if (assetLifeSpan > soonEoLSpan3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (assetLifeSpan > soonEoLSpan6)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

    internal class AssetTrackorContext : DbContext
    {
        public DbSet<CompanyAsset> CompanyAssets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-Q3LBSV4\SQLEXPRESS;Initial Catalog=AssetTrackor;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
    
}
