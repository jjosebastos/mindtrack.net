using mindtrack.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace mindtrack.Model
{
    [Table("T_MT_CHECK_IN_HUMOR")]
    public class CheckinHumor
    {


        [Key]
        [Column("ID_CHECKIN")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCheckin { get; set; }


        [Column("DS_COMENTARIO")] // Caso tenha um campo de texto livre
        [MaxLength(200)]
        public string Comentario { get; set; }


        [Required]
        [Column("ST_HUMOR")]
        [MaxLength(20)]
        public StatusHumorEnum StatusHumor { get; set; }

        [Column("DT_REGISTRO")]
        public DateTime DataRegistro { get; set; } = DateTime.Now;

        [Column("ID_USER")]
        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        [JsonIgnore]
        public User User { get; set; }
    }
}
