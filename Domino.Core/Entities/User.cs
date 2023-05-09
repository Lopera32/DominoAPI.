using System.Collections;
using System.Collections.Generic;

namespace Domino.Core.Entities
{
    public class User : Entity
    {
        
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<DominoFullGame> DominoFullGame { get; set; }
      
    }
}
