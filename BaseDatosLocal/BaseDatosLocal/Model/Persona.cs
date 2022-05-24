using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BaseDatosLocal.Model
{
    public class Persona
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }    
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
       // public string Celular { get; set; }
    }
}
