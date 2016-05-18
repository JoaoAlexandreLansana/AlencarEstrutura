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

        public Pessoa CarregaEmpresa()
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
            {
                string query = @"SELECT
                                      PKNI002_IDEMPRESA,
                                      ATSF002_NOMEFANTASIA,
                                      ATSF002_RAZAOSOCIAL,
                                      ATSF002_EMAIL,
                                      ATNI002_CNPJ,
                                      FKNI002_IDPESSOA,
                                      TB2.ATSF008_LOGRADOURO,
                                      TB2.ATNI008_NUMERO ENDERECO_NUMERO,
                                      TB2.ATSF008_BAIRRO,
                                      TB2.ATSF008_CEP,
                                      TB3.ATNI009_DDD,
                                      TB3.ATSF009_NUMERO TELEFONE,
                                      TB3.FKNI009_IDTIPOTELEFONE,
                                      TB4.PKNI012_IDMUNICIPIO,
                                      TB4.ATSF012_NOME CIDADE,
                                      TB5.PKNI013_IDESTADO,
                                      TB5.ATSF013_SIGLA,
                                      TB5.ATSF013_NOME ESTADO
                                       FROM
                                       ALC002T_EMPRESA TB1                                       
                                       INNER JOIN ALC008T_ENDERECO TB2
                                        ON TB1.FKNI002_IDPESSOA = TB2.FKNI008_IDPESSOA
                                        INNER JOIN ALC009T_TELEFONE TB3
                                        ON TB1.FKNI002_IDPESSOA = TB3.FKNI009_IDPESSOA
                                        INNER JOIN ALC012T_MUNICIPIO TB4
                                        ON TB2.FKNI008_IDMUNICIPIO = TB4.PKNI012_IDMUNICIPIO
                                        INNER JOIN ALC013T_ESTADO TB5
                                        ON TB2.FKNI008_IDESTADO = TB5.PKNI013_IDESTADO
                                        INNER JOIN ALC014T_PESSOA TB6
                                       ON TB1.FKNI002_IDPESSOA = TB6.PKNI014_IDPESSOA
                                       WHERE PKNI002_IDEMPRESA = 1 ";

                conn.Open();

                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;


                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Pessoa dbPessoa = new Pessoa();

                            dbPessoa.IdPessoa = Convert.ToInt32(reader["FKNI002_IDPESSOA"]);
                            dbPessoa.NomeFantasia = reader["ATSF002_NOMEFANTASIA"].ToString();
                            dbPessoa.RazaoSocial = reader["ATSF002_RAZAOSOCIAL"].ToString();
                            dbPessoa.Email = reader["ATSF002_EMAIL"].ToString();
                            dbPessoa.CNPJ = Convert.ToInt64(reader["ATNI002_CNPJ"]);

                            dbPessoa.endereco = new Endereco();
                            dbPessoa.endereco.Logradouro = reader["ATSF008_LOGRADOURO"].ToString();
                            dbPessoa.endereco.Numero = reader["ENDERECO_NUMERO"].ToString();
                            dbPessoa.endereco.Bairro = reader["ATSF008_BAIRRO"].ToString();
                            dbPessoa.endereco.CEP = reader["ATSF008_CEP"].ToString();
                            dbPessoa.endereco.IdMunicipio = Convert.ToInt32(reader["PKNI012_IDMUNICIPIO"]);
                            dbPessoa.endereco.IdEstado = Convert.ToInt32(reader["PKNI013_IDESTADO"]);
                            dbPessoa.endereco.NomeMunicipio = reader["CIDADE"].ToString();
                            dbPessoa.endereco.NomeEstado = reader["PKNI013_IDESTADO"].ToString();
                            dbPessoa.endereco.Sigla = reader["ATSF013_SIGLA"].ToString();

                            dbPessoa.telefone = new Telefone();
                            dbPessoa.telefone.DDD = Convert.ToInt32(reader["ATNI009_DDD"]);
                            dbPessoa.telefone.Numero = reader["TELEFONE"].ToString();

                            return dbPessoa;
                        }
                        return null;
                    }
                }
            }
        }

        public bool AtualizarEmpresa(Pessoa pessoa, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC002T_EMPRESA
                                    SET
                                    ATSF002_NOMEFANTASIA = :NOMEFANTASIA,
                                    ATSF002_RAZAOSOCIAL  = :RAZAO,
                                    ATSF002_EMAIL        = :EMAIL,
                                    ATNI002_CNPJ         = :CNPJ,
                                    FKNI002_IDPESSOA     = :IDPESSOA
                                    WHERE PKNI002_IDEMPRESA  = 1";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":NOMEFANTASIA", pessoa.NomeFantasia);
                        cmd.Parameters.Add(":RAZAO", pessoa.RazaoSocial);
                        cmd.Parameters.Add(":EMAIL", pessoa.Email);
                        cmd.Parameters.Add(":CNPJ", pessoa.CNPJ);
                        cmd.Parameters.Add(":IDPESSOA", pessoa.IdPessoa);

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