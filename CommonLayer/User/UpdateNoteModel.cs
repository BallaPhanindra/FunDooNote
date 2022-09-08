using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{
    public class UpdateNoteModel
    {

        [RegularExpression("^[A-Z]{1}[a-z]{2,}$", ErrorMessage = " Start with capital letter and has minimum three character")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }
        public bool IsPin { get; set; }
        public bool IsReminder { get; set; }
        public bool IsArchieve { get; set; }
        public bool IsTrash { get; set; }
        public DateTime Reminder { get; set; }
    }
}