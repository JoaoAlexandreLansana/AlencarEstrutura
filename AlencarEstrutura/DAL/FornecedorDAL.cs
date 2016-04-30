using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DAL
{
    public class FornecedorDAL
    {
        public Pessoa ObterFornecedorPorIDPessoa(int idPessoa, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT 
                                      TB1.PKNI014_IDPESSOA,
                                      TB1.ATSF014_EMAIL,
                                      TB1.ATSF014_TIPOPESSOA,
                                      TB1.ATSF014_SENHA,
                                      TB1.ATSF014_NOME NOME_PESSOA,
                                      TB2.ATNI007_CNPJ,
                                      TB2.ATSF007_NOMEFANTASIA,
                                      TB2.ATSF007_RAZAOSOCIAL,
                                      TB2.ATSF007_STATUS,
                                      TB2.PKNI007_IDFORNECEDOR,
                                      TB3.ATSF008_LOGRADOURO,
                                      TB3.ATNI008_NUMERO NUMERO,
                                      TB3.ATSF008_BAIRRO,
                                      TB3.ATSF008_COMPLEMENTO,
                                      TB3.ATSF008_CEP,
                                      TB4.ATNI009_DDD,
                                      TB4.ATSF009_NUMERO TELEFONE,
                                      TB7.ATSF016_DESCRICAO TIPO_TELEFONE,
                                      TB5.PKNI012_IDMUNICIPIO,
                                      TB5.ATSF012_NOME NOME_MUNICIPIO,
                                      TB6.PKNI013_IDESTADO,
                                      TB6.ATSF013_NOME NOME_ESTADO,
                                      TB6.ATSF013_SIGLA
                                    FROM ALC014T_PESSOA TB1
                                    INNER JOIN ALC007T_FORNECEDOR TB2
                                    ON TB1.PKNI014_IDPESSOA = TB2.FKNI007_IDPESSOA
                                    INNER JOIN ALC008T_ENDERECO TB3
                                    ON TB1.PKNI014_IDPESSOA = TB3.FKNI008_IDPESSOA
                                    INNER JOIN ALC009T_TELEFONE TB4
                                    ON TB1.PKNI014_IDPESSOA = TB4.FKNI009_IDPESSOA
                                    INNER JOIN ALC012T_MUNICIPIO TB5
                                    ON TB3.FKNI008_IDMUNICIPIO = TB5.PKNI012_IDMUNICIPIO
                                    INNER JOIN ALC013T_ESTADO TB6
                                    ON TB5.FKNI012_IDESTADO = TB6.PKNI013_IDESTADO
                                    INNER JOIN ALC010T_TIPOTELEFONE TB7
                                    ON TB4.FKNI009_IDTIPOTELEFONE = TB7.PKNI016_IDTIPOTELEFONE
                                    WHERE TB1.PKNI014_IDPESSOA = :IDPESSOA";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add("IDPESSOA", idPessoa);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            Pessoa objPessoa = new Pessoa();
                            objPessoa.endereco = new Endereco();
                            objPessoa.telefone = new Telefone();

                            if (reader.Read())
                            {
                                objPessoa.IdPessoa = Convert.ToInt32(reader["PKNI014_IDPESSOA"]);
                                objPessoa.Email = reader["ATSF014_EMAIL"].ToString();
                                objPessoa.TipoPessoa = reader["ATSF014_TIPOPESSOA"].ToString();
                                objPessoa.Senha = reader["ATSF014_SENHA"].ToString();
                                objPessoa.NomePessoa = reader["NOME_PESSOA"].ToString();
                                objPessoa.CNPJ = Convert.ToInt64(reader["ATNI007_CNPJ"]);
                                objPessoa.NomeFantasia = reader["ATSF007_NOMEFANTASIA"].ToString();
                                objPessoa.RazaoSocial = reader["ATSF007_RAZAOSOCIAL"].ToString();
                                objPessoa.Status = Convert.ToInt32(reader["ATSF007_STATUS"]);
                                objPessoa.endereco.Logradouro = reader["ATSF008_LOGRADOURO"].ToString();
                                objPessoa.endereco.Numero = reader["NUMERO"].ToString();
                                objPessoa.endereco.Bairro = reader["ATSF008_BAIRRO"].ToString();
                                objPessoa.endereco.Complemento = reader["ATSF008_COMPLEMENTO"].ToString();
                                objPessoa.endereco.CEP = reader["ATSF008_CEP"].ToString();
                                objPessoa.telefone.DDD = Convert.ToInt32(reader["ATNI009_DDD"]);
                                objPessoa.telefone.Numero = reader["TELEFONE"].ToString();
                                objPessoa.telefone.TipoTelefone = reader["TIPO_TELEFONE"].ToString();
                                objPessoa.endereco.NomeMunicipio = reader["NOME_MUNICIPIO"].ToString();
                                objPessoa.endereco.NomeEstado = reader["NOME_ESTADO"].ToString();
                                objPessoa.endereco.IdMunicipio = Convert.ToInt32(reader["PKNI012_IDMUNICIPIO"]);
                                objPessoa.endereco.IdEstado = Convert.ToInt32(reader["PKNI013_IDESTADO"]);
                                objPessoa.endereco.Sigla = reader["ATSF013_SIGLA"].ToString();
                            }
                            return objPessoa;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return null;
            }
        }

        public List<Pessoa> ObterListaFornecedor(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT
                                      TB1.PKNI014_IDPESSOA,
                                      TB1.ATSF014_EMAIL,
                                      TB1.ATSF014_TIPOPESSOA,
                                      TB1.ATSF014_SENHA,
                                      TB1.ATSF014_NOME NOME_PESSOA,
                                      TB2.ATNI007_CNPJ,
                                      TB2.ATSF007_NOMEFANTASIA,
                                      TB2.ATSF007_RAZAOSOCIAL,
                                      TB2.ATSF007_STATUS,
                                      TB2.PKNI007_IDFORNECEDOR,
                                      TB3.ATSF008_LOGRADOURO,
                                      TB3.ATNI008_NUMERO,
                                      TB3.ATSF008_BAIRRO,
                                      TB3.ATSF008_COMPLEMENTO,
                                      TB4.ATNI009_DDD,
                                      TB4.ATSF009_NUMERO,
                                      TB7.ATSF016_DESCRICAO TIPO_TELEFONE,
                                      TB5.PKNI012_IDMUNICIPIO,
                                      TB5.ATSF012_NOME NOME_MUNICIPIO,
                                      TB6.PKNI013_IDESTADO,
                                      TB6.ATSF013_NOME NOME_ESTADO,
                                      TB6.ATSF013_SIGLA
                                    FROM ALC014T_PESSOA TB1
                                    INNER JOIN ALC007T_FORNECEDOR TB2
                                    ON TB1.PKNI014_IDPESSOA = TB2.FKNI007_IDPESSOA
                                    INNER JOIN ALC008T_ENDERECO TB3
                                    ON TB1.PKNI014_IDPESSOA = TB3.FKNI008_IDPESSOA
                                    INNER JOIN ALC009T_TELEFONE TB4
                                    ON TB1.PKNI014_IDPESSOA = TB4.FKNI009_IDPESSOA
                                    INNER JOIN ALC012T_MUNICIPIO TB5
                                    ON TB3.FKNI008_IDMUNICIPIO = TB5.PKNI012_IDMUNICIPIO
                                    INNER JOIN ALC013T_ESTADO TB6
                                    ON TB5.FKNI012_IDESTADO = TB6.PKNI013_IDESTADO
                                    INNER JOIN ALC010T_TIPOTELEFONE TB7
                                    ON TB4.FKNI009_IDTIPOTELEFONE = TB7.PKNI016_IDTIPOTELEFONE";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            List<Pessoa> lstPessoa = new List<Pessoa>();
                            while (reader.Read())
                            {
                                Pessoa objPessoa = new Pessoa();
                                objPessoa.endereco = new Endereco();
                                objPessoa.telefone = new Telefone();

                                objPessoa.IdPessoa = Convert.ToInt32(reader["PKNI014_IDPESSOA"]);
                                objPessoa.Email = reader["ATSF014_EMAIL"].ToString();
                                objPessoa.TipoPessoa = reader["ATSF014_TIPOPESSOA"].ToString();
                                objPessoa.Senha = reader["ATSF014_SENHA"].ToString();
                                objPessoa.NomePessoa = reader["NOME_PESSOA"].ToString();
                                objPessoa.CNPJ = Convert.ToInt64(reader["ATNI007_CNPJ"]);
                                objPessoa.NomeFantasia = reader["ATSF007_NOMEFANTASIA"].ToString();
                                objPessoa.RazaoSocial = reader["ATSF007_RAZAOSOCIAL"].ToString();
                                objPessoa.Status = Convert.ToInt32(reader["ATSF007_STATUS"]);
                                objPessoa.endereco.Logradouro = reader["ATSF008_LOGRADOURO"].ToString();
                                objPessoa.endereco.Numero = reader["ATNI008_NUMERO"].ToString();
                                objPessoa.endereco.Bairro = reader["ATSF008_BAIRRO"].ToString();
                                objPessoa.endereco.Complemento = reader["ATSF008_COMPLEMENTO"].ToString();
                                objPessoa.telefone.DDD = Convert.ToInt32(reader["ATNI009_DDD"]);
                                objPessoa.telefone.Numero = reader["ATSF009_NUMERO"].ToString();
                                objPessoa.telefone.TipoTelefone = reader["TIPO_TELEFONE"].ToString();
                                objPessoa.endereco.NomeMunicipio = reader["NOME_MUNICIPIO"].ToString();
                                objPessoa.endereco.NomeEstado = reader["NOME_ESTADO"].ToString();
                                objPessoa.endereco.IdMunicipio = Convert.ToInt32(reader["PKNI012_IDMUNICIPIO"]);
                                objPessoa.endereco.IdEstado = Convert.ToInt32(reader["PKNI013_IDESTADO"]);
                                objPessoa.endereco.Sigla = reader["ATSF013_SIGLA"].ToString();
                                lstPessoa.Add(objPessoa);
                            }
                            return lstPessoa;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return null;
            }
        }

        public DataTable ObterListaNomeFornecedor(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT
                                      TB1.PKNI014_IDPESSOA as CODIGO,
                                      TB2.ATSF007_NOMEFANTASIA AS NOME
                                    FROM ALC014T_PESSOA TB1
                                    INNER JOIN ALC007T_FORNECEDOR TB2
                                    ON TB1.PKNI014_IDPESSOA = TB2.FKNI007_IDPESSOA"
                                    ;

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        DataTable dt = new DataTable();
                        OracleDataAdapter da = new OracleDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return null;
            }
        }

        public bool AtualizaFornecedor(Pessoa fornecedor, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC007T_FORNECEDOR
                                    SET 
                                    ATSF007_NOMEFANTASIA   = :NOMEFANTASIA,
                                    ATSF007_RAZAOSOCIAL    = :RAZAO,
                                    ATNI007_CNPJ           = :CNPJ,
                                    ATSF007_STATUS         = :STATUS ";
                    query += "WHERE FKNI007_IDPESSOA = :IDPESSOA";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":NOMEFANTASIA", fornecedor.NomeFantasia);
                        cmd.Parameters.Add(":RAZAO", fornecedor.RazaoSocial);
                        cmd.Parameters.Add(":CNPJ", fornecedor.CNPJ);
                        cmd.Parameters.Add(":STATUS", fornecedor.Status);
                        cmd.Parameters.Add(":IDPESSOA", fornecedor.IdPessoa);

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