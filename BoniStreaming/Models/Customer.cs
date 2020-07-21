using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoniStreaming.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        [ForeignKey("MembershipTypeId")]
        public virtual MembershipType MembershipType { get; set; }

        [Required]
        [Display(Name = "Type of Membership")]
        public byte MembershipTypeId { get; set; }

        [Min18YearsIfAMember]
        [Display(Name = "Date of Birth")]
        public DateTime? Birthday { get; set; }

    }
}