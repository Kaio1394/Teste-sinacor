using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p_sinacor.Pages
{
    public class Pages
    {
        public CorreiosPage correioPage;
        public LinkCorreiosPage linkCorreiosPage;
        public Pages() 
        { 
            correioPage = new CorreiosPage();
            linkCorreiosPage = new LinkCorreiosPage();
        }
    }
}
