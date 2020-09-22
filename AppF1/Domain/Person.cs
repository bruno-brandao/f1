using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppF1.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string OP_CPF { get; set; }
        public string OP_MAT { get; set; }
        [Required]
        public string OP_NM { get; set; }
        public DateTime DN { get; set; }
        public string PAI { get; set; }
        public string MAE { get; set; }
        public string PROFISSAO { get; set; }
        public int? ParentId { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual Person Parent { get; set; }
    }
}
