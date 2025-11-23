namespace mindtrack.DTO.Request
{
    using System.ComponentModel.DataAnnotations;

    public class NotificacaoDto
    {
        [Required(ErrorMessage = "A mensagem é obrigatória.")]
        [StringLength(200, ErrorMessage = "A mensagem deve ter no máximo {1} caracteres.")]
        public string Mensagem { get; set; }

        [Required(ErrorMessage = "O tipo de notificação é obrigatório.")]
        [StringLength(20, ErrorMessage = "O tipo deve ter no máximo {1} caracteres.")]
        public string TipoNotificacao { get; set; }

        [Required(ErrorMessage = "A data de envio é obrigatória.")]
        public DateTime DataEnvio { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do usuário deve ser maior que zero.")]
        public int IdUser { get; set; }
    }
}
