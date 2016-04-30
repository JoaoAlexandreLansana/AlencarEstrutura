using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DAL
{
    public class EstadoDAL
    {
        public List<Estado> ObterListaDeEstados()
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
            {
                string query = @"SELECT PKNI013_IDESTADO, 
                                        ATSF013_SIGLA, 
                                        ATSF013_NOME 
                                        FROM ALC013T_ESTADO";

                conn.Open();

                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        List<Estado> lstEstado = new List<Estado>();
                        while (reader.Read())
                        {
                            Estado objEstado = new Estado();

                            objEstado.IdEstado = Convert.ToInt32(reader[0]);
                            objEstado.Sigla = reader[1].ToString();
                            objEstado.NomeEstado = reader[2].ToString();
                            lstEstado.Add(objEstado);
                        }
                        return lstEstado;
                    }
                }
            }
        }
    }
}