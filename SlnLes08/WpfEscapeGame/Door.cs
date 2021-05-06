using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEscapeGame
{
    class Door
    {
        public bool IsLocked { get; set; } = true;
        public string Name { get; } // read-only: kan maar één keer ingesteld worden
        public string Description { get; }
        public List<Item> Items { get; set; } = new List<Item>();
        public Door(string name, string desc)
        {
            Name = name;
            Description = desc;
        }
    }
}
