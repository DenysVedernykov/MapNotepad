using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Models
{
    public class Events : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Autor { get; set; }
        public string Title { get; set; }
        public bool Happened { get; set; }
        public DateTime TimeEvent { get; set; }
        public DateTime TimeCreating { get; set; }
    }
}
