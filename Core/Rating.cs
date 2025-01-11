using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Rating : IEntity
    {
        public Guid Id { get; set; }
        public Guid MusicId { get; set; }
        public Music Music { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
