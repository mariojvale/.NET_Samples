using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ManipulandoJSON
{
    class Topico
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public string Usuario { get; set; }
        
        public Topico(int id, string titulo, string sinopse, string usuario)
        {
            Id = id;
            Titulo = titulo;
            Sinopse = sinopse;
            Usuario = usuario;  
        }

        public override string ToString()
        {
            return $"{Titulo} - {Sinopse} - {Usuario}";
        }
    }
}
