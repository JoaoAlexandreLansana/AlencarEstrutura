using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DAL
{
    public class ProdutoDAL
    {
        public Produto ObterProdutoPorID(int IdProduto, ref string erro)
        {
            try
            {

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @" SELECT PKNI003_IDPRODUTO,
                                      ATSF003_DESCRICAO,
                                      FKNI003_IDCATEGORIA,
                                      ATSF003_OBSERVACAO,
                                      to_char(ATDC003_VALOR,'999G999G999G999D99') as ATDC003_VALOR,
                                      ATDC003_PESO,
                                      ATDC003_LITROS,
                                      to_char(ATDC003_VALOR_METRO,'999G999G999G999D99') as ATDC003_VALOR_METRO
                                    FROM ALC003T_PRODUTO
                                    WHERE PKNI003_IDPRODUTO=:IDPRODUTO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":IDPRODUTO", IdProduto);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            Produto objProduto = new Produto();

                            if (reader.Read())
                            {
                                objProduto.IdProduto = Convert.ToInt32(reader[0]);
                                objProduto.Descricao = reader[1].ToString();
                                objProduto.IdCategoria = Convert.ToInt32(reader[2]);
                                objProduto.Observacao = reader[3].ToString();
                                objProduto.Valor = (reader[4] == DBNull.Value) ? 0 : Convert.ToDouble(reader[4]);
                                objProduto.Peso = (reader[5] == DBNull.Value) ? 0 : Convert.ToDecimal(reader[5]);
                                objProduto.Litros = (reader[6] == DBNull.Value) ? 0 : Convert.ToDecimal(reader[6]);
                                objProduto.ValorPorMetro = (reader[7] == DBNull.Value) ? 0 : Convert.ToDecimal(reader[7]);

                                return objProduto;
                            }
                            return objProduto;
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

        public List<Produto> ObterListadeProduto(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @" SELECT PKNI003_IDPRODUTO,
                                      ATSF003_DESCRICAO,
                                      FKNI003_IDCATEGORIA,
                                      ATSF003_OBSERVACAO,
                                      to_char(ATDC003_VALOR,'999G999G999G999D99') as ATDC003_VALOR,
                                      ATDC003_PESO,
                                      ATDC003_LITROS,
                                      to_char(ATDC003_VALOR_METRO,'999G999G999G999D99') as ATDC003_VALOR_METRO
                                    FROM ALC003T_PRODUTO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            List<Produto> lstProduto = new List<Produto>();

                            while (reader.Read())
                            {
                                Produto objProduto = new Produto();

                                objProduto.IdProduto = Convert.ToInt32(reader[0]);
                                objProduto.Descricao = reader[1].ToString();
                                objProduto.IdCategoria = (reader[2] == DBNull.Value) ? 0 : Convert.ToInt32(reader[2]);
                                objProduto.Observacao = reader[3].ToString();
                                objProduto.Valor = (reader[4] == DBNull.Value) ? 0 : Convert.ToDouble(reader[4]);
                                objProduto.Peso = (reader[5] == DBNull.Value) ? 0 : Convert.ToDecimal(reader[5]);
                                objProduto.Litros = (reader[6] == DBNull.Value) ? 0 : Convert.ToDecimal(reader[6]);
                                objProduto.ValorPorMetro = (reader[7] == DBNull.Value) ? 0 : Convert.ToDecimal(reader[7]);

                                lstProduto.Add(objProduto);
                            }
                            return lstProduto;
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

        public List<Produto> PesquisarListadeProduto(string produto, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @" SELECT PKNI003_IDPRODUTO,
                                      ATSF003_DESCRICAO,
                                      FKNI003_IDCATEGORIA,
                                      ATSF003_OBSERVACAO,
                                      to_char(ATDC003_VALOR,'999G999G999G999D99') as ATDC003_VALOR,
                                      ATDC003_PESO,
                                      ATDC003_LITROS,
                                      to_char(ATDC003_VALOR_METRO,'999G999G999G999D99') as ATDC003_VALOR_METRO
                                    FROM ALC003T_PRODUTO
                                    WHERE ATSF003_DESCRICAO LIKE '%'||:PRODUTO||'%'";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":PRODUTO", produto);
                        
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            List<Produto> lstProduto = new List<Produto>();

                            while (reader.Read())
                            {
                                Produto objProduto = new Produto();

                                objProduto.IdProduto = Convert.ToInt32(reader[0]);
                                objProduto.Descricao = reader[1].ToString();
                                objProduto.IdCategoria = (reader[2] == DBNull.Value) ? 0 : Convert.ToInt32(reader[2]);
                                objProduto.Observacao = reader[3].ToString();
                                objProduto.Valor = (reader[4] == DBNull.Value) ? 0 : Convert.ToDouble(reader[4]);
                                objProduto.Peso = (reader[5] == DBNull.Value) ? 0 : Convert.ToDecimal(reader[5]);
                                objProduto.Litros = (reader[6] == DBNull.Value) ? 0 : Convert.ToDecimal(reader[6]);
                                objProduto.ValorPorMetro = (reader[7] == DBNull.Value) ? 0 : Convert.ToDecimal(reader[7]);

                                lstProduto.Add(objProduto);
                            }
                            return lstProduto;
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
        public bool InserirProduto(Produto produto, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"INSERT
                                        INTO ALC003T_PRODUTO
                                          (
                                            ATSF003_DESCRICAO,
                                            FKNI003_IDCATEGORIA,
                                            ATSF003_OBSERVACAO,
                                            ATDC003_VALOR,
                                            ATDC003_PESO,
                                            ATDC003_LITROS,
                                            ATDC003_VALOR_METRO,
                                            PKNI003_IDEMPRESA
                                          )
                                          VALUES
                                          (
                                            :DESCRICAO,
                                            :IDCATEGORIA,
                                            :OBSERVACAO,
                                            :VALOR,
                                            :PESO,
                                            :LITROS,
                                            :VALORMETRO,
                                            :IDEMPRESA
                                          )";
                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":DESCRICAO", produto.Descricao);
                        cmd.Parameters.Add(":IDCATEGORIA", produto.IdCategoria);
                        cmd.Parameters.Add(":OBSERVACAO", produto.Observacao);
                        cmd.Parameters.Add(":VALOR", produto.Valor);
                        cmd.Parameters.Add(":PESO", produto.Peso);
                        cmd.Parameters.Add(":LITROS", produto.Litros);
                        cmd.Parameters.Add(":VALORMETRO", produto.ValorPorMetro);
                        cmd.Parameters.Add(":IDEMPRESA", 1);
                        int reader = cmd.ExecuteNonQuery();
                        sucesso = Convert.ToBoolean(reader);
                    }
                }

                return sucesso;

            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }

        }

        public bool AtualizarProdutoPorId(Produto produto, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC003T_PRODUTO
                                    SET ATSF003_DESCRICAO   = :DESCRICAO 
                                        ,FKNI003_IDCATEGORIA = :IDCATEGORIA 
                                        ,ATSF003_OBSERVACAO = :OBSERVACAO 
                                        ,ATDC003_VALOR = :VALOR 
                                        ,ATDC003_PESO = :PESO 
                                        ,ATDC003_LITROS = :LITROS 
                                        ,ATDC003_VALOR_METRO = :VALORMETRO 
                                        WHERE PKNI003_IDPRODUTO = :IDPRODUTO";
                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":DESCRICAO", produto.Descricao);
                        cmd.Parameters.Add(":IDCATEGORIA", produto.IdCategoria);
                        cmd.Parameters.Add(":OBSERVACAO", produto.Observacao);
                        cmd.Parameters.Add(":VALOR", produto.Valor);
                        cmd.Parameters.Add(":PESO", produto.Peso);
                        cmd.Parameters.Add(":LITROS", produto.Litros);
                        cmd.Parameters.Add(":VALORMETRO", produto.Litros);
                        cmd.Parameters.Add(":IDPRODUTO", produto.IdProduto);
                        int reader = cmd.ExecuteNonQuery();
                        sucesso = Convert.ToBoolean(reader);
                    }
                }

                return sucesso;

            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public bool ExcluirProduto(int IdProduto, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"DELETE FROM ALC003T_PRODUTO WHERE PKNI003_IDPRODUTO=:IDPRODUTO";
                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDPRODUTO", IdProduto);
                        int reader = cmd.ExecuteNonQuery();
                        sucesso = Convert.ToBoolean(reader);
                    }
                }
                return sucesso;
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }

        }
    }
}