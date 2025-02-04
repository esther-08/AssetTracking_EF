﻿using System;
using System.Linq;
using System.Globalization;

namespace AssetTracking_EF
{
    class DatabaseMethods
    {
        AssetTrackorContext _assetTrackorContext = new AssetTrackorContext();

        static void ColorConsole(TimeSpan assetLifeSpan)
        {
            //1005 is 2 years and 9 months  = (365*3)-(30*3) days
            TimeSpan soonEoLSpan3 = new TimeSpan(1005, 0, 0, 0, 0);
            //915 is 2 years and 6 months  = (365*3)-(30*6) days
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

        static void WriteHeadingToConsole()
        {
            Console.WriteLine("Type        ModelName           Price  PurchaseDate Location");
            Console.WriteLine("----------  ----------          -----  ------------ --------");
        }
        static void WriteItemToConsole(CompanyAsset item, string price)
        {
            if (price == "")
            {
                // Convert CompanyAsset price to a string
                price = item.Price.ToString();
            }
            Console.WriteLine(
                item.AssetType.PadRight(10) + "  "
                + item.ModelName.PadRight(11)
                + price.PadLeft(14) + "  "
                + item.PurchaseDate.ToString("MM/dd/yyyy").PadRight(10) + "   "
                + item.Office.PadRight(10));
        }
        public void Create()
        {
            LaptopComputer pc1 = new LaptopComputer("MacBook", 3341, new DateTime(2018, 12, 12), "Uganda");
            LaptopComputer pc2 = new LaptopComputer("Asus", 2950, new DateTime(2018, 1, 12), "Sweden");
            LaptopComputer pc3 = new LaptopComputer("Lenovo", 999, new DateTime(2019, 12, 12), "USA");

            MobilePhone phone1 = new MobilePhone("Iphone", 1099, new DateTime(2011, 12, 12), "Uganda");
            MobilePhone phone2 = new MobilePhone("Samsung", 1299, new DateTime(2018, 3, 12), "USA");
            MobilePhone phone3 = new MobilePhone("Nokia", 699, new DateTime(2018, 4, 12), "Sweden");

            _assetTrackorContext.CompanyAssets.Add(pc1);
            _assetTrackorContext.SaveChanges();

            _assetTrackorContext.CompanyAssets.Add(phone1);
            _assetTrackorContext.SaveChanges();

            _assetTrackorContext.CompanyAssets.Add(pc3);
            _assetTrackorContext.SaveChanges();

            _assetTrackorContext.CompanyAssets.Add(phone2);
            _assetTrackorContext.SaveChanges();

            _assetTrackorContext.CompanyAssets.Add(pc2);
            _assetTrackorContext.SaveChanges();

            _assetTrackorContext.CompanyAssets.Add(phone3);
            _assetTrackorContext.SaveChanges();
        }
        public void ReadAndReportLevel2()
        {
            var query = from item in _assetTrackorContext.CompanyAssets
                        orderby item.AssetType ascending
                        select item;

            WriteHeadingToConsole();
            foreach (var item in query)
            {
                WriteItemToConsole(item, "");
            }

            query = from item in _assetTrackorContext.CompanyAssets
                    orderby item.PurchaseDate ascending
                    select item;

            Console.WriteLine("\nOrder by Purchase Date");
            WriteHeadingToConsole();
            DateTime nowDate = DateTime.Now;
            TimeSpan assetLifeSpan;

            foreach (var item in query)
            {
                assetLifeSpan = nowDate - item.PurchaseDate;
                ColorConsole(assetLifeSpan);
                WriteItemToConsole(item, "");
            }
            // Restore console to normal
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ReadAndReportLevel3()
        {
            DateTime nowDate = DateTime.Now;
            TimeSpan assetLifeSpan;
            string price;

            var query = from item in _assetTrackorContext.CompanyAssets
                        orderby item.Office ascending
                        select item;

            WriteHeadingToConsole();
            foreach (var item in query)
            {
                assetLifeSpan = nowDate - item.PurchaseDate;

                if (item.Office == "Sweden")
                {
                    // Convert dollars to SEK first
                    price = (item.Price * 8.23).ToString("C", CultureInfo.CreateSpecificCulture("sv-SE"));
                }
                else if (item.Office == "Uganda")
                {
                    // Convert dollars to Ugx first
                    price = (item.Price * 3700).ToString("C", CultureInfo.CreateSpecificCulture("en-UG"));
                }
                else
                {
                    // Default price in dollars
                    price = item.Price.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
                }

                ColorConsole(assetLifeSpan);
                WriteItemToConsole(item, price);
            }
            // Restore console to normal
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Update()
        {
            // Returns first element that was added to database (Lowest ID),  in this case MacBook
            CompanyAsset assetUpdate = _assetTrackorContext.CompanyAssets.FirstOrDefault();
            assetUpdate.Office = "Sweden";
            _assetTrackorContext.SaveChanges();
        }

        public void Delete()
        {
            // Returns first element called Nokia to database 
            CompanyAsset assetDelete = _assetTrackorContext.CompanyAssets.Where(asset => asset.ModelName == "Nokia").FirstOrDefault();
            _assetTrackorContext.CompanyAssets.Remove(assetDelete);
            _assetTrackorContext.SaveChanges();
        }
    }
}
