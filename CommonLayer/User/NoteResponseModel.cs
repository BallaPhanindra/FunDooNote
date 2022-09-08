using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.User
{
    public class NoteResponseModel
    {

        public int NoteId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
