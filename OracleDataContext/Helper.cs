using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace OracleDataContext
{
    public static class Helper
    {
        public static System.Array ToOracle(this List<Oracle.DTO.OracleParameter> parameters)
        {
            List<Oracle.ManagedDataAccess.Client.OracleParameter> result = null;

            if (parameters != null)
            {
                result = new List<Oracle.ManagedDataAccess.Client.OracleParameter>();

                foreach (var item in parameters)
                {
                    Oracle.ManagedDataAccess.Client.OracleParameter param = item.ToOracle();

                    if (param != null)
                        result.Add(param);
                }
            }

            return result.ToArray();
        }

        private static Oracle.ManagedDataAccess.Client.OracleParameter ToOracle(this Oracle.DTO.OracleParameter parameter)
        {
            Oracle.ManagedDataAccess.Client.OracleParameter result = null;

            if (parameter != null)
            {
                object type = null;

                result = new OracleParameter()
                {
                    ParameterName = parameter.ParameterName,
                    Value = parameter.Value,
                    Direction = parameter.Direction,
                    OracleDbType = parameter.OracleDbType.ToOracle()
                };
            }

            return result;
        }

        private static OracleDbType ToOracle(this Oracle.DTO.OracleDbType type)
        {
            OracleDbType result = OracleDbType.Varchar2;

            switch (type)
            {
                case Oracle.DTO.OracleDbType.BFile:
                    result = OracleDbType.BFile;
                    break;
                case Oracle.DTO.OracleDbType.Blob:
                    result = OracleDbType.Blob;
                    break;
                case Oracle.DTO.OracleDbType.Byte:
                    result = OracleDbType.Byte;
                    break;
                case Oracle.DTO.OracleDbType.Char:
                    result = OracleDbType.Char;
                    break;
                case Oracle.DTO.OracleDbType.Clob:
                    result = OracleDbType.Clob;
                    break;
                case Oracle.DTO.OracleDbType.Date:
                    result = OracleDbType.Date;
                    break;
                case Oracle.DTO.OracleDbType.Decimal:
                    result = OracleDbType.Decimal;
                    break;
                case Oracle.DTO.OracleDbType.Double:
                    result = OracleDbType.Double;
                    break;
                case Oracle.DTO.OracleDbType.Long:
                    result = OracleDbType.Long;
                    break;
                case Oracle.DTO.OracleDbType.LongRaw:
                    result = OracleDbType.LongRaw;
                    break;
                case Oracle.DTO.OracleDbType.Int16:
                    result = OracleDbType.Int16;
                    break;
                case Oracle.DTO.OracleDbType.Int32:
                    result = OracleDbType.Int32;
                    break;
                case Oracle.DTO.OracleDbType.Int64:
                    result = OracleDbType.Int64;
                    break;
                case Oracle.DTO.OracleDbType.IntervalDS:
                    result = OracleDbType.IntervalDS;
                    break;
                case Oracle.DTO.OracleDbType.IntervalYM:
                    result = OracleDbType.IntervalYM;
                    break;
                case Oracle.DTO.OracleDbType.NClob:
                    result = OracleDbType.NClob;
                    break;
                case Oracle.DTO.OracleDbType.NChar:
                    result = OracleDbType.NChar;
                    break;
                case Oracle.DTO.OracleDbType.NVarchar2:
                    result = OracleDbType.NVarchar2;
                    break;
                case Oracle.DTO.OracleDbType.Raw:
                    result = OracleDbType.Raw;
                    break;
                case Oracle.DTO.OracleDbType.RefCursor:
                    result = OracleDbType.RefCursor;
                    break;
                case Oracle.DTO.OracleDbType.Single:
                    result = OracleDbType.Single;
                    break;
                case Oracle.DTO.OracleDbType.TimeStamp:
                    result = OracleDbType.TimeStamp;
                    break;
                case Oracle.DTO.OracleDbType.TimeStampLTZ:
                    result = OracleDbType.TimeStampLTZ;
                    break;
                case Oracle.DTO.OracleDbType.TimeStampTZ:
                    result = OracleDbType.TimeStampTZ;
                    break;
                case Oracle.DTO.OracleDbType.Varchar2:
                    result = OracleDbType.Varchar2;
                    break;
                case Oracle.DTO.OracleDbType.XmlType:
                    result = OracleDbType.XmlType;
                    break;
                //case Oracle.DTO.OracleDbType.Array:
                //    result = OracleDbType.Array;
                //    break;
                //case Oracle.DTO.OracleDbType.Object:
                //    result = OracleDbType.Object;
                //    break;
                //case Oracle.DTO.OracleDbType.Ref:
                //    result = OracleDbType.Ref;
                //    break;
                case Oracle.DTO.OracleDbType.BinaryDouble:
                    result = OracleDbType.BinaryDouble;
                    break;
                case Oracle.DTO.OracleDbType.BinaryFloat:
                    result = OracleDbType.BinaryFloat;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
