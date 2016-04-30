using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DAL
{
    public class MunicipioDAL
    {
        public List<Municipio> ObterListaDeMunicipio()
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
            {
                string query = @"SELECT 
                                      PKNI012_IDMUNICIPIO,
                                      ATSF012_NOME,
                                      FKNI012_IDESTADO
                                    FROM ALC012T_MUNICIPIO ";

                conn.Open();

                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        List<Municipio> lstMunicipio = new List<Municipio>();
                        while (reader.Read())
                        {
                            Municipio objMunicipio = new Municipio();

                            objMunicipio.IdMunicipio = Convert.ToInt32(reader[0]);
                            objMunicipio.Nome = reader[1].ToString();
                            objMunicipio.IdEstado = Convert.ToInt32(reader[2]);
                            lstMunicipio.Add(objMunicipio);
                        }
                        return lstMunicipio;
                    }
                }
            }
        }

        public List<Municipio> ObterListaDeMunicipioPorID(int IdEstado)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
            {
                string query = @"SELECT 
                                      PKNI012_IDMUNICIPIO,
                                      ATSF012_NOME,
                                      FKNI012_IDESTADO
                                    FROM ALC012T_MUNICIPIO 
                                    WHERE FKNI012_IDESTADO= :IDESTADO
                                    ORDER BY ATSF012_NOME";

                conn.Open();

                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(":IDESTADO",IdEstado);

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        List<Municipio> lstMunicipio = new List<Municipio>();
                        while (reader.Read())
                        {
                            Municipio objMunicipio = new Municipio();

                            objMunicipio.IdMunicipio = Convert.ToInt32(reader[0]);
                            objMunicipio.Nome = reader[1].ToString();
                            objMunicipio.IdEstado = Convert.ToInt32(reader[2]);
                            lstMunicipio.Add(objMunicipio);
                        }
                        return lstMunicipio;
                    }
                }
            }
        }
    }
}