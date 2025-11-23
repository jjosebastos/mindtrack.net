using System.ComponentModel.DataAnnotations;

namespace mindtrack.DTO.Request
{
    public class RecomendacaoIADto
    {
        [Required(ErrorMessage = "O texto da recomendação é obrigatório.")]
        [StringLength(2000, ErrorMessage = "O texto deve ter no máximo {1} caracteres.")]
        public string Texto { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um ID de usuário válido.")]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "O ID do check-in associado é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um ID de check-in válido.")]
        public int IdCheckin { get; set; }
    }
}
