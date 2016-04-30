using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DAL
{
    public class EstoqueDAL
    {
        public Estoque ObterEstoquePorID(int IdEstoque, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI004_IDESTOQUE,
                                      ATSF004_DESCRICAO,
                                      FKNI004_IDPRODUTO,
                                      ATDT004_VALIDADE,
                                      ATSF004_QUANTIDADE,
                                      ATSF004_OBSERVACAO
                                    FROM ALC004T_ESTOQUE WHERE PKNI004_IDESTOQUE = :IDESTOQUE";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":IDESTOQUE", IdEstoque);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            Estoque objEstoque = new Estoque();
                            if (reader.Read())
                            {
                                objEstoque.IdEstoque = Convert.ToInt32(reader[0]);
                                objEstoque.Descricao = reader[1].ToString();
                                objEstoque.IdProduto = Convert.ToInt32(reader[2]);
                                objEstoque.Validade = Convert.ToDateTime(reader[3]);
                                objEstoque.Quantidade = Convert.ToDecimal(reader[4]);
                                objEstoque.Observacao = reader[5].ToString();
                            }
                            return objEstoque;
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

        public List<Estoque> ObterListaEstoque(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI004_IDESTOQUE,
                                      ATSF004_DESCRICAO,
                                      FKNI004_IDPRODUTO,
                                      ATDT004_VALIDADE,
                                      ATSF004_QUANTIDADE,
                                      ATSF004_OBSERVACAO
                                    FROM ALC004T_ESTOQUE";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            List<Estoque> lstEstoque = new List<Estoque>();
                            while (reader.Read())
                            {
                                Estoque objEstoque = new Estoque();

                                objEstoque.IdEstoque = Convert.ToInt32(reader[0]);
                                objEstoque.Descricao = reader[1].ToString();
                                objEstoque.IdProduto = Convert.ToInt32(reader[2]);
                                objEstoque.Validade = Convert.ToDateTime(reader[3]);
                                objEstoque.Quantidade = Convert.ToDecimal(reader[4]);
                                objEstoque.Observacao = reader[5].ToString();

                                lstEstoque.Add(objEstoque);
                            }
                            return lstEstoque;
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

        public bool InserirEstoque(Estoque estoque, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"INSERT
                                        INTO ALC004T_ESTOQUE
                                          (
                                                ATSF004_DESCRICAO,
                                                FKNI004_IDPRODUTO,
                                                ATDT004_VALIDADE,
                                                ATSF004_QUANTIDADE,
                                                ATSF004_OBSERVACAO
                                          )
                                          VALUES
                                          (
                                             :DESCRICAO
                                            ,:IDPRODUTO
                                            ,:VALIDADE
                                            ,:QUANTIDADE
                                            ,:OBSERVACAO
                                          )";
                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":DESCRICAO", estoque.Descricao);
                        cmd.Parameters.Add(":IDPRODUTO", estoque.IdProduto);
                        cmd.Parameters.Add(":VALIDADE", estoque.Validade);
                        cmd.Parameters.Add(":QUANTIDADE", estoque.Quantidade);
                        cmd.Parameters.Add(":OBSERVACAO", estoque.Observacao);

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

        public bool AtualizaEstoque(Estoque estoque, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC004T_ESTOQUE
                                    SET";
                    if (!string.IsNullOrEmpty(estoque.Descricao))
                    {
                        query += " ATSF004_DESCRICAO = :DESCRICAO";
                    }
                    if (!string.IsNullOrEmpty(estoque.Observacao))
                    {
                        query += " ,FKNI004_IDPRODUTO = :IDPRODUTO ";
                    }   
                    if (estoque.Validade != null)
                    {
                        query += " ,ATDT004_VALIDADE = :VALIDADE ";
                    }
                    if(estoque.Quantidade > 0)
                    {
                        query += " ,ATSF004_QUANTIDADE = :QUANTIDADE ";
                    }

                    if (!string.IsNullOrEmpty(estoque.Observacao))
                    {
                        query += " ,ATSF004_OBSERVACAO = :OBSERVACAO ";
                    }

                    query += " WHERE PKNI004_IDESTOQUE = :IDESTOQUE";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        
                        cmd.Parameters.Add(":DESCRICAO", estoque.Descricao);
                        cmd.Parameters.Add(":IDPRODUTO", estoque.IdProduto);
                        cmd.Parameters.Add(":VALIDADE", estoque.Validade);
                        cmd.Parameters.Add(":QUANTIDADE", estoque.Quantidade);
                        cmd.Parameters.Add(":OBSERVACAO", estoque.Observacao);
                        cmd.Parameters.Add(":IDESTOQUE", estoque.IdEstoque);

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

        public bool ExcluirEstoque(int IdEstoque, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"DELETE
                                        FROM ALC004T_ESTOQUE
                                        WHERE PKNI004_IDESTOQUE = :IDESTOQUE
                                          ";
                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":IDESTOQUE", IdEstoque);

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