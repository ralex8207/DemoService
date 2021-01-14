using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoService.Ef;

namespace DemoService.Entities {

    [Table(nameof(Users))]
    public class Users : EntityKey<int> {

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255)]
        public string Name { get; set; }
    }
}