using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProgrammingTechnology : Entity
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ProgrammingLanguage? ProgrammingLanguage { get; set; }
        public ProgrammingTechnology()
        {

        }

        public ProgrammingTechnology(int id, string name, string description, int programmingLanguageId) : this()
        {
            Id = id;
            Name = name;
            Description = description;
            ProgrammingLanguageId = programmingLanguageId;
        }
    }
}
