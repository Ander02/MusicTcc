using System;

namespace Core.Interfaces
{
    public interface IEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
