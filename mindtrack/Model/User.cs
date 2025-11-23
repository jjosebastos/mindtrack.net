using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mindtrack.Model
{
    [Table("T_MT_USER")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_user")] 
        public int IdUser { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Column("nm_completo")]
        [MaxLength(50)]        
        public string Nome { get; set; }

        [Required]
        [Column("ad_email")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [Column("vl_senha")]
        [MaxLength(72)] 
        public string Senha { get; set; }

        [Required]
        [Column("ds_setor")]
        [MaxLength(40)]
        public string Setor { get; set; }

        [Required]
        [Column("ds_cargo")]
        [MaxLength(40)]
        public string Cargo { get; set; }

        [Required]
        [Column("dt_admissao")]
        public DateTime DataAdmissao { get; set; }

  
    }
}