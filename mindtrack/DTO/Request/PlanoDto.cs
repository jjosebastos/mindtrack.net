using System.ComponentModel.DataAnnotations;

namespace mindtrack.DTO.Request
{
    public class PlanoDto : IValidatableObject
    {
        [Required(ErrorMessage = "O título do plano é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo {1} caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo {1} caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        [StringLength(30, ErrorMessage = "O status deve ter no máximo {1} caracteres.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um ID de usuário válido.")]
        public int IdUser { get; set; }

        /// <summary>
        /// Validação customizada para garantir coerência nas datas.
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataFim < DataInicio)
            {
                yield return new ValidationResult(
                    "A Data de Fim não pode ser anterior à Data de Início.",
                    new[] { nameof(DataFim) }
                );
            }
        }
    }
}
