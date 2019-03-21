using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FichaAcademia.Dominio.Models
{
    public class ListaExercicio
    {
        public int ListaExercicioId { get; set; }

        public int ExercicioId { get; set; }
        public Exercicio Exercicio { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, 10, ErrorMessage = "Frequencia inválida")]
        public int Frequencia { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, 100, ErrorMessage = "Número inválida")]
        public int Repeticoes { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, 200, ErrorMessage = "Número inválida")]
        public int Carga { get; set; }

        public int FichaId { get; set; }
        public Ficha Ficha { get; set; }
    }
}
