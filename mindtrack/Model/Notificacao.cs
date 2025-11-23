using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mindtrack.Model
{

    [Table("T_MT_NOTIFICACAO")]
    public class Notificacao
    {

        [Key]
        [Column("ID_NOTIFICACAO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNotificacao { get; set; }


        [Required]
        [StringLength(200)]
        [Column("DS_MENSAGEM")]
        public string Mensagem {  get; set; }

        [Required]
        [StringLength(20)]
        [Column("TP_NOTIFICACAO")]
        public string TipoNotificacao { get; set; }


        [Required]
        [Column("DT_ENVIO")]
        public DateTime DataEnvio {  get; set; }

        [Required]
        [Column("ID_USER")]
        public int IdUser { get; set; }


    }
}
