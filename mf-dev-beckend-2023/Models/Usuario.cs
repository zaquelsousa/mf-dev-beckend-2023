using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_dev_beckend_2023.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int  ID { get; set; }
        [Required(ErrorMessage = "deve informar o nome")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "deve informar a senha")]
        [DataType(DataType.Password)]
        public string? senha { get; set; }

        [Required(ErrorMessage ="deve informar o tipo de usuario")]
        public Perfil perfil { get; set; } 
    }

    public enum Perfil { 
        Admin,
        User
    }
}
