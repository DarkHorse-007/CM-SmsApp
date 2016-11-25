using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageApp.Models
{
    [Table("CountryDialingCodes")]
    public class CountryCode
    {
        [Key]
        public short Code { get; set; }
        public string Abbrevation { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return string.Format("(+{0})-{1}", Code, Abbrevation);
        }
    }
}
