using Project.Data.Models.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    public partial class Users : ActiveCreateDeleteUpdate
    {
        [Key, Required]
        public long UserId { get; set; }

        [StringLength(5, ErrorMessage = Message.CodeLength5Error)]
        [Required]
        public string InfoCode { get; set; }

        [Required]
        public long InfoId { get; set; }

        [StringLength(100, ErrorMessage = Message.NameLengthError)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = Message.NameLengthError)]
        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = Message.NameLengthError)]
        public string UserName { get; set; }

        [PasswordValidate]
        [Required]
        public string Password { get; set; }

        public int UserTypeId { get; set; }

        [StringLength(5, ErrorMessage = Message.CodeLength5Error)]
        public string UserTypeCode { get; set; }

        public bool IsEmailVerified { get; set; }
        public DateTime? EmailVerifiedOn { get; set; }
        public bool IsPhoneVerified { get; set; }
        public DateTime? PhoneVerifiedOn { get; set; }
        public bool IsBlocked { get; set; }
        public string Comment { get; set; }
    }
}
