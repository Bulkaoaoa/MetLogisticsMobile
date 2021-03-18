using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class TroubleModel
    {
        public TroubleModel(Trouble trouble, bool viewTypeTrouble)
        {
            Id = trouble.Id;
            Description = trouble.Description;
            TypeOfTrouble = new TypeOfTroubleModel(trouble.TypeOfTrouble);
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProccesId { get; set; }

        public virtual TypeOfTroubleModel TypeOfTrouble { get; set; }
    }
}