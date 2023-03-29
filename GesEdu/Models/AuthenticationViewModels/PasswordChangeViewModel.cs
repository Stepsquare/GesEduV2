﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GesEdu.Models.AuthenticationViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "* Campo obrigatório.")]
        [DisplayName("Password Antiga")]
        [MinLength(6, ErrorMessage = "A password tem de ter no mínimo 6 caracteres.")]
        public string? OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "* Campo obrigatório.")]
        [DisplayName("Password Nova")]
        [MinLength(6, ErrorMessage = "A password tem de ter no mínimo 6 caracteres.")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "* Campo obrigatório.")]
        [DisplayName("Confirmar Password Nova")]
        [MinLength(6, ErrorMessage = "A password tem de ter no mínimo 6 caracteres.")]
        [Compare("NewPassword", ErrorMessage = "As passwords são diferentes.")]
        public string? ConfirmNewPassword { get; set; }
    }
}
