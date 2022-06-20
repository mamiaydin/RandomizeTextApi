using System;

namespace MyApi.Data.Entities.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
    }
}