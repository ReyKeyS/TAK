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

        public Stone(int player, bool stand, bool caps)
        {
            Player = player;
            Stand = stand;
            Caps = caps;
        }

        public static implicit operator List<object>(Stone v)
        {
            throw new NotImplementedException();
        }
    }
}
