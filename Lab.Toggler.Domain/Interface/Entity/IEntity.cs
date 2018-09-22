using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Toggler.Domain.Interface.Entity
{
    /// <summary>
    /// Entity for persistence
    /// </summary>
    /// <typeparam name="T">Type of the identity property</typeparam>
    public interface IEntity<T>
    {
        T Id { get; set; }
    }

    /// <summary>
    /// Entity for persistence
    /// </summary>
    public interface IEntity
    {
    }
}
