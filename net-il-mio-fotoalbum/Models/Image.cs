using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace net_il_mio_fotoalbum.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Plase provide a Title.")]
        [StringLength(100, ErrorMessage = "Title must have less than 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Plase provide a Description.")]
        [Column(TypeName = "text")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Plase provide an Image url.")]
        public string Url { get; set; } = string.Empty;
        public bool Visible { get; set; } = false;

        public List<Category>? Categories { get; set; }

    }
}
