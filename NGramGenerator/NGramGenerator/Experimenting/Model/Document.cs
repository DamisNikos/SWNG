using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGramGenerator.Experimenting.Model
{
    //A suggestion about our database model
    class Document
    {
        public int DocID { get; set; }
        public string DocName { get; set; }
        public byte[] DocContent { get; set; }



    }
}
