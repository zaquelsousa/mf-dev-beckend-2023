using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_dev_beckend_2023.Models
{
    //data anotation pra criar a tabela, se nao colocar ele cria no ingrez so q fica merda porcausa preguiça de explicar
    [Table("Veiculos")]
    public class Veiculo
    {
        //cheve primaria auto incremento
        [Key]
        public int ID { get; set; }
        //menor se é burro? vou ter que explicar todo agr vai poluir o codigo com cometario desnecessario
        [Required(ErrorMessage = "Obrigario informa o nome!")]
        public string? NomeVeiculo { get; set; }

        [Required(ErrorMessage = "Obrigario informa a placa!")]
        public string? VeiculoPlaca { get; set; }

        [Required(ErrorMessage = "Obrigario informa o ano de fabricação!")]
        public int AnoFabricacao { get; set; }

        [Required(ErrorMessage = "Obrigario informa ano do modelo!")]
        public int AnoModelo { get; set; }
    }
}
