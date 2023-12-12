using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAK
{
    class Stone
    {
        public int Player { get; set; }
        public bool Caps { get; set; }
        public bool Stand { get; set; }

        public Stone(int player, bool caps, bool stand)
        {
            Player = player;
            Caps = caps;
            Stand = stand;
        }

        public static implicit operator List<object>(Stone v)
        {
            throw new NotImplementedException();
        }
    }
}
