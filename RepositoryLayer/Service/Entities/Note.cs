﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Service.Entities
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public bool IsPin { get; set; }
        public bool IsReminder { get; set; }
        public bool IsArchieve { get; set; }
        public bool IsTrash { get; set; }
        public DateTime Reminder { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User user { get; set; }
    }
}