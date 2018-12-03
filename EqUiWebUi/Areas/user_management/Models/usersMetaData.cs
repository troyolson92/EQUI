using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EqUiWebUi.Areas.user_management.Models
{
    [MetadataType(typeof (usersMetaData))]
    public partial class users
    {
        //containts a list of Locationtree type that is sets by ResponsibleArea field from user profile
        public List<string> ResponsibleAreaLocations { get; set; }
    }

    public class usersMetaData
    {
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string username { get; set; }

        [StringLength(100,MinimumLength =3)]
        [Required]
        public string LocationRoot { get; set; }

//      [StringLength(100, MinimumLength = 0, ErrorMessage ="Must at least be empty string")]
//      [Required]
        public string AssetRoot { get; set; }
    }
}