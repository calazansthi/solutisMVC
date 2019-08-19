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
        public int CidadeId { get; set; }

        public Cidade(int id, string nome, int cidadeId)
        {
            this.Id = id;
            this.Nome = nome;
            this.CidadeId = cidadeId;
        }
    }
}
