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
                                    FROM ALC006T_PEDIDOCOMPRA";

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
                                objProduto.ValorPrevisto = (reader["ATDC018_VALORPREVISTO"] == DBNull.Value)? 0 : Convert.ToDecimal(reader["ATDC018_VALORPREVISTO"]);
                                objProduto.Quantidade = (reader["ATDC018_QUANTIDADE"] == DBNull.Value)? 0 : Convert.ToDecimal(reader["ATDC018_QUANTIDADE"]);

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

        //public PedidoCompra CarregaPedidoCompra(int idPedido, ref string erro)
        //{
        //    try
        //    {
        //        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
        //        {
        //            string query = @"SELECT PKNI018_IDPRODUTO_PEDIDO,
        //                                    ATSD018_DATAPEDIDO,
        //                                    ATSF018_OBSERVACAO,
        //                                    ATDC018_QUANTIDADE,
        //                                    ATDC018_VALORPREVISTO,
        //                                    FKNI018_IDPRODUTO,
        //                                    TB2.ATSF003_DESCRICAO,
        //                                    FKNI018_IDPEDIDOCOMPRA
        //                                FROM ALC018T_PRODUTO_PEDIDO TB1
        //                                INNER JOIN ALC003T_PRODUTO TB2
        //                                ON TB1.FKNI018_IDPRODUTO = TB2.PKNI003_IDPRODUTO
        //                                WHERE FKNI018_IDPEDIDOCOMPRA = :IDPEDIDOCOMPRA";

        //            conn.Open();

        //            using (OracleCommand cmd = conn.CreateCommand())
        //            {
        //                cmd.CommandText = query;
        //                cmd.Parameters.Add(":IDPEDIDOCOMPRA", idPedidoCompra);

        //                DataTable dt = new DataTable();
        //                OracleDataAdapter da = new OracleDataAdapter();
        //                da.SelectCommand = cmd;
        //                da.Fill(dt);
        //                return dt;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        erro = ex.Message;
        //        return null;
        //    }
        //}
    }
}