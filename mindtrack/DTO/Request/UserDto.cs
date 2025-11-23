using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace mindtrack.DTO.Request
{
    public class UserDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O campo Genero é obrigatório.")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        [StringLength(100, ErrorMessage = "O e-mail não pode exceder 100 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo Setor é obrigatório.")]
        [StringLength(50, ErrorMessage = "O setor não pode exceder 50 caracteres.")]
        public string Setor { get; set; }

        [Required(ErrorMessage = "O campo Cargo é obrigatório.")]
        [StringLength(50, ErrorMessage = "O cargo não pode exceder 50 caracteres.")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "A data de admissão é obrigatória.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataAdmissao { get; set; }



    }
}
