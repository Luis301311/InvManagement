using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ProductsJson
    {
        public class AccountGroup
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class AdditionalFields
        {
            public string Barcode { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Tariff { get; set; }
        }

        public class Metadata
        {
            public DateTime Created { get; set; }
        }

        public class Tax
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Percentage { get; set; }
            public string Type { get; set; }
        }

        public class Unit
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class Warehouse
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Quantity { get; set; }
        }

        public class ProductResult
        {
            public AccountGroup AccountGroup { get; set; }
            public bool Active { get; set; }
            public AdditionalFields AdditionalFields { get; set; }
            public double AvailableQuantity { get; set; }
            public string Code { get; set; }
            public string Id { get; set; }
            public Metadata Metadata { get; set; }
            public string Name { get; set; }
            public string Reference { get; set; }
            public bool StockControl { get; set; }
            public string TaxClassification { get; set; }
            public int TaxConsumptionValue { get; set; }
            public bool TaxIncluded { get; set; }
            public List<Tax> Taxes { get; set; }
            public string Type { get; set; }
            public Unit Unit { get; set; }
            public string UnitLabel { get; set; }
            public List<Warehouse> Warehouses { get; set; }
        }

        public class ProductResponse
        {
            public List<ProductResult> Results { get; set; }
        }
    }
}
 