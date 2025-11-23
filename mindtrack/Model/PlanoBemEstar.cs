using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mindtrack.Model
{
    [Table("T_MT_PLANO_BEM_ESTAR")]
    public class PlanoBemEstar
    {

        [Key]
        [Column("ID_PLANO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPlano { get; set; }

        [Required]
        [StringLength(40)]
        [Column("DS_TITULO")]
        public string Titulo { get; set; }

        [Required]
        [StringLength(200)]
        [Column("DS_PLANO")]
        public string Descricao { get; set; }

        [Required]
        [StringLength(10)]
        [Column("ST_PLANO")] // Ex: "Ativo", "Concluído", "Pendente"
        public string Status { get; set; }

        [Required]
        [Column("DT_INICIO")]
        public DateTime DataInicio { get; set; }

        [Column("DT_FIM")]
        public DateTime DataFim { get; set; }

        [Required]
        [Column("ID_USER")]
        public int IdUser { get; set; }


    }
}
