using mindtrack.DTO.Shared;

namespace mindtrack.DTO.Response
{
    public class UserResponse
    {

        public int IdUser { get; set; }

        public String Nome { get; set; }

        public String Email { get; set; }

        public String Setor { get; set; }

        public String Cargo { get; set; }

        public DateTime DataAdmissao { get; set; }

        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
