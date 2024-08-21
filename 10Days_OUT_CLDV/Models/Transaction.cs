using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using Azure;
using Azure.Data.Tables;

namespace _10Days_OUT_CLDV.Models
{
    public class Transaction : ITableEntity
    {
        [Key]
        public int Transaction_Id { get; set; }

        //

        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        //
        
        [Required(ErrorMessage = "Choose a customer!")]
        public int Customer_ID { get; set; } 

        [Required(ErrorMessage = "Pick a valid product.")]
        public int Product_ID { get; set; } 

        [Required(ErrorMessage = "Select a date within valid range.")]
        public DateTime Transaction_Date { get; set; }

        [Required(ErrorMessage = "Pick a valid category.")]
        public string? Transaction_Category { get; set; }

    }
}
