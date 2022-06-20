using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyApi.Data.Entities.Base;

namespace MyApi.Data.Entities.Models
{
    [Table("RequestsLog")]
    public class RequestsLog : BaseEntity
    {
        public string? Text { get; set; }
        public string? Response { get; set; }
        public string? RandomText { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}