using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Plase provide an Email.")]
        [EmailAddress(ErrorMessage = "Incorrect Email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Plase provide a Message.")]
        [Column(TypeName = "text")]
        public string MessageText { get; set; } = string.Empty;
    }
}
