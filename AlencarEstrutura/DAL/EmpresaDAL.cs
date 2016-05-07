using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DAL
{
    public class EmpresaDAL
    {

        public  Pessoa CarregaEmpresa()
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
            {
                string query = @"SELECT
                                       PKNI002_IDEMPRESA,
                                       ATSF002_NOMEFANTASIA,
                                       ATSF002_RAZAOSOCIAL,
                                       ATSF002_EMAIL,
                                       ATNI002_CNPJ,
                                       FKNI002_IDPESSOA
                                       FROM
                                       ALC002T_EMPRESA  ";

                conn.Open();

                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;


                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Pessoa dbPessoa = new Pessoa();

                            dbPessoa.IdPessoa = Convert.ToInt32(reader[1]);
                            dbPessoa.NomeFantasia = reader[2].ToString();
                            dbPessoa.RazaoSocial = reader[3].ToString();
                            dbPessoa.Email = reader[4].ToString();
                            dbPessoa.CNPJ = Convert.ToInt32(reader[5]);
                            dbPessoa.IdPessoa = Convert.ToInt32(reader[6]);
                            

                            return dbPessoa;
                        }
                        return null;
                    }
                }
            }
        }
    }
}