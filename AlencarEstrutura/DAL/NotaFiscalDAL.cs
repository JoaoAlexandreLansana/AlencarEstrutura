using AlencarEstrutura.DataSets;
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
    public class NotaFiscalDAL
    {
        public bool InserirNotaFiscal(NotaFiscal notaFiscal, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"INSERT
                                        INTO ALC021T_NOTASFISCAIS
                                          (
                                            FKNI021_IDORCAMENTO,
                                            ATDT021_VENCIMENTO,
                                            ATDT021_DATA,
                                            ATNI021_STATUS,
                                            FKNI021_IDEMPRESA,
                                            ATDC021_VALOR,
                                            ATNI021_PARCELAS,
                                            FKNI021_IDPESSOA
                                          )
                                          VALUES
                                          (
                                            :IDORCAMENTO,
                                            :ATDT021_VENCIMENTO,
                                            :ATDT021_DATA,
                                            :ATNI021_STATUS,
                                            :FKNI021_IDEMPRESA,
                                            :VALOR,
                                            :PARCELAS,
                                            :IDPESSOA
                                          )";
                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDORCAMENTO", notaFiscal.idOrcamento);
                        cmd.Parameters.Add(":ATDT021_VENCIMENTO", notaFiscal.Vencimento);
                        cmd.Parameters.Add(":ATDT021_DATA", notaFiscal.DataEmissao);
                        cmd.Parameters.Add(":ATNI021_STATUS", notaFiscal.Status);
                        cmd.Parameters.Add(":FKNI021_IDEMPRESA", 0);
                        cmd.Parameters.Add(":VALOR", notaFiscal.Valor);
                        cmd.Parameters.Add(":PARCELAS", notaFiscal.Parcelas);
                        cmd.Parameters.Add(":IDPESSOA", notaFiscal.IdPessoa);

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

        public List<NotaFiscal> ObterListaDeNotaFiscais(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI021_IDNOTASFISCAIS,
                                      FKNI021_IDORCAMENTO,
                                      ATDT021_VENCIMENTO,
                                      ATDT021_DATA,
                                      ATNI021_STATUS,
                                      FKNI021_IDEMPRESA,
                                      ATDC021_VALOR,
                                      ATNI021_PARCELAS
                                    FROM ALC021T_NOTASFISCAIS ";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            List<NotaFiscal> lstNotaFiscal = new List<NotaFiscal>();
                            while (reader.Read())
                            {
                                NotaFiscal objNotaFiscal = new NotaFiscal();

                                objNotaFiscal.IdNotaFiscal = Convert.ToInt32(reader[0]);
                                objNotaFiscal.idOrcamento = Convert.ToInt32(reader[1]);
                                objNotaFiscal.Vencimento = Convert.ToDateTime(reader[2]);
                                objNotaFiscal.DataEmissao = Convert.ToDateTime(reader[3]);
                                objNotaFiscal.Status = Convert.ToInt32(reader[4]);
                                objNotaFiscal.IdEmpresa = Convert.ToInt32(reader[5]);
                                objNotaFiscal.Valor = Convert.ToDecimal(reader[6]);
                                objNotaFiscal.Parcelas = Convert.ToInt32(reader[7]);

                                lstNotaFiscal.Add(objNotaFiscal);
                            }
                            return lstNotaFiscal;
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

        public bool AtualizaNotaFiscal(NotaFiscal notaFiscal, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC021T_NOTASFISCAIS
                                    SET
                                    FKNI021_IDORCAMENTO      = :IDORCAMENTO,
                                    ATDT021_VENCIMENTO       = :VENCIMENTO,
                                    ATDT021_DATA             = :DATA,
                                    ATNI021_STATUS           = :STATUS,
                                    FKNI021_IDEMPRESA        = :IDEMPRESA,
                                    ATDC021_VALOR            = :VALOR,
                                    ATNI021_PARCELAS         = :PARCELAS
                                    WHERE PKNI021_IDNOTASFISCAIS = :IDNOTASFISCAIS
                                    ";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDORCAMENTO", notaFiscal.idOrcamento);
                        cmd.Parameters.Add(":VENCIMENTO", notaFiscal.Vencimento);
                        cmd.Parameters.Add(":DATA", notaFiscal.DataEmissao);
                        cmd.Parameters.Add(":STATUS", notaFiscal.Status);
                        cmd.Parameters.Add(":IDEMPRESA", notaFiscal.IdEmpresa);
                        cmd.Parameters.Add(":VALOR", notaFiscal.Valor);
                        cmd.Parameters.Add(":PARCELAS", notaFiscal.Parcelas);
                        cmd.Parameters.Add(":IDNOTASFISCAIS", notaFiscal.IdEmpresa);

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

        public NotaFiscal ObterNotaFiscalPorID(int IdNotaFiscal, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI021_IDNOTASFISCAIS,
                                      FKNI021_IDORCAMENTO,
                                      ATDT021_VENCIMENTO,
                                      ATDT021_DATA,
                                      ATNI021_STATUS,
                                      FKNI021_IDEMPRESA,
                                      ATDC021_VALOR,
                                      ATNI021_PARCELAS
                                    FROM ALC021T_NOTASFISCAIS
                                    WHERE PKNI021_IDNOTASFISCAIS = :IDNOTASFISCAIS";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add("IDNOTASFISCAIS", IdNotaFiscal);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            NotaFiscal objNotaFiscal = new NotaFiscal();
                            if (reader.Read())
                            {
                                objNotaFiscal.IdNotaFiscal = Convert.ToInt32(reader[0]);
                                objNotaFiscal.idOrcamento = Convert.ToInt32(reader[1]);
                                objNotaFiscal.Vencimento = Convert.ToDateTime(reader[2]);
                                objNotaFiscal.DataEmissao = Convert.ToDateTime(reader[3]);
                                objNotaFiscal.Status = Convert.ToInt32(reader[4]);
                                objNotaFiscal.IdEmpresa = Convert.ToInt32(reader[5]);
                                objNotaFiscal.Valor = Convert.ToDecimal(reader[6]);
                                objNotaFiscal.Parcelas = Convert.ToInt32(reader[7]);
                            }
                            return objNotaFiscal;
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

        public NotaFiscal ObterNotaFiscalPorIDOrcamento(int IdOrcamento, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI021_IDNOTASFISCAIS,
                                      FKNI021_IDORCAMENTO,
                                      ATDT021_VENCIMENTO,
                                      ATDT021_DATA,
                                      ATNI021_STATUS,
                                      FKNI021_IDEMPRESA,
                                      ATDC021_VALOR,
                                      ATNI021_PARCELAS
                                    FROM ALC021T_NOTASFISCAIS
                                    WHERE FKNI021_IDORCAMENTO = :IDORCAMENTO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add("IDORCAMENTO", IdOrcamento);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            NotaFiscal objNotaFiscal = new NotaFiscal();
                            if (reader.Read())
                            {
                                objNotaFiscal.IdNotaFiscal = Convert.ToInt32(reader[0]);
                                objNotaFiscal.idOrcamento = Convert.ToInt32(reader[1]);
                                objNotaFiscal.Vencimento = Convert.ToDateTime(reader[2]);
                                objNotaFiscal.DataEmissao = Convert.ToDateTime(reader[3]);
                                objNotaFiscal.Status = Convert.ToInt32(reader[4]);
                                objNotaFiscal.IdEmpresa = Convert.ToInt32(reader[5]);
                                objNotaFiscal.Valor = Convert.ToDecimal(reader[6]);
                                objNotaFiscal.Parcelas = Convert.ToInt32(reader[7]);
                            }
                            return objNotaFiscal;
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

        public bool ExcluirNotaFiscal(int IdNotaFiscal, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"DELETE
                                        FROM ALC021T_NOTASFISCAIS 
                                        WHERE PKNI021_IDNOTASFISCAIS = :IDNOTASFISCAIS ";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add("IDNOTASFISCAIS", IdNotaFiscal);

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

        public LSN023_NOTAFISCAL GetData(int idNotaFiscal, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT 
                                 TB1.PKNI021_IDNOTASFISCAIS,
                                 TB1.FKNI021_IDORCAMENTO,
                                 TB1.ATDT021_VENCIMENTO,
                                 TB1.ATDT021_DATA,
                                 TB1.ATNI021_STATUS,
                                 TB1.FKNI021_IDEMPRESA,
                                 TB1.ATDC021_VALOR,
                                 TB1.ATNI021_PARCELAS,
                                 TB2.FKNI022_IDPRODUTO,
                                 TB2.ATNI022_QUANTIDADE,
                                 TB2.ATDC022_VALOR,
                                 TB2.ATNI022_VALOR_UNITARIO,
                                 TB2.ATDC022_QTDE_METRO_QUADRADO,
                                 TB3.ATSF002_NOMEFANTASIA,
                                 TB3.ATSF002_EMAIL,
                                 TB3.ATSF002_RAZAOSOCIAL,
                                 TB3.ATNI002_CNPJ,
                                 TB4.ATSF008_LOGRADOURO,
                                 TB4.ATNI008_NUMERO,
                                 TB4.ATSF008_BAIRRO,
                                 TB4.ATSF008_CEP,
                                 TB5.ATNI009_DDD,
                                 TB5.ATSF009_NUMERO AS TELEFONE,
                                 TB6.ATSF012_NOME AS CIDADE,
                                 TB7.ATSF013_SIGLA,
                                 TB8.ATSF003_DESCRICAO AS DESCPRODUTO,
                                 TB8.ATDC003_VALOR,
                                 TB8.ATDC003_VALOR_METRO,
                                 TB8.ATSF003_OBSERVACAO,
                                 TB9.ATSF014_NOME,
                                 TB9.ATSF014_EMAIL,
                                 TB9.ATSF014_TIPOPESSOA
                                 FROM ALC021T_NOTASFISCAIS TB1
                                 INNER JOIN ALC022T_PRODUTO_ORCAMENTO TB2
                                 ON TB1.FKNI021_IDORCAMENTO = TB2.FKNI022_IDORCAMENTO
                                 INNER JOIN ALC002T_EMPRESA TB3
                                 ON TB1.FKNI021_IDEMPRESA = TB3.PKNI002_IDEMPRESA
                                 INNER JOIN ALC008T_ENDERECO TB4
                                 ON TB3.FKNI002_IDPESSOA = TB4.FKNI008_IDPESSOA
                                 INNER JOIN ALC009T_TELEFONE TB5
                                 ON TB3.FKNI002_IDPESSOA = TB5.FKNI009_IDPESSOA
                                 INNER JOIN ALC012T_MUNICIPIO TB6
                                 ON TB4.FKNI008_IDMUNICIPIO = TB6.PKNI012_IDMUNICIPIO
                                 INNER JOIN ALC013T_ESTADO TB7
                                 ON TB4.FKNI008_IDESTADO = TB7.PKNI013_IDESTADO
                                 INNER JOIN ALC003T_PRODUTO TB8
                                 ON TB2.FKNI022_IDPRODUTO = TB8.PKNI003_IDPRODUTO
                                 INNER JOIN ALC014T_PESSOA TB9
                                 ON TB1.FKNI021_IDPESSOA = TB9.PKNI014_IDPESSOA
                                WHERE TB1.FKNI021_IDORCAMENTO = :IDNOTAFISCAL
                                    ";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":IDNOTAFISCAL", idNotaFiscal);

                        OracleDataAdapter da = new OracleDataAdapter();
                        da.SelectCommand = cmd;

                        using (LSN023_NOTAFISCAL dataSet = new LSN023_NOTAFISCAL())
                        {
                            da.Fill(dataSet, "BUSCA_NOTA_FISCAL");
                            return dataSet;
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

        public int ObterNotasFiscaisPendentes(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT COUNT(*)
                                     FROM ALC021T_NOTASFISCAIS 
                                     WHERE ATNI021_STATUS = 0 
                                     AND ATDT021_VENCIMENTO < :HOJE";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":HOJE", DateTime.Now);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            NotaFiscal objNotaFiscal = new NotaFiscal();
                            if (reader.Read())
                            {
                                return Convert.ToInt32(reader[0]);
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return 0;
            }
        }

        public int ObterNotasFiscaisAVencer(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT COUNT(*)
                                     FROM ALC021T_NOTASFISCAIS 
                                     WHERE ATNI021_STATUS = 0 
                                     AND ATDT021_VENCIMENTO = :HOJE
                                     OR ATDT021_VENCIMENTO = :AMANHA";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":HOJE", DateTime.Now);
                        cmd.Parameters.Add(":AMANHA", DateTime.Now.AddDays(1));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            NotaFiscal objNotaFiscal = new NotaFiscal();
                            if (reader.Read())
                            {
                                return Convert.ToInt32(reader[0]);
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return 0;
            }
        }

        public DataTable ObterListaDeNotaFiscaisAVencidas(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI021_IDNOTASFISCAIS,
                                      FKNI021_IDORCAMENTO,
                                      ATDT021_VENCIMENTO,
                                      ATDT021_DATA,
                                      ATNI021_STATUS,
                                      FKNI021_IDEMPRESA,
                                      ATDC021_VALOR,
                                      ATNI021_PARCELAS
                                    FROM ALC021T_NOTASFISCAIS 
                                    WHERE ATNI021_STATUS = 0 
                                     AND ATDT021_VENCIMENTO < :HOJE";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":HOJE", DateTime.Now);

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
    }
}