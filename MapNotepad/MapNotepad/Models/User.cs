﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Models
{
    public class User : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime TimeCreating { get; set; }
    }
}
