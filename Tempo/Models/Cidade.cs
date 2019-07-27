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
        public int Id { get; protected set; }
    }

    public class Cidade : BaseModel
    {
        public Cidade()
        {

        }
        
        [Required]
        public string Nome { get; private set; }        
        public string Condicao { get; private set; }
        public decimal Temperatura { get; private set; }

        public Cidade(string nome, string condicao, decimal temperatura)
        {        
            this.Nome = nome;
            this.Condicao = condicao;
            this.Temperatura = temperatura;
        }
    }
}
