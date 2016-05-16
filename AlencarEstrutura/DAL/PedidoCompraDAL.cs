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
    public class PedidoCompraDAL
    {
        public int CriarPedidoCompra(PedidoCompra pedidoCompra, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("INSERIRPEDIDO", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@pIDPEDIDOCOMPRA", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add("@pIDUSUARIO", OracleDbType.Int32).Value = pedidoCompra.IdUsuario;
                        cmd.Parameters.Add("@pDATAPEDIDO", OracleDbType.Date).Value = pedidoCompra.DataPedido;
                        cmd.Parameters.Add("@pSTATUS", OracleDbType.Int32).Value = pedidoCompra.Status;
                        int reader = cmd.ExecuteNonQuery();
                        int IdPessoa = Convert.ToInt32(cmd.Parameters["@pIDPEDIDOCOMPRA"].Value.ToString());
                        return IdPessoa;
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return 0;
            }
        }

        public PedidoCompra ObterPedidoPorID(int idPedido, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI006_IDPEDIDOCOMPRA,
                                      FKNI006_IDUSUARIO,
                                      ATDT006_DATAPEDIDO,
                                      ATNI006_STATUS
                                    FROM ALC006T_PEDIDOCOMPRA WHERE PKNI006_IDPEDIDOCOMPRA = :IDPEDIDO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":IDPEDIDO", idPedido);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            PedidoCompra objPedido = new PedidoCompra();
                            if (reader.Read())
                            {
                                objPedido.IdPedidoCompra = Convert.ToInt32(reader["PKNI006_IDPEDIDOCOMPRA"]);
                                objPedido.IdUsuario = Convert.ToInt32(reader["FKNI006_IDUSUARIO"]);
                                objPedido.DataPedido = Convert.ToDateTime(reader["ATDT006_DATAPEDIDO"]);
                                objPedido.Status = Convert.ToInt32(reader["ATNI006_STATUS"]);
                            }
                            return objPedido;
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

        public bool ExcluirPedidoCompra(int idPedidoCompra, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"DELETE
                                    FROM ALC006T_PEDIDOCOMPRA
                                    WHERE PKNI006_IDPEDIDOCOMPRA = :IDPEDIDOCOMPRA";
                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":IDPEDIDOCOMPRA", idPedidoCompra);

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

        public bool AdicionarProdutos(PedidoCompra pedidoCompra, ref string erro, int IdPedidoCompra)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"INSERT
                                        INTO ALC018T_PRODUTO_PEDIDO
                                          (
                                            ATSD018_DATAPEDIDO,
                                            ATSF018_OBSERVACAO,
                                            ATDC018_QUANTIDADE,
                                            ATDC018_VALORPREVISTO,
                                            FKNI018_IDPRODUTO,
                                            FKNI018_IDPEDIDOCOMPRA,
                                            FKNI018_IDFORNECEDOR
                                          )
                                          VALUES
                                          (
                                            :DATAPEDIDO,
                                            :OBSERVACAO,
                                            :QUANTIDADE,
                                            :VALORPREVISTO,
                                            :IDPRODUTO,
                                            :IDPEDIDOCOMPRA,
                                            :IDFORNECEDOR
                                          )";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add("DATAPEDIDO", pedidoCompra.DataPedido);
                        cmd.Parameters.Add("OBSERVACAO", pedidoCompra.Observacao);
                        cmd.Parameters.Add("QUANTIDADE", pedidoCompra.Quantidade);
                        cmd.Parameters.Add("VALORPREVISTO", pedidoCompra.ValorPrevisto);
                        cmd.Parameters.Add("IDPRODUTO", pedidoCompra.IdProduto);
                        cmd.Parameters.Add("IDPEDIDOCOMPRA", IdPedidoCompra);
                        cmd.Parameters.Add("IDPEDIDOCOMPRA", pedidoCompra.IdFornecedor);

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

        public DataTable CarregaListaProdutos(int idPedidoCompra, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI018_IDPRODUTO_PEDIDO,
                                            ATSD018_DATAPEDIDO,
                                            ATSF018_OBSERVACAO,
                                            ATDC018_QUANTIDADE,
                                            ATDC018_VALORPREVISTO,
                                            FKNI018_IDPRODUTO,
                                            TB2.ATSF003_DESCRICAO,
                                            FKNI018_IDPEDIDOCOMPRA
                                        FROM ALC018T_PRODUTO_PEDIDO TB1
                                        INNER JOIN ALC003T_PRODUTO TB2
                                        ON TB1.FKNI018_IDPRODUTO = TB2.PKNI003_IDPRODUTO
                                        WHERE FKNI018_IDPEDIDOCOMPRA = :IDPEDIDOCOMPRA";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":IDPEDIDOCOMPRA", idPedidoCompra);

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

        public DataTable CarregaListaPedidos(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI006_IDPEDIDOCOMPRA,
                                      FKNI006_IDUSUARIO,
                                      ATDT006_DATAPEDIDO,
                                      ATNI006_STATUS
                                    FROM ALC006T_PEDIDOCOMPRA
                                    ORDER BY PKNI006_IDPEDIDOCOMPRA DESC";

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

        public Produto_Pedido CarregaProdutoporId(int idPedidoProduto, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI018_IDPRODUTO_PEDIDO,
                                            ATSD018_DATAPEDIDO,
                                            ATSF018_OBSERVACAO,
                                            ATDC018_QUANTIDADE,
                                            ATDC018_VALORPREVISTO,
                                            FKNI018_IDPRODUTO,
                                            TB2.ATSF003_DESCRICAO,
                                            FKNI018_IDPEDIDOCOMPRA,
                                            FKNI018_IDFORNECEDOR
                                        FROM ALC018T_PRODUTO_PEDIDO TB1
                                        INNER JOIN ALC003T_PRODUTO TB2
                                        ON TB1.FKNI018_IDPRODUTO = TB2.PKNI003_IDPRODUTO
                                        WHERE PKNI018_IDPRODUTO_PEDIDO = :IDPRODUTO_PEDIDO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":IDPRODUTO_PEDIDO", idPedidoProduto);

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Produto_Pedido objProduto = new Produto_Pedido();

                                objProduto.IdProdutoPedido = Convert.ToInt32(reader["PKNI018_IDPRODUTO_PEDIDO"]);
                                objProduto.IdProduto = Convert.ToInt32(reader["FKNI018_IDPRODUTO"]);
                                objProduto.IdFornecedor = (reader["FKNI018_IDFORNECEDOR"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["FKNI018_IDFORNECEDOR"]);
                                objProduto.Observacao = reader["ATSF018_OBSERVACAO"].ToString();
                                objProduto.ValorPrevisto = (reader["ATDC018_VALORPREVISTO"] == DBNull.Value) ? 0 : Convert.ToDouble(reader["ATDC018_VALORPREVISTO"]);
                                objProduto.Quantidade = (reader["ATDC018_QUANTIDADE"] == DBNull.Value) ? 0 : Convert.ToDecimal(reader["ATDC018_QUANTIDADE"]);

                                return objProduto;
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

        public bool AtualizaProduto(PedidoCompra produto, ref string erro, int IdPedidoCompra)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC018T_PRODUTO_PEDIDO SET
                                        ATSD018_DATAPEDIDO         = :DATAPEDIDO,
                                        ATSF018_OBSERVACAO         = :OBSERVACAO,
                                        ATDC018_QUANTIDADE         = :QUANTIDADE,
                                        ATDC018_VALORPREVISTO      = :VALORPREVISTO,
                                        FKNI018_IDPRODUTO          = :IDPRODUTO,
                                        FKNI018_IDPEDIDOCOMPRA     = :IDPEDIDOCOMPRA,
                                        FKNI018_IDFORNECEDOR       = :IDFORNECEDOR
                                        WHERE PKNI018_IDPRODUTO_PEDIDO = :IDPRODUTO_PEDIDO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":DATAPEDIDO", produto.DataPedido);
                        cmd.Parameters.Add(":OBSERVACAO", produto.Observacao);
                        cmd.Parameters.Add(":QUANTIDADE", produto.Quantidade);
                        cmd.Parameters.Add(":VALORPREVISTO", produto.ValorPrevisto);
                        cmd.Parameters.Add(":FKNI018_IDPRODUTO", produto.IdProduto);
                        cmd.Parameters.Add(":FKNI018_IDPEDIDOCOMPRA", IdPedidoCompra);
                        cmd.Parameters.Add(":FKNI018_IDFORNECEDOR", produto.IdFornecedor);
                        cmd.Parameters.Add("IDPRODUTO_PEDIDO", produto.IdProdutoPedido);

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

        public bool ExcluirProdutoPorID(int idProdutoPedido, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"DELETE
                                    FROM ALC018T_PRODUTO_PEDIDO
                                    WHERE PKNI018_IDPRODUTO_PEDIDO = :IDPRODUTO_PEDIDO";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add("IDPRODUTO_PEDIDO", idProdutoPedido);

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

        public bool ExcluirProdutoPorIdPedido(int idPedido, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"DELETE
                                        FROM ALC006T_PEDIDOCOMPRA
                                        WHERE PKNI006_IDPEDIDOCOMPRA = :IDPEDIDOCOMPRA";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":IDPEDIDOCOMPRA", idPedido);

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

        public DataSet1 GetData(int idPedido, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI018_IDPRODUTO_PEDIDO,
                                  ATSD018_DATAPEDIDO,
                                  ATSF018_OBSERVACAO,
                                  ATDC018_QUANTIDADE,
                                  ATDC018_VALORPREVISTO,
                                  FKNI018_IDPRODUTO,
                                  FKNI018_IDPEDIDOCOMPRA,
                                  FKNI018_IDFORNECEDOR,
                                  ATSF003_DESCRICAO,
                                  ATSF007_NOMEFANTASIA,
                                  TB2.*
                                FROM BUSCA_PEDIDO_COMPRA TB1
                                INNER JOIN ALC002T_EMPRESA TB2
                                ON TB1.PKNI003_IDEMPRESA = TB2.PKNI002_IDEMPRESA
                                WHERE FKNI018_IDPEDIDOCOMPRA = :IDPEDIDOCOMPRA
                                    ";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":IDPEDIDOCOMPRA", idPedido);

                        OracleDataAdapter da = new OracleDataAdapter();
                        da.SelectCommand = cmd;

                        using (DataSet1 dataSet = new DataSet1())
                        {
                            da.Fill(dataSet, "BUSCA_PEDIDO_COMPRA");
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
    }
}