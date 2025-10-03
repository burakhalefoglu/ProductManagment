﻿using Nest;
using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class UserGroup : IEntity
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}