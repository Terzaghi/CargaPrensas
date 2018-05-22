using BSHP.LoggerManager;
using Model.DAL.Common;
using Model.DAL.DTO;
using Model.DAL.Interfaces;
using Model.DAL.Oracle;
using Oracle.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace Model.DAL
{
    public class PrensasDatosDAL : IPrensasDatos
    {
        ILogger log = BSHP.LoggerManager.LogFactory.GetLogger(typeof(PrensasDatosDAL));

        private string _connectionString;

        public PrensasDatosDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<PrensaDato> ListarNuevosRegistros(DateTime Fecha)
        {
            List<PrensaDato> datos = null;

            try
            {
                var sql = string.Format(@"SELECT
                                                FECHA_HORA AS {0},
                                                PRENSA_NAME AS {1},
                                                PRENSA_TAG_ACTIVA AS {2},
                                                PRENSA_TAG_CV AS {3},
                                                PRENSA_TAG_TEMP AS {4},
                                                PRENSA_TAG_CICLO AS {5},
                                                PRENSA_TAG_PROD AS {6},
                                                PRENSA_CAVIDAD AS {7}
                                            FROM PRENSAS_DATOS
                                            WHERE FECHA_HORA > :{8}
                                            ORDER BY FECHA_HORA ASC",
                                        Arguments.Fecha, Arguments.Prensa, Arguments.TagActiva,
                                        Arguments.TagCV, Arguments.TagTemp, Arguments.TagCiclo,
                                        Arguments.TagProd, Arguments.Cavidad, Arguments.FechaParameter);

                var accessor = new DataAccessor(_connectionString);

                List<OracleParameter> parameters = new List<OracleParameter>()
                {
                    new OracleParameter(Arguments.FechaParameter, Fecha)
                };

                var ds = accessor.FillDataSet(sql, parameters);

                datos = GetCollection(ds);
            }
            catch (Exception ex)
            {
                log.Error("ListarNuevosRegistros. ", ex);
            }

            return datos;
        }

        #region Private Interface

        private List<PrensaDato> GetCollection(DataSet ds)
        {
            List<PrensaDato> tags = null;

            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    tags = new List<PrensaDato>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tags.Add(new PrensaDato
                        {
                            Fecha = ConvertValue.ToDateTime(ds.Tables[0].Rows[i][Arguments.Fecha]),
                            PrensaId = ConvertValue.ToInt32(ds.Tables[0].Rows[i][Arguments.Prensa]),
                            TagActivaValue = ConvertValue.ToString(ds.Tables[0].Rows[i][Arguments.TagActiva]),
                            TagCVValue = ConvertValue.ToString(ds.Tables[0].Rows[i][Arguments.TagCV]),
                            TagTempValue = ConvertValue.ToString(ds.Tables[0].Rows[i][Arguments.TagTemp]),
                            TagCicloValue = ConvertValue.ToString(ds.Tables[0].Rows[i][Arguments.TagCiclo]),
                            TagProdValue = ConvertValue.ToString(ds.Tables[0].Rows[i][Arguments.TagProd]),
                            Cavidad = ConvertValue.ToString(ds.Tables[0].Rows[i][Arguments.Cavidad])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("GetCollection", ex);
            }

            return tags;
        }

        #endregion
    }
}
