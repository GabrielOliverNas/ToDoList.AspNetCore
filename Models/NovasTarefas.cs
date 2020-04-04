using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TodoAspNET.Models;

namespace ToDoListGit.Models
{
    public class NovasTarefas
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }

        [ForeignKey("Lista")]
        public int IdLista { get; set; }

        public static implicit operator NovasTarefas(List<Tarefa> v)
        {
            throw new NotImplementedException();
        }
    }
}