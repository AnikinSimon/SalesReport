using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public static class ITProductExtensions
    {
        public static ITProductDto ToDto(this ITProduct product)
        {
            var dto = new ITProductDto
            {
                Article = product.Article,
                Brand = product.Brand,
                Model = product.Model,
                BasePrice = product.BasePrice,
                SaleDate = product.SaleDate,
                Type = product.Type,
                ID = product.ID,
            };

            switch (product)
            {
                case Laptop laptop:
                    dto.RAM = laptop.RAM;
                    dto.ProcessorType = laptop.ProcessorType;
                    break;
                case Smartphone phone:
                    dto.ScreenSize = phone.ScreenSize;
                    dto.Has5G = phone.Has5G;
                    break;
                case Tablet tablet:
                    dto.HasPenSupport = tablet.HasPenSupport;
                    dto.StorageCapacity = tablet.StorageCapacity;
                    break;
            }

            return dto;
        }

        public static ITProduct FromDto(ITProductDto dto)
        {
            return dto.Type switch
            {
                nameof(Laptop) => new Laptop(dto.ID,
                    dto.Article, dto.Brand, dto.Model, dto.BasePrice,
                    dto.SaleDate, dto.RAM ?? 0, dto.ProcessorType ?? "i3"),

                nameof(Smartphone) => new Smartphone(dto.ID,
                    dto.Article, dto.Brand, dto.Model, dto.BasePrice,
                    dto.SaleDate, dto.ScreenSize ?? 6.0, dto.Has5G ?? false),

                nameof(Tablet) => new Tablet(dto.ID,
                    dto.Article, dto.Brand, dto.Model, dto.BasePrice,
                    dto.SaleDate, dto.HasPenSupport ?? false, dto.StorageCapacity ?? 64),

                _ => throw new InvalidOperationException($"Unknown product type: {dto.Type}")
            };
        }
    }
}
