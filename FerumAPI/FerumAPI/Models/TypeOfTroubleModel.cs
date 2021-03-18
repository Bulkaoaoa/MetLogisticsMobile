using FerumAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class TypeOfTroubleModel
    {
        public TypeOfTroubleModel(TypeOfTrouble typeOfTrouble)
        {
            Id = typeOfTrouble.Id;
            Name = typeOfTrouble.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}