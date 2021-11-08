﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Models
{
    public class Pin : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Autor { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public bool Favorites { get; set; }
        public DateTime TimeCreating { get; set; }
    }
}
