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
    public class OrcamentoDAL
    {
        public Orcamento ObterOrcamentoPorId(int IdOrcamento, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI020_IDORCAMENTO,
                                            ATSF020_DESCRICAO,
                                            ATDT020_DATA,
                                            ATDT020_VENCIMENTO,
                                            FKNI020_IDPESSOA,
                                            ATDC020_VALOR,
                                            ATSF020_STATUS
                                        FROM ALC020T_ORCAMENTO WHERE PKNI020_IDORCAMENTO = :IDORCAMENTO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add("IDORCAMENTO", IdOrcamento);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            Orcamento objOrcamento = new Orcamento();

                            if (reader.Read())
                            {
                                objOrcamento.IdOrcamento = Convert.ToInt32(reader[0]);
                                objOrcamento.Descricao = reader[1].ToString();
                                objOrcamento.Data = Convert.ToDateTime(reader[2]);
                                objOrcamento.Vencimento = Convert.ToDateTime(reader[3]);
                                objOrcamento.IdPessoa = Convert.ToInt32(reader[4]);
                                objOrcamento.Valor = Convert.ToInt32(reader[5]);
                                objOrcamento.Status = Convert.ToInt32(reader[6]);
                            }
                            return objOrcamento;
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

        public DataTable ObterListaOrcamento(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI020_IDORCAMENTO,
                                            ATSF020_DESCRICAO,
                                            ATDT020_DATA,
                                            ATDT020_VENCIMENTO,
                                            FKNI020_IDPESSOA,
                                            ATDC020_VALOR,
                                            ATSF020_STATUS
                                        FROM ALC020T_ORCAMENTO";
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

        public int InserirOrcamento(Orcamento orcamento, ref string erro)
        {
            try
            {
                int idOrcamento = 0;
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("INSERIRORCAMENTO", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("pIDORCAMENTO", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add("pDESCRICAO", OracleDbType.Char).Value = orcamento.Descricao;
                        cmd.Parameters.Add("pDATA", OracleDbType.Char).Value = orcamento.Data;
                        cmd.Parameters.Add("pVENCIMENTO", OracleDbType.Char).Value = orcamento.Vencimento;
                        cmd.Parameters.Add("pIDPESSOA", OracleDbType.Char).Value = orcamento.IdPessoa;
                        cmd.Parameters.Add("pVALOR", OracleDbType.Int64).Value = orcamento.Valor;
                        cmd.Parameters.Add("pSTATUS", OracleDbType.Char).Value = orcamento.Status;

                        cmd.ExecuteNonQuery();
                        idOrcamento = Convert.ToInt32(cmd.Parameters["pIDORCAMENTO"].Value.ToString());                        
                    }
                }

                return idOrcamento;
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return 0;
            }
        }

        public bool AtualizarOrcamento(Orcamento orcamento, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC020T_ORCAMENTO
                                    SET
                                    ATSF020_DESCRICAO     = :DESCRICAO,
                                    ATDT020_DATA          = :DATA,
                                    ATDT020_VENCIMENTO    = :VENCIMENTO,
                                    FKNI020_IDPESSOA      = :IDPESSOA,
                                    ATDC020_VALOR         = :VALOR,
                                    ATSF020_STATUS        = :STATUS
                                WHERE PKNI020_IDORCAMENTO = :IDVENCIMENTO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":DESCRICAO", orcamento.Descricao);
                        cmd.Parameters.Add(":DATA", orcamento.Data);
                        cmd.Parameters.Add(":VENCIMENTO", orcamento.Vencimento);
                        cmd.Parameters.Add(":IDPESSOA", orcamento.IdPessoa);
                        cmd.Parameters.Add(":VALOR", orcamento.Valor);
                        cmd.Parameters.Add(":STATUS", orcamento.Status);

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

        public bool ExcluirOrcamento(int idOrcamento, ref string erro)
        {

            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"DELETE
                                            FROM ALC020T_ORCAMENTO
                                            WHERE PKNI020_IDORCAMENTO = :IDORCAMENTO
                                          ";
                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":IDORCAMENTO", idOrcamento);

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

        public bool AdicionarProduto(Orcamento orcamento, ref string erro, int idOrcamento)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"INSERT
                                        INTO ALC022T_PRODUTO_ORCAMENTO
                                          (
                                            FKNI022_IDPRODUTO,
                                            FKNI022_IDORCAMENTO,
                                            ATNI022_QUANTIDADE,
                                            ATDC022_VALOR
                                          )
                                          VALUES
                                          (
                                            :IDPRODUTO,
                                            :IDORCAMENTO,
                                            :QUANTIDADE,
                                            :VALOR
                                          )";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDPRODUTO", orcamento.IdProduto);
                        cmd.Parameters.Add(":IDORCAMENTO", idOrcamento);
                        cmd.Parameters.Add(":QUANTIDADE", orcamento.Quantidade);
                        cmd.Parameters.Add(":VALOR", orcamento.Valor);

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