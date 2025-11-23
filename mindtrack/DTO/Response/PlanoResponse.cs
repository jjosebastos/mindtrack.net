using mindtrack.DTO.Shared;
using System.ComponentModel.DataAnnotations;

namespace mindtrack.DTO.Response
{
    public class PlanoResponse
    {
        public int IdPlano { get; set; }        
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public DateTime DataInicio { get; set; }      
        public DateTime DataFim { get; set; }
        public int IdUser { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
        
    }
}
