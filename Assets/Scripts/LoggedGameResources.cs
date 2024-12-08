using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class LoggedGameResources
    {
        public GameResources resources;
        public string message;

        public LoggedGameResources(GameResources resources, string message)
        {
            this.resources = resources;
            this.message = message;
        }
    }
}
