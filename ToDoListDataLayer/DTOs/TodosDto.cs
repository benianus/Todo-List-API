using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;

namespace ToDoListDataLayer.Dtos;

public class TodosDto
{
    public TodosDto(List<TaskDto> tasks, int page, int limite)
    {
        Tasks = tasks;
        Page = page;
        Limite = limite;
        Total = tasks.Count;
    }

    public List<TaskDto> Tasks { get; set; }
    [Required]
    public int Page { get; set; }
    [Required]
    public int Limite { get; set; }
    public int Total { get; set; }

   
}
