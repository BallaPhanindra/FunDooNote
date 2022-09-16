using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Service.Entities
{
    public class Collaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int collaboratorId { get; set; }
        public string collabEmail { get; set; }
        public int userId { get; set; }
        public virtual User user { get; set; }
        public int? NoteId { get; set; }
        public virtual Note Note { get; set; }
    }
}
