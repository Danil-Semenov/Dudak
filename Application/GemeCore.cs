using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application
{
    public static class GemeCore
    {
        public static IEnumerable<int> Guests { get;
            set;
        }

        public static IEnumerable<Room> Rooms { get; set; }
    }
}
