using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mindtrack.Model
{
    [Table("T_MT_USER")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_USER")] 
        public int IdUser { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Column("NM_COMPLETO")]
        [MaxLength(50)]        
        public string Nome { get; set; }

        [Column("DS_GENERO")]
        [Required(ErrorMessage = "O gênero é obrigatório")]
        public string Genero { get; set; }

        [Required]
        [Column("AD_EMAIL")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [Column("VL_SENHA")]
        [MaxLength(72)] 
        public string Senha { get; set; }

        [Required]
        [Column("DS_SETOR")]
        [MaxLength(40)]
        public string Setor { get; set; }

        [Required]
        [Column("DS_CARGO")]
        [MaxLength(40)]
        public string Cargo { get; set; }

        [Required]
        [Column("DT_ADMISSAO")]
        public DateTime DataAdmissao { get; set; }

  
    }
}