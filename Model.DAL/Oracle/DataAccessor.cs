using BSHP.LoggerManager;
using Oracle.DTO;
using OracleDataContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Model.DAL.Oracle
{
    public class DataAccessor : IDataAccessor
    {
        ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(DataAccessor));

        private string _cadenaConexion = "";

        #region Contructor

        public DataAccessor(string nameConnectionString)// : base(pConnectionString)
        {
            try
            {
                string s = ConfigurationManager.ConnectionStrings[nameConnectionString].ConnectionString;

                if (s.Length > 0)
                    _cadenaConexion = s;
            }
            catch (Exception e)
            {
                log.Error("DataAccessor. ", e);
            }
        }

        #endregion

        #region IDataAccessor

        public int ExecuteNonQuery(string sql, List<OracleParameter> parameters = null, bool IsProcedure = false)
        {
            Accessor accessor = new Accessor(_cadenaConexion);
            return accessor.ExecuteNonQuery(sql, parameters, IsProcedure);
        }

        public object ExecuteNonQueryWithResult(string sql, List<OracleParameter> parameters, bool IsProcedure = false)
        {
            Accessor accessor = new Accessor(_cadenaConexion);
            return accessor.ExecuteNonQueryWithResult(sql, parameters, IsProcedure);
        }

        public object ExecuteScalar(string sql, List<OracleParameter> parameters = null, bool IsProcedure = false)
        {
            Accessor accessor = new Accessor(_cadenaConexion);
            return accessor.ExecuteScalar(sql, parameters, IsProcedure);
        }

        public DataSet FillDataSet(string sql, List<OracleParameter> parameters = null, bool IsProcedure = false)
        {
            Accessor accessor = new Accessor(_cadenaConexion);
            return accessor.FillDataSet(sql, parameters, IsProcedure);
        }

        #endregion
    }
}
