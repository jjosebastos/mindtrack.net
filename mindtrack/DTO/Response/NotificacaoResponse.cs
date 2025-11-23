using mindtrack.DTO.Shared;
using System.ComponentModel.DataAnnotations;

namespace mindtrack.DTO.Response
{
    public class NotificacaoResponse
    {

        public int IdNotificacao { get; set; }
        public string Mensagem { get; set; }
        public string TipoNotificacao { get; set; }
        public DateTime DataEnvio { get; set; }
        public int IdUser { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
