using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.Models
{
    public class MetMuseumResult
    {
        public List<int> objectIDs { get; set; }

        public MetMuseumResult()
        {
            objectIDs = new List<int>();
        }
    }
}
