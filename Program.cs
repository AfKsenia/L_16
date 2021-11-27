using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json.Serialization;

namespace L_16
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "Products.json";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            const int n = 5;
            Product[] products = new Product[n];
            for (int i = 0; i < n; i++)
            {
                Product product = new Product();
                Console.WriteLine("Введите код товара");
                product.ProductCode = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите наименование товара");
                product.ProductName = Console.ReadLine();
                Console.WriteLine("Введите стоимость товара");
                product.ProductPrice = Convert.ToDouble(Console.ReadLine());
                products[i] = product;
            }
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            using (StreamWriter sw = new StreamWriter(path, false))
            {
                string jsonString = JsonSerializer.Serialize(products, options);
                sw.WriteLine(jsonString);
                sw.Close();
            }

            using (StreamReader sr = new StreamReader(path))
            {
                /*Console.WriteLine(sr.ReadToEnd());*/
                Product[] product = JsonSerializer.Deserialize<Product[]>(File.ReadAllText(path));

                double price = 0;
                string name = "";

                for (int i = 0; i < n; i++)
                {
                    if (product[i].ProductPrice > price)
                    {
                        price = product[i].ProductPrice;
                        name = product[i].ProductName;
                    }
                }
                Console.WriteLine("Самый дорогой товар - {0} \nЦена:{1}", name, price);
            }

            Console.ReadKey();
        }
        class Product
        {
            [JsonPropertyName("productCode")]
            public int ProductCode { get; set; }
            [JsonPropertyName("productName")]
            public string ProductName { get; set; }
            [JsonPropertyName("productPrice")]
            public double ProductPrice { get; set; }

        }
    }
}
