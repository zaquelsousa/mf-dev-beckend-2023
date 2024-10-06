using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_dev_beckend_2023.Models
{
    [Table("Consumos")]
    public class Consumo
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar a descrição")]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar data")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar a valor")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar a quilometragem")]
        public int Km { get; set; }

        [Display(Name = "Tipo de combustivel")]
        public TipoCombustivel Tipo { get; set; }

        //agora vamos fazer uma associação de um veiculo com consumo atravez do que podemos dizer que e a chave estrangeira
        [Display(Name = "Veiculo")]
        public int VeiculoID { get; set; }

        [ForeignKey("VeiculoID")]
        public Veiculo? Veiculo { get; set; }
    }

    public enum TipoCombustivel { 
        Gasolina,
        Etanol
    }
}
