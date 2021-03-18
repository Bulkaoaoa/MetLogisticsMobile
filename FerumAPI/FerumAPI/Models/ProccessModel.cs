using FerumAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerumAPI.Models
{
    public class ProccessModel
    {
        public ProccessModel(Proccess proccess)
        {
            Id = proccess.Id;
            Name = proccess.Name;
            NormativeTime = proccess.NormativeTime;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int NormativeTime { get; set; }
    }
}