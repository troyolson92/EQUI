using System.ComponentModel.DataAnnotations;

namespace EqUiWebUi.Areas.user_management.Models
{
    [MetadataType(typeof (usersMetaData))]
    public partial class users
    {
    }

    public class usersMetaData
    {
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string username { get; set; }

        [StringLength(100,MinimumLength =3)]
        [Required]
        public string LocationRoot { get; set; }

        //[StringLength(100, MinimumLength = 1, ErrorMessage ="Must at least be ' '")]
        [Required]
        public string AssetRoot { get; set; }
    }
}