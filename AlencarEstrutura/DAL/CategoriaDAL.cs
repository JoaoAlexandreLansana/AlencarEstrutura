using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DAL
{
    public class CategoriaDAL
    {
        public bool InserirCategoria(Categoria categoria, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"INSERT
                                        INTO ALC011T_CATEGORIA
                                          (
                                            ATSF017_NOME,
                                            ATSF017_OBSERVACAO
                                          )
                                          VALUES
                                          (
                                            :NOME,
                                            :OBSERVACAO
                                          )";
                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":NOME", categoria.Descricao);
                        cmd.Parameters.Add(":OBSERVACAO", categoria.Observacao);
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

        public List<Categoria> ObterListaDeCategoria(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI017_IDCATEGORIA,
                                          ATSF017_NOME,
                                          ATSF017_OBSERVACAO
                                        FROM ALC011T_CATEGORIA 
                                        ORDER BY PKNI017_IDCATEGORIA";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            List<Categoria> lstCategoriao = new List<Categoria>();
                            while (reader.Read())
                            {
                                Categoria objCategoria = new Categoria();

                                objCategoria.IdCategoria = Convert.ToInt32(reader[0]);
                                objCategoria.Descricao = reader[1].ToString();
                                objCategoria.Observacao = reader[2].ToString();
                                lstCategoriao.Add(objCategoria);
                            }
                            return lstCategoriao;
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

        public bool AtulizaCategoria(Categoria categoria, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC011T_CATEGORIA
                                    SET";
                    if (!string.IsNullOrEmpty(categoria.Descricao))
                    {
                        query += @" ATSF017_NOME = :NOME ";
                    }
                    if (!string.IsNullOrEmpty(categoria.Observacao))
                    {
                        query += @" ,ATSF017_OBSERVACAO = :OBSERVACAO ";
                    }

                    query += "WHERE PKNI017_IDCATEGORIA = :IDCATEGORIA";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":NOME", categoria.Descricao);
                        cmd.Parameters.Add(":OBSERVACAO", categoria.Observacao);
                        cmd.Parameters.Add(":IDCATEGORIA", categoria.IdCategoria);

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

        public Categoria ObterCategoriaPorID(int IdCategoria, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI017_IDCATEGORIA,
                                          ATSF017_NOME,
                                          ATSF017_OBSERVACAO
                                        FROM ALC011T_CATEGORIA 
                                        WHERE PKNI017_IDCATEGORIA=:IDCATEGORIA ";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add("IDCATEGORIA", IdCategoria);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            Categoria objCategoria = new Categoria();
                            if (reader.Read())
                            {
                                objCategoria.IdCategoria = Convert.ToInt32(reader[0]);
                                objCategoria.Descricao = reader[1].ToString();
                                objCategoria.Observacao = reader[2].ToString();
                            }
                            return objCategoria;
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

        public bool ExcluirCategoria(int IdCategoria, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"DELETE
                                        FROM ALC011T_CATEGORIA 
                                        WHERE PKNI017_IDCATEGORIA=:IDCATEGORIA ";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add("PKNI017_IDCATEGORIA", IdCategoria);

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