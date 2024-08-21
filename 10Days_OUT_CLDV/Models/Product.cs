using Azure;
using Azure.Data.Tables;
using System;
using System.ComponentModel.DataAnnotations;

namespace _10Days_OUT_CLDV.Models
{
    public class Product : ITableEntity
    {
        [Key]
        public int Product_Id { get; set; }  
        public string? Product_Name { get; set; }  
        public string? ProductDescription { get; set; }
        public string? ImageUrl { get; set; }
        public string? ProductCategory { get; set; }

        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }

    }
}
