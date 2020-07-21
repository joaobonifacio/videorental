using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoniStreaming.Models
{
    public class MembershipType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public byte Id { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        [Display(Name = "Type of Membership")]
        [StringLength(255)]
        [Required]
        public string TypeName { get; set; }

        public static readonly byte UNKNOWN = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}