using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_il_mio_fotoalbum.Models
{
    public class ImageFormModel
    {
        public Image Image { get; set; } = new Image { Url = "https://picsum.photos/200/300" };
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        public List<string> SelectedCategory { get; set; } = new();

    }
}
