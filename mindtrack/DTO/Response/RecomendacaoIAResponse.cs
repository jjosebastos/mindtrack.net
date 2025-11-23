using mindtrack.DTO.Shared;

namespace mindtrack.DTO.Response
{
    public class RecomendacaoIAResponse 
    {
        public int IdRecomendacao { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdUser { get; set; }
        public int IdCheckin { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
