using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DAL
{
    public class TelefoneDAL
    {
        public bool AtualizarTelefone(Telefone telefone,ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC009T_TELEFONE
                                    SET 
                                    ATNI009_DDD            = :DDD,
                                    ATSF009_NUMERO         = :NUMERO,
                                    FKNI009_IDTIPOTELEFONE = :IDTIPOTELEFONE ";
                    query += "WHERE FKNI009_IDPESSOA = :IDPESSOA";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        
                        cmd.Parameters.Add(":DDD", telefone.DDD );
                        cmd.Parameters.Add(":NUMERO", telefone.Numero);
                        cmd.Parameters.Add(":IDTIPOTELEFONE", telefone.IdTipoTelefone);
                        cmd.Parameters.Add(":IDPESSOA", telefone.IdPessoa);

                        return Convert.ToBoolean(cmd.ExecuteNonQuery());
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }
    }
}
