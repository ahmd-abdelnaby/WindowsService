using Model.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VictoryAPI.Models
{
    public class Request:BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }
        public int MobileNumber { get; set; }
        public DateTime ReuestDate { get; set; }
    }
}
