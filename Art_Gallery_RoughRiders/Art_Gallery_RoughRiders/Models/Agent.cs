using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art_Gallery_RoughRiders.Models
{
    public class Agent
    {
        public int IdAgent { get; set; }
        public string AgentFirstName { get; set; }
        public string AgentLastName { get; set; }
        public string AgentLocation { get; set; }
        public string AgentAddress { get; set; }
        public int AgentPhoneNumber { get; set; }
        public bool Active { get; set; }
    }
}