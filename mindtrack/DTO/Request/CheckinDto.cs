using mindtrack.Enums;
using mindtrack.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace mindtrack.DTO.Request
{
    public class CheckinDto
    {

        
        
        [Required(ErrorMessage = "O status de humor é obrigatório.")]
        [EnumDataType(typeof(StatusHumorEnum), ErrorMessage = "O status de humor informado é inválido.")]
        public StatusHumorEnum StatusHumor { get; set; }

        [StringLength(200, ErrorMessage = "O comentário não pode exceder 200 caracteres.")]
        public string Comentario { get; set; }

        public int IdUser { get; set; }

        [JsonIgnore]
        public DateTime DataRegistro { get; set; } = DateTime.Now;

    }
}
