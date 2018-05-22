using BSHP.LoggerManager;
using System;

namespace Model.DAL.Common
{
    public static class ConvertValue
    {
        /// <summary>
        /// Convierte un objeto a int. En el caso que no tenga valor devuelve un 0
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static int ToInt32(object field)
        {
            int r = 0;

            try
            {
                if (field != null && field != DBNull.Value)
                {
                    r = Convert.ToInt32(field);
                }
            }
            catch (Exception er)
            {
                ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(ConvertValue));
                log.Error(string.Format("ToInt32({0})", field), er);
            }

            return r;
        }

        public static int? ToInt32_Nullable(object field)
        {
            int? r = null;

            try
            {
                if (field != null && field != DBNull.Value)
                {
                    r = Convert.ToInt32(field);
                }
            }
            catch (Exception er)
            {
                ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(ConvertValue));
                log.Error(string.Format("ToInt32_Nullable({0})", field), er);
            }

            return r;
        }

        public static long ToInt64(object field)
        {
            long r = 0;

            try
            {
                if (field != null && field != DBNull.Value)
                {
                    r = Convert.ToInt64(field);
                }
            }
            catch (Exception er)
            {
                ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(ConvertValue));
                log.Error(string.Format("ToInt64({0})", field), er);
            }

            return r;
        }


        /// <summary>
        /// Convierte un objeto a short. En el caso que no tenga valor devuelve un 0
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static short ToShort(object field)
        {
            short r = 0;

            try
            {
                if (field != null && field != DBNull.Value)
                {
                    r = Convert.ToInt16(field);
                }
            }
            catch (Exception er)
            {
                ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(ConvertValue));
                log.Error(string.Format("ConvertToShort({0})", field), er);
            }

            return r;
        }

        /// <summary>
        /// Convierte un objeto a int. En el caso que no tenga valor devuelve un 0
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static int? ToNullableInt32(object field)
        {
            int? r = null;

            try
            {
                if (field != null && field != DBNull.Value)
                {
                    r = Convert.ToInt32(field);
                }
            }
            catch (Exception er)
            {
                ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(ConvertValue));
                log.Error(string.Format("ConvertToNullableInt32({0})", field), er);
            }

            return r;
        }

        /// <summary>
        /// Convierte un objeto a string. En el caso que no tenga valor devuelve una cadena vacía
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string ToString(object field)
        {
            string r = string.Empty;

            try
            {
                if (field != null && field != DBNull.Value)
                {
                    r = Convert.ToString(field);

                    // Quitamos caracter inválido de cierre
                    r = r.Replace("\0", "");
                }
            }
            catch (Exception er)
            {
                ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(ConvertValue));
                log.Error(string.Format("ConvertToString({0})", field), er);
            }

            return r;
        }

        /// <summary>
        /// Convierte un objeto a DateTime. En el caso que no tenga valor devuelve un DateTime.MinValue
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object field)
        {
            DateTime r = DateTime.MinValue;

            try
            {
                if (field != null && field != DBNull.Value)
                {
                    r = Convert.ToDateTime(field);
                }
            }
            catch (Exception er)
            {
                ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(ConvertValue));
                log.Error(string.Format("ConvertToDateTime({0})", field), er);
            }

            return r;
        }

        /// <summary>
        /// Convierte un objeto a array de bytes. En el caso que no tenga valor devuelve nulo
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(object field)
        {
            byte[] b = null;

            try
            {
                if (field != null && field != DBNull.Value)
                {
                    b = field as byte[];
                }
            }
            catch (Exception er)
            {
                ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(ConvertValue));
                log.Error("ToByteArray()", er);
            }

            return b;
        }

        /// <summary>
        /// Convierte un objeto a bool. En el caso que no tenga valor devuelve false
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool ToBoolean(object field)
        {
            bool r = false;

            try
            {
                if (field != null && field != DBNull.Value)
                {
                    r = Convert.ToInt32(field) == 1;
                }
            }
            catch (Exception er)
            {
                ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(ConvertValue));
                log.Error(string.Format("ConvertToBoolean({0})", field), er);
            }

            return r;
        }
    }
}
