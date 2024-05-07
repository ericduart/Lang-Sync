using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSyncServer.utils
{
    public class Constants
    {
        public class GrammarItem
        {

            public string english { get; set; }
            public string spanish { get; set; }

            public GrammarItem() { }

        }

        public class PlayerData
        {
            public string name { get; set; }

            public string grammar { get; set; }

            public string userInput { get; set; }
            public bool isCorrect { get; set; }

            public PlayerData() { }

        }
    }
}
