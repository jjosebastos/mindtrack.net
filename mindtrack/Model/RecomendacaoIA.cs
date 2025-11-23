using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mindtrack.Model
{

    [Table("T_MT_RECOMENDACAO_IA")]
    public class RecomendacaoIA
    {


        [Key]
        [Column("ID_RECOMENDACAO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRecomendacao { get; set; }

        [Required]
        [StringLength(2000)] 
        [Column("TX_RECOMENDACAO")]
        public string Texto { get; set; }

        [Required]
        [Column("DT_CRIACAO")]
        public DateTime DataCriacao { get; set; }

        [Required]
        [Column("ID_USER")]
        public int IdUser { get; set; }

        [Required]
        [Column("ID_CHECKIN")]
        public int IdCheckin { get; set; }
    }
}
