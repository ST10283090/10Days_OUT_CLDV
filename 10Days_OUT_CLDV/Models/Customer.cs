using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace _10Days_OUT_CLDV.Models
{
    public class Customer : ITableEntity 
    {
        [Key]
        public int Customer_Id { get; set; }  
        public string? Customer_Name { get; set; }  
        public string? Customer_Email { get; set; }
        public string? Customer_Password { get; set; }

        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }

    }
}
