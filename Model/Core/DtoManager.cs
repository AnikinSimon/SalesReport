using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class DtoManager
    {
        public static ReportDto ToDto(Report report)
        {
            return new ReportDto(report);
        }

        public static ITProductDto ToDto(ITProduct product)
        {
            return new ITProductDto(product);
        }

        public static Report FromDto(ReportDto dto)
        {
            return new Report(
                dto.Name,
                dto.StartDate,
                dto.EndDate,
                dto.Devices.Select(DtoManager.FromDto).ToList());
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

                _ => null
            };
        }
    }
}
