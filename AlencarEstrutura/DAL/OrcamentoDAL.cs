using AlencarEstrutura.DataSets;
using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;

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
                                            TRUNC (ATDC020_VALOR, 2) AS ATDC020_VALOR,
                                            ATSF020_STATUS,
                                            TB2.ATSF014_NOME,
                                            TB2.ATSF014_EMAIL
                                        FROM ALC020T_ORCAMENTO TB1
                                        INNER JOIN ALC014T_PESSOA TB2
                                        ON TB1.FKNI020_IDPESSOA = TB2.PKNI014_IDPESSOA WHERE PKNI020_IDORCAMENTO = :IDORCAMENTO";

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
                                objOrcamento.Valor = Convert.ToDouble(reader[5]);
                                objOrcamento.Status = Convert.ToInt32(reader[6]);
                                objOrcamento.NomeCliente = reader[7].ToString();
                                objOrcamento.EmailCliente = reader[8].ToString();
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
                                            TRUNC (ATDC020_VALOR, 2) AS ATDC020_VALOR,
                                            ATSF020_STATUS
                                        FROM ALC020T_ORCAMENTO  ORDER BY ATDT020_DATA DESC";

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

        public DataTable PesquisaListaOrcamento(string orcamento, ref string erro)
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
                                            TRUNC (ATDC020_VALOR, 2) AS ATDC020_VALOR,
                                            ATSF020_STATUS
                                        FROM ALC020T_ORCAMENTO  
                                        WHERE ATSF020_DESCRICAO LIKE '%'||:ORCAMENTO||'%'
                                        OR ATDT020_VENCIMENTO LIKE '%'||:ORCAMENTO||'%'
                                        ORDER BY ATDT020_DATA DESC";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":ORCAMENTO", orcamento);

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
        public DataTable ObterListaOrcamentoPorStatus(ref string erro)
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
                                            TRUNC (ATDC020_VALOR, 2) AS ATDC020_VALOR,
                                            ATSF020_STATUS
                                        FROM ALC020T_ORCAMENTO WHERE ATSF020_STATUS = 1";

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
                        cmd.Parameters.Add("pDATA", OracleDbType.Date).Value = DateTime.Now;
                        cmd.Parameters.Add("pVENCIMENTO", OracleDbType.Date).Value = orcamento.Vencimento;
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
                                WHERE PKNI020_IDORCAMENTO = :IDORCAMENTO";

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
                        cmd.Parameters.Add("IDORCAMENTO", orcamento.IdOrcamento);

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
                                            ATDC022_VALOR,
                                            ATDC022_QTDE_METRO_QUADRADO,
                                            ATNI022_VALOR_UNITARIO
                                          )
                                          VALUES
                                          (
                                            :IDPRODUTO,
                                            :IDORCAMENTO,
                                            :QUANTIDADE,
                                            :VALOR,
                                            :QTDE_METRO_QUADRADO,
                                            :VALOR_UNITARIO
                                          )";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDPRODUTO", orcamento.IdProduto);
                        cmd.Parameters.Add(":IDORCAMENTO", idOrcamento);
                        cmd.Parameters.Add(":QUANTIDADE", orcamento.Quantidade);
                        cmd.Parameters.Add(":VALOR", orcamento.Valor);
                        cmd.Parameters.Add(":QTDE_METRO_QUADRADO", orcamento.Qdte_metro_quadrado);
                        cmd.Parameters.Add(":VALOR_UNITARIO", orcamento.ValorUnitario);

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

        public bool AtualizarProduto(Orcamento orcamento, ref string erro, int idOrcamento)
        {

            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC022T_PRODUTO_ORCAMENTO
                                            SET
                                            FKNI022_IDPRODUTO             = :IDPRODUTO,
                                            FKNI022_IDORCAMENTO           = :IDORCAMENTO,
                                            ATNI022_QUANTIDADE            = :QUANTIDADE,
                                            ATDC022_VALOR                 = :VALOR,
                                            ATDC022_QTDE_METRO_QUADRADO   = :QTDE_METRO_QUADRADO,
                                            ATNI022_VALOR_UNITARIO        = :VALOR_UNITARIO
                                            WHERE PKNI022_IDPRODUTO_ORCAMENTO = :IDPRODUTO_ORCAMENTO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDPRODUTO", orcamento.IdProduto);
                        cmd.Parameters.Add(":IDORCAMENTO", idOrcamento);
                        cmd.Parameters.Add(":QUANTIDADE", orcamento.Quantidade);
                        cmd.Parameters.Add(":VALOR", orcamento.Valor);
                        cmd.Parameters.Add(":QTDE_METRO_QUADRADO", orcamento.Qdte_metro_quadrado);
                        cmd.Parameters.Add(":VALOR_UNITARIO", orcamento.ValorUnitario);
                        cmd.Parameters.Add(":IDPRODUTO_ORCAMENTO", orcamento.IdProdutoOrcamento);
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

        public DataTable ObterListaProdutoOrcamentoPorID(int idOrcamento, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT
                                        PKNI022_IDPRODUTO_ORCAMENTO,
                                        FKNI022_IDPRODUTO,
                                        TB2.ATSF003_DESCRICAO,
                                        FKNI022_IDORCAMENTO,
                                        ATNI022_QUANTIDADE,
                                        to_char(ATDC022_VALOR,'999G999G999G999D99') as ATDC022_VALOR,
                                        to_char((SELECT SUM(ATDC022_VALOR)
                                          FROM ALC022T_PRODUTO_ORCAMENTO where FKNI022_IDORCAMENTO = :IDORCAMENTO),'999G999G999G999D99') as TOTAL,
                                        ATDC022_QTDE_METRO_QUADRADO,
                                        ATNI022_VALOR_UNITARIO
                                      FROM ALC022T_PRODUTO_ORCAMENTO TB1
                                      INNER JOIN ALC003T_PRODUTO TB2
                                      ON TB1.FKNI022_IDPRODUTO = TB2.PKNI003_IDPRODUTO
                                        where FKNI022_IDORCAMENTO = :IDORCAMENTO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDORCAMENTO", idOrcamento);

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

        public Orcamento ObertProdutoPorID(int IdProduto_Orcamento, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT
                                        PKNI022_IDPRODUTO_ORCAMENTO,
                                        FKNI022_IDPRODUTO,
                                        TB2.ATSF003_DESCRICAO,
                                        FKNI022_IDORCAMENTO,
                                        ATNI022_QUANTIDADE,
                                        to_char(ATDC022_VALOR,'999G999G999G999D99') as ATDC022_VALOR,
                                        to_char((SELECT SUM(ATDC022_VALOR)
                                                    FROM ALC022T_PRODUTO_ORCAMENTO
                                                    where FKNI022_IDORCAMENTO = 2),'999G999G999G999D99') as TOTAL,
                                        to_char(TB2.ATDC003_VALOR_METRO,'999G999G999G999D99') AS ATDC003_VALOR_METRO,
                                        to_char(TB2.ATDC003_VALOR,'999G999G999G999D99') AS ATDC003_VALOR,
                                        ATDC022_QTDE_METRO_QUADRADO,
                                        TB3.FKNI020_IDPESSOA,
                                        ATNI022_VALOR_UNITARIO
                                      FROM ALC022T_PRODUTO_ORCAMENTO TB1
                                      INNER JOIN ALC003T_PRODUTO TB2
                                      ON TB1.FKNI022_IDPRODUTO = TB2.PKNI003_IDPRODUTO
                                      INNER JOIN ALC020T_ORCAMENTO TB3
                                      ON TB1.FKNI022_IDORCAMENTO = TB3.PKNI020_IDORCAMENTO
                                        where PKNI022_IDPRODUTO_ORCAMENTO = :IDPRODTO_ORCAMENTO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDPRODTO_ORCAMENTO", IdProduto_Orcamento);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Orcamento objOrcamento = new Orcamento();

                                objOrcamento.IdProdutoOrcamento = Convert.ToInt32(reader[0]);
                                objOrcamento.IdProduto = (reader[1] == DBNull.Value) ? 0 : Convert.ToInt32(reader[1]);
                                objOrcamento.Descricao = reader[2].ToString();
                                objOrcamento.IdOrcamento = (reader[3] == DBNull.Value) ? 0 : Convert.ToInt32(reader[3]);
                                objOrcamento.Quantidade = (reader["ATNI022_QUANTIDADE"] == DBNull.Value) ? 0 : Convert.ToDecimal(reader["ATNI022_QUANTIDADE"]);
                                objOrcamento.Valor = (reader["ATDC003_VALOR"] == DBNull.Value) ? 0 : Convert.ToDouble(reader["ATDC003_VALOR"]);
                                objOrcamento.ValorMetro = (reader["ATDC003_VALOR_METRO"] == DBNull.Value) ? 0 : Convert.ToDecimal(reader["ATDC003_VALOR_METRO"]);
                                objOrcamento.Qdte_metro_quadrado = (reader["ATDC022_QTDE_METRO_QUADRADO"] == DBNull.Value) ? 0 : Convert.ToDecimal(reader["ATDC022_QTDE_METRO_QUADRADO"]);
                                objOrcamento.IdPessoa = (reader["FKNI020_IDPESSOA"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["FKNI020_IDPESSOA"]);
                                objOrcamento.ValorUnitario = (reader["ATNI022_VALOR_UNITARIO"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["ATNI022_VALOR_UNITARIO"]);

                                return objOrcamento;
                            }
                            return null;
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

        public bool ExcluirProtudoPorID(int idProduto, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"DELETE FROM ALC022T_PRODUTO_ORCAMENTO
                                          WHERE PKNI022_IDPRODUTO_ORCAMENTO = :IDORCAMENTO_PRODUTO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDORCAMENTO_PRODUTO", idProduto);

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

        public bool ExcluirProdutoPorIdOrcamento(int idOrcamento, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"DELETE FROM ALC022T_PRODUTO_ORCAMENTO
                                          WHERE PKNI022_IDPRODUTO_ORCAMENTO = :IDORCAMENTO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDORCAMENTO", idOrcamento);

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
        public LSN020_ORCAMENTO_ds GetData(int idOrcamento, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"  SELECT PKNI020_IDORCAMENTO,
                                        ATSF020_DESCRICAO,
                                        ATDT020_DATA,
                                        ATDT020_VENCIMENTO,
                                        FKNI020_IDPESSOA,
                                        ATDC020_VALOR,
                                        ATSF020_STATUS,
                                        TB1.FKNI020_IDEMPRESA,
                                        TB2.FKNI022_IDPRODUTO,
                                        TB8.ATSF003_DESCRICAO,
                                        TB2.ATDC022_VALOR,
                                        TB2.ATNI022_QUANTIDADE,
                                        TB2.ATDC022_QTDE_METRO_QUADRADO,
                                        TB3.ATNI002_CNPJ,
                                        TB3.ATSF002_NOMEFANTASIA,
                                        TB3.ATSF002_RAZAOSOCIAL,
                                        TB3.ATSF002_EMAIL,
                                        TB4.ATSF008_LOGRADOURO,
                                        TB4.ATNI008_NUMERO,
                                        TB4.ATSF008_BAIRRO,
                                        TB4.ATSF008_CEP,
                                        TB6.ATSF012_NOME AS NOME_CIDADE,
                                        TB7.ATSF013_SIGLA,
                                        TB5.ATNI009_DDD,
                                        TB5.ATSF009_NUMERO AS TELEFONE
                                    FROM ALC020T_ORCAMENTO TB1
                                    INNER JOIN ALC022T_PRODUTO_ORCAMENTO TB2
                                    ON TB1.PKNI020_IDORCAMENTO = TB2.FKNI022_IDORCAMENTO
                                    INNER JOIN ALC002T_EMPRESA TB3
                                    ON TB1.FKNI020_IDEMPRESA = TB3.PKNI002_IDEMPRESA
                                    INNER JOIN ALC008T_ENDERECO TB4
                                    ON TB3.FKNI002_IDPESSOA = TB4.FKNI008_IDPESSOA
                                    INNER JOIN ALC009T_TELEFONE TB5
                                    ON TB3.FKNI002_IDPESSOA = TB5.FKNI009_IDPESSOA
                                    INNER JOIN ALC012T_MUNICIPIO TB6
                                    ON TB4.FKNI008_IDMUNICIPIO = TB6.PKNI012_IDMUNICIPIO
                                    INNER JOIN ALC013T_ESTADO TB7
                                    ON TB6.FKNI012_IDESTADO = TB7.PKNI013_IDESTADO
                                    INNER JOIN ALC003T_PRODUTO TB8
                                    ON TB2.FKNI022_IDPRODUTO = TB8.PKNI003_IDPRODUTO
                                    WHERE PKNI020_IDORCAMENTO = :IDORCAMENTO
                                    
                                    ";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":IDORCAMENTO", idOrcamento);

                        OracleDataAdapter da = new OracleDataAdapter();
                        da.SelectCommand = cmd;

                        using (LSN020_ORCAMENTO_ds dataSet = new LSN020_ORCAMENTO_ds())
                        {
                            da.Fill(dataSet, "BUSCA_ORCAMENTO");
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

        public int ObertOrcamentosPendentes(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT count(*)
                                     FROM ALC020T_ORCAMENTO 
                                     where ATSF020_STATUS = 0 
                                     AND ATDT020_VENCIMENTO < :HOJE";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":HOJE", DateTime.Now);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            Orcamento objOrcamento = new Orcamento();

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

        public int ObertOrcamentosAVencer(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT count(*)
                                     FROM ALC020T_ORCAMENTO 
                                     where ATSF020_STATUS = 0 
                                     AND ATDT020_VENCIMENTO < :HOJE
                                     OR  ATDT020_VENCIMENTO < :AMANHA";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":HOJE", DateTime.Now);
                        cmd.Parameters.Add(":AMANHA", DateTime.Now.AddDays(1));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            Orcamento objOrcamento = new Orcamento();

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

        public DataTable ObterListaOrcamentoPendentes(ref string erro)
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
                                            TRUNC (ATDC020_VALOR, 2) AS ATDC020_VALOR,
                                            ATSF020_STATUS
                                        FROM ALC020T_ORCAMENTO 
                                        where ATSF020_STATUS = 0 
                                     AND ATDT020_VENCIMENTO < :HOJE
                                            ";

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