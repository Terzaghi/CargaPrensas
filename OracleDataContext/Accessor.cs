using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using BSHP.LoggerManager;

namespace OracleDataContext
{
    public class Accessor : Oracle.DTO.IDataAccessor
    {
        ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(Accessor));

        private string _cadenaConexion = "";

        #region Contructor

        public Accessor(string pConnectionString)
        {
            this._cadenaConexion = pConnectionString;
        }

        #endregion

        #region Public Interface

        /// <summary>
        /// Ejecuta una consulta SQL o un Procedimiento devolviendo el numero de filas afectadas
        /// NO EJECUTA PROCEDIMIENTOS ALMACENADOS
        /// </summary>
        public int ExecuteNonQuery(string sql, List<Oracle.DTO.OracleParameter> parameters = null, bool IsProcedure = false)
        {
            int resultado = 0;

            DateTime fchTiempoRespuesta = DateTime.UtcNow;

            try
            {
                using (var context = new DataContext(_cadenaConexion))
                {
                    parameters = GetParametersForCurrentContext(parameters, context.Context);

                    using (var cmd = new OracleCommand(sql, context.Context))
                    {
                        cmd.CommandType = IsProcedure ? CommandType.StoredProcedure : CommandType.Text;
                        cmd.BindByName = true;

                        if (parameters != null && parameters.Count > 0)
                            cmd.Parameters.AddRange(parameters.ToOracle());

                        resultado = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("ExecuteNonQuery()", ex);

                //SaveParametersOnLog(sql, parameters);
            }

            //if (log.isDebugEnabled)
            //{
            //    TimeSpan tsTiempoRespuesta = DateTime.UtcNow - fchTiempoRespuesta;
            //    log.Debug("ExecuteNonQuery(). Tiempo respuesta SQL: {0}", tsTiempoRespuesta.TotalSeconds);
            //}

            return resultado;
        }

        /// <summary>
        /// Ejecuta una consulta SQL o un Procedimiento devolviendo un valor de un parámetro
        /// NO EJECUTA PROCEDIMIENTOS ALMACENADOS
        /// </summary>
        public object ExecuteNonQueryWithResult(string sql, List<Oracle.DTO.OracleParameter> parameters, bool IsProcedure = false)
        {
            object resultado = null;

            DateTime fchTiempoRespuesta = DateTime.UtcNow;

            try
            {
                // Para hacer la petición, devolviendo al menos un resultado (widthResult), hace falta al menos un parámetro de salida
                if (parameters == null || parameters.Count == 0)
                {
                    log.Warning("ExecuteNonQueryWithResult(). Necesita al menos un parámetro de salida");
                    return resultado;
                }

                using (var context = new DataContext(_cadenaConexion))
                {
                    parameters = GetParametersForCurrentContext(parameters, context.Context);

                    using (var cmd = new OracleCommand(sql, context.Context))
                    {
                        cmd.CommandType = IsProcedure ? CommandType.StoredProcedure : CommandType.Text;
                        cmd.BindByName = true;

                        cmd.Parameters.AddRange(parameters.ToOracle());

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            var returningParameter = parameters.FirstOrDefault(a => a.Direction == ParameterDirection.Output);

                            if (returningParameter != null)
                            {
                                switch (returningParameter.OracleDbType)
                                {
                                    case Oracle.DTO.OracleDbType.Decimal:
                                    case Oracle.DTO.OracleDbType.Double:
                                    case Oracle.DTO.OracleDbType.Int16:
                                    case Oracle.DTO.OracleDbType.Int32:
                                        if (cmd.Parameters[returningParameter.ParameterName].Value != DBNull.Value)
                                        {
                                            int id = 0;

                                            if (int.TryParse(cmd.Parameters[returningParameter.ParameterName].Value.ToString(), out id))
                                                resultado = id;
                                        }
                                        break;
                                    case Oracle.DTO.OracleDbType.Long:
                                    case Oracle.DTO.OracleDbType.Int64:
                                        if (cmd.Parameters[returningParameter.ParameterName].Value != DBNull.Value)
                                        {
                                            long id = 0;

                                            if (long.TryParse(cmd.Parameters[returningParameter.ParameterName].Value.ToString(), out id))
                                                resultado = id;
                                        }
                                        break;
                                    default:
                                        resultado = cmd.Parameters[returningParameter.ParameterName].Value != DBNull.Value ? cmd.Parameters[returningParameter.ParameterName].Value.ToString() : string.Empty;
                                        break;
                                }
                            }
                            //else log.Warning("ExecuteNonQueryWithResult. No se ha obtenido el parámetro de retorno");
                        }
                        //else log.Warning("ExecuteNonQueryWithResult. No se ha realizado la inserción");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("ExecuteNonQueryWithResult()", ex);

                //SaveParametersOnLog(sql, parameters);
            }

            //if (log.isDebugEnabled)
            //{
            //    TimeSpan tsTiempoRespuesta = DateTime.UtcNow - fchTiempoRespuesta;
            //    log.Debug("ExecuteNonQueryWithResult(). Tiempo respuesta SQL: {0}", tsTiempoRespuesta.TotalSeconds);
            //}

            return resultado;
        }

        /// <summary>
        /// Ejecuta una consulta SQL o un Procedimiento devolviendo el objeto obtenido
        /// </summary>
        public object ExecuteScalar(string sql, List<Oracle.DTO.OracleParameter> parameters = null, bool IsProcedure = false)
        {
            object resultado = null;

            DateTime fchTiempoRespuesta = DateTime.UtcNow;

            try
            {
                using (var context = new DataContext(_cadenaConexion))
                {
                    using (var cmd = new OracleCommand(sql, context.Context))
                    {
                        cmd.CommandType = IsProcedure ? CommandType.StoredProcedure : CommandType.Text;
                        cmd.BindByName = true;

                        if (parameters != null && parameters.Count > 0) cmd.Parameters.AddRange(parameters.ToOracle());

                        resultado = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("ExecuteScalar()", ex);

                //SaveParametersOnLog(sql, parameters);
            }

            //if (log.isDebugEnabled)
            //{
            //    TimeSpan tsTiempoRespuesta = DateTime.UtcNow - fchTiempoRespuesta;
            //    log.Debug("ExecuteScalar(). Tiempo respuesta SQL: {0}", tsTiempoRespuesta.TotalSeconds);
            //}

            return resultado;
        }
        
        /// <summary>
        /// Ejecuta una consulta o un procedimiento y devuelve un DataSet con los valores obtenidos
        /// </summary>
        public DataSet FillDataSet(string sql, List<Oracle.DTO.OracleParameter> parameters = null, bool IsProcedure = false)
        {
            DataSet ds = new DataSet();

            DateTime fchTiempoRespuesta = DateTime.UtcNow;

            try
            {
                using (var context = new DataContext(_cadenaConexion))
                {
                    using (var cmd = new OracleCommand(sql, context.Context))
                    {
                        cmd.CommandType = IsProcedure ? CommandType.StoredProcedure : CommandType.Text;
                        cmd.BindByName = true;

                        if (parameters != null && parameters.Count > 0)
                        {
                            cmd.Parameters.AddRange(parameters.ToOracle());
                        }

                        using (var adapter = new OracleDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("FillDataSet()", ex);

                //SaveParametersOnLog(sql, parameters);
            }

            //if (log.isDebugEnabled)
            //{
            //    TimeSpan tsTiempoRespuesta = DateTime.UtcNow - fchTiempoRespuesta;
            //    log.Debug("FillDataSet(). Tiempo respuesta SQL: {0}", tsTiempoRespuesta.TotalSeconds);
            //}

            return ds;
        }

        #endregion

        #region Métodos privados
        private void SaveParametersOnLog(string procedimiento, List<Oracle.DTO.OracleParameter> Parametros)
        {
            try
            {
                log.Warning("Parámetros empleados en la petición. Consulta: {0}", procedimiento);
                string strParametros = "";
                if ((Parametros != null) && (Parametros.Count > 0))
                {
                    foreach (var p in Parametros)
                    {
                        if (p.Direction == ParameterDirection.Output)
                        {
                            strParametros += string.Format("{0} (Output parameter)", p.ParameterName);
                        }
                        else
                        {
                            strParametros += string.Format("{0}: {1} ", p.ParameterName, p.Value);
                        }
                    }
                    log.Warning(string.Format("Parámetros en la petición: {0}", strParametros));
                }
            }
            catch (Exception er)
            {
                log.Error("SaveParametersOnLog()", er);
            }
        }
        #endregion

        #region Private Interface

        /// <summary>
        /// Establece los parametros para la conexión actual. Es necesario para los valores de tipo BLOB
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private List<Oracle.DTO.OracleParameter> GetParametersForCurrentContext(List<Oracle.DTO.OracleParameter> parameters, OracleConnection context)
        {
            var finalParameters = new List<Oracle.DTO.OracleParameter>();

            try
            {
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        if (parameters[i].OracleDbType.Equals(Oracle.DTO.OracleDbType.Blob) &&
                            parameters[i].Value != null &&
                            parameters[i].Value.GetType().Equals(typeof(byte[])))
                        {
                            var value = (byte[])parameters[i].Value;

                            var blob = new OracleBlob(context);
                            blob.Erase();
                            blob.Write(value, 0, value.Length);

                            parameters[i] = new Oracle.DTO.OracleParameter(parameters[i].ParameterName, Oracle.DTO.OracleDbType.Blob, blob, parameters[i].Direction);
                        }

                        finalParameters.Add(parameters[i]);
                    }
                }
            }
            catch (Exception er)
            {
                log.Error("GetParametersForCurrentContext()", er);
            }


            return finalParameters;
        }

        #endregion
    }
}
