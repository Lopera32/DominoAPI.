using System;
using System.Collections.Generic;
using System.Text;

namespace Domino.Core.Entities
{
    public class DominoFullGame:Entity
    {
        public string DominoGame { get; set; }

        public bool isValid { get; set; }

        public int UserId { get; set; }

        public User user { get; set; }
    }
}
