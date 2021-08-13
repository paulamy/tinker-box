using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.Models
{
    public class OwlBotResult
    {
        //definition only from the returned JSON object
        public List<OwlBotDefinition> Definitions { get; set; }
        
        public OwlBotResult()
        {
            Definitions = new List<OwlBotDefinition>();
        }
    }
}
