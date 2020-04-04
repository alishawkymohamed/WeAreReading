﻿using System;

namespace Models.DbModels
{
    public class Government : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
