using Model.BL.DTO;
using System.Collections.Generic;

namespace Model.BL.Utils
{
    internal static class Converter
    {
        internal static PrensaDato ConvertToBL(DAL.DTO.PrensaDato item)
        {
            PrensaDato dato = null;

            if (item != null)
            {
                dato = new PrensaDato
                {
                    Fecha = item.Fecha,
                    PrensaId = item.PrensaId,
                    TagActivaValue = item.TagActivaValue,
                    TagCVValue = item.TagCVValue,
                    TagCicloValue = item.TagCicloValue,
                    TagProdValue = item.TagProdValue,
                    TagTempValue = item.TagTempValue,
                    Cavidad = item.Cavidad
                };
            }
            return dato;
        }

        internal static List<PrensaDato> ConvertToBL(IList<DAL.DTO.PrensaDato> datos)
        {
            List<PrensaDato> result = null;

            if (datos != null)
            {
                result = new List<PrensaDato>();

                foreach (var item in datos)
                {
                    var device = ConvertToBL(item);

                    if (device != null)
                        result.Add(device);
                }
            }
            return result;
        }
    }
}
