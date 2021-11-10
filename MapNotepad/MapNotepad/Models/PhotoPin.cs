using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Models
{
    public class PhotoPin: IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int IdPin { get; set; }

        public string PathImage { get; set; }

        public DateTime TimeAddition { get; set; }
    }
}
