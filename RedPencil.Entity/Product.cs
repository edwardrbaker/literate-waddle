﻿using System;
using System.Collections.Generic;

namespace RedPencil.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double OriginalPrice { get; set; }
        public double CurrentPrice { get; set; }
        public bool IsRedPencil { get; set; }
        public List<PriceHistory> PriceHistories { get; set; }
    }

    public class PriceHistory
    {
        public double Price { get; set; }
        public DateTimeOffset LastDateChange { get; set; }
    }
}
