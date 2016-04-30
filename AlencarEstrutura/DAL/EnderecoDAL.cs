using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DAL
{
    public class EnderecoDAL
    {
        public bool AtualizaEndereco(Endereco endereco, ref string erro )
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC008T_ENDERECO
                                    SET
                                    ATSF008_LOGRADOURO   = :LOGRADOURO,
                                    ATNI008_NUMERO       = :NUMERO,
                                    ATSF008_BAIRRO       = :BAIRRO,
                                    ATSF008_CEP          = :CEP,
                                    ATSF008_COMPLEMENTO  = :COMPLEMENTO,
                                    FKNI008_IDMUNICIPIO  = :IDMUNICIPIO,
                                    FKNI008_IDESTADO     = :IDESTADO ";
                    query += "WHERE FKNI008_IDPESSOA = :IDPESSOA";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDPESSOA", endereco.IdPessoa);

                        cmd.Parameters.Add(":LOGRADOURO", endereco.Numero);
                        cmd.Parameters.Add(":NUMERO", endereco.Numero);
                        cmd.Parameters.Add(":BAIRRO", endereco.Bairro);
                        cmd.Parameters.Add(":CEP", endereco.CEP);
                        cmd.Parameters.Add(":COMPLEMENTO", endereco.Complemento);
                        cmd.Parameters.Add(":IDMUNICIPIO", endereco.IdMunicipio);
                        cmd.Parameters.Add(":IDESTADO", endereco.IdEstado);

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