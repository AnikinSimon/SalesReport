﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Laptop : ITProduct
    {
        public int RAM { get; private set; } // в ГБ
        public string ProcessorType { get; private set; } // "i3", "i5", "i7" и т.д.

        // переопредление
        public override decimal Price => BasePrice + RAM * 1500 +
                                      ProcessorType switch
                                      {
                                          "i3" => 5000,
                                          "i5" => 10000,
                                          "i7" => 15000,
                                          "i9" => 20000,
                                          _ => 0
                                      };

        public Laptop(Guid id, string article, string brand, string model, decimal basePrice,
                DateTime? saleDate, int ram, string processorType)
        : base(id, article, brand, model, basePrice, saleDate)
        {
            RAM = ram;
            ProcessorType = processorType;
        }

        public override string ToString()
        {
            return String.Join(" ", base.ToString(), $"RAM: {RAM}", $"ProcessorType: {ProcessorType}");
        }
    }
}
