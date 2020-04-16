using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    public partial class InfoCode
    {
        [Key, Required]
        public int InfoCodeId { get; set; }

        [StringLength(5, ErrorMessage = Message.CodeLength5Error)]
        [Required]
        public string Code { get; set; }

        [StringLength(100, ErrorMessage = Message.NameLengthError)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
