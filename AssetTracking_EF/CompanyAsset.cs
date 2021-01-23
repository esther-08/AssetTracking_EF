using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracking_EF
{
    class LaptopComputer : CompanyAsset
    {
        public LaptopComputer(string modelName, int price, DateTime purchaseDate, string office) : base(modelName, price, purchaseDate, office)
        {
            ModelName = modelName;
            Price = price;
            PurchaseDate = purchaseDate;
            AssetType = "Laptop";
        }

    }
    class MobilePhone : CompanyAsset
    {
        public MobilePhone(string modelName, int price, DateTime purchaseDate, string office) : base(modelName, price, purchaseDate, office)
        {
            ModelName = modelName;
            Price = price; // in dollars
            PurchaseDate = purchaseDate;
            AssetType = "Phone";
        }
    }
    class CompanyAsset
    {
        public CompanyAsset(string modelName, int price, DateTime purchaseDate, string office)
        {

            AssetType = "";
            ModelName = modelName;
            Price = price;
            PurchaseDate = purchaseDate;
            Office = office;
        }
        public int ID { get; set; }
        public string AssetType { get; set; }
        public string ModelName { get; set; }
        public int Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Office { get; set; }

    }
}
