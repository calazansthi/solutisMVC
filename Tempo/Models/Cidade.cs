using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tempo.Models
{
    [DataContract]
    public class BaseModel
    {
        [DataMember]
        public int? Id { get; set; }
    }

    public class Cidade : BaseModel
    {
        public Cidade()
        {

        }
        
        [Required]
        public string Nome { get; set; }        
        public string Condicao { get; set; }
        public decimal? Temperatura { get; set; }

        public Cidade(int id, string nome, string condicao, decimal temperatura)
        {
            this.Id = id;
            this.Nome = nome;
            this.Condicao = condicao;
            this.Temperatura = temperatura;
        }
    }
}
