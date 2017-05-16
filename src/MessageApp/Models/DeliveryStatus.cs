using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageApp.Models
{
    public enum DeliveryStatusCode
    {
        Undefined = 100,
        Delivered = 200,
        NotDelivered = 300,
        ClientError = 400,
        ServerError = 500,
    }

    [Table("Messages")]
    public class DeliveryStatus
    {
        [Column("DeliveryStatusCode")]
        public DeliveryStatusCode Code { get; set; }

        [Column("DeliveryStatusMessage")]
        public string Text { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageID { get; set; }
    }
}
