using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageApp.Models
{
    [Table("Messages")]
    public class Message
    {
        [Required(ErrorMessage = "Name is required") ]
        [MaxLength(11, ErrorMessage = "Name should not be more than 11 characters")]
        public string From { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{4,10}$", ErrorMessage = "Invalid phone number")]
        public Double To { get; set; }

        [Required(ErrorMessage = "Country dialing code is required")]
        public short CountryCode { get; set; }

        public int DCS { get; set; }

        public bool IsMultipart { get; set; }

        [Required(ErrorMessage = "Message text is required")]
        public string Body { get; set; }

        [ForeignKey("MessageID")]
        public DeliveryStatus Status { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageID { get; set; }

        public DateTime Timestamp { get; set; }

        public string FormattedPhoneNumber
        {
            get
            {
                string countrucode = CountryCode < 10 ? CountryCode.ToString().PadLeft(2, '0') : CountryCode.ToString();
                return string.Format("({0}) {1}", countrucode, To);
            }
        }

        public override bool Equals(object obj)
        {
            Message m = obj as Message;
            if(m == null)
            {
                return false;
            }
            else
            {
                return m.MessageID.Equals(this.MessageID);
            }
        }

        public bool Validate()
        {
            List<ValidationResult> Results = new List<ValidationResult>();
            bool ValidationResult = Validator.TryValidateObject(this, new ValidationContext(this), Results , true);

            if ( !ValidationResult || (ValidationResult && !IsMultipart && Body.Length > 160) )
            {
                return false;
            }

            return true;
        }
    }
}
