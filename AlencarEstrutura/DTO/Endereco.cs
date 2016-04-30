using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class Endereco
    {
        public int IdEndereco { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Complemento { get; set; }
        public int IdMunicipio { get; set; }
        public string NomeMunicipio { get; set; }
        public int IdEstado { get; set; }
        public string NomeEstado { get; set; }
        public string Sigla { get; set; }
        public int IdPessoa { get; set; }
    }
}