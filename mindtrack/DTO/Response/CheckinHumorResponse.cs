using mindtrack.DTO.Shared;
using mindtrack.Enums;
using mindtrack.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mindtrack.DTO.Response
{
    public class CheckinHumorResponse
    {

        public int IdCheckin { get; set; }
        public string Comentario { get; set; }

        public StatusHumorEnum StatusHumor { get; set; }

        public DateTime DataRegistro { get; set; } = DateTime.Now;

        public int IdUser { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
