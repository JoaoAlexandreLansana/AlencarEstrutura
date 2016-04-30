using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DAL
{
    public class PessoaDAL
    {
        public bool InserirPessoa(Pessoa pessoa, Endereco endereco, Telefone telefone, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("INSERIRPESSOA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("pIDPESSOA", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add("pEMAIL", OracleDbType.Char).Value = pessoa.Email;
                        cmd.Parameters.Add("pTIPOPESSOA", OracleDbType.Char).Value = pessoa.TipoPessoa;
                        cmd.Parameters.Add("pSENHA", OracleDbType.Char).Value = pessoa.Senha;
                        cmd.Parameters.Add("pNOME", OracleDbType.Char).Value = pessoa.NomePessoa;
                        cmd.Parameters.Add("pCNPJ", OracleDbType.Int64).Value = pessoa.CNPJ;
                        cmd.Parameters.Add("pNOMEFANTASIA", OracleDbType.Char).Value = pessoa.NomeFantasia;
                        cmd.Parameters.Add("pRAZAO", OracleDbType.Char).Value = pessoa.RazaoSocial;
                        cmd.Parameters.Add("pSTATUS", OracleDbType.Int32).Value = pessoa.Status;
                        cmd.Parameters.Add("pTELEFONE", OracleDbType.Char).Value = telefone.Numero;
                        cmd.Parameters.Add("pDDD", OracleDbType.Int32).Value = telefone.DDD;
                        cmd.Parameters.Add("pTIPOTELEFONE", OracleDbType.Int32).Value = telefone.IdTipoTelefone;
                        cmd.Parameters.Add("pLOGRADOURO", OracleDbType.Char).Value = endereco.Logradouro;
                        cmd.Parameters.Add("pBAIRRO", OracleDbType.Char).Value = endereco.Bairro;
                        cmd.Parameters.Add("pNUMERO", OracleDbType.Char).Value = endereco.Numero;
                        cmd.Parameters.Add("pCEP", OracleDbType.Char).Value = endereco.CEP;
                        cmd.Parameters.Add("pCOMPLEMENTO", OracleDbType.Char).Value = endereco.Complemento;
                        cmd.Parameters.Add("pIDMUNICIPIO", OracleDbType.Int32).Value = endereco.IdMunicipio;
                        cmd.Parameters.Add("pIDESTADO", OracleDbType.Int32).Value = endereco.IdEstado;
                        

                        int result = cmd.ExecuteNonQuery();

                        int IdPessoa = Convert.ToInt32(cmd.Parameters["pIDPESSOA"].Value.ToString());
                        sucesso = Convert.ToBoolean(result);
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

        public bool AtualizarPessoa(Pessoa pessoa, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"UPDATE ALC014T_PESSOA
                                    SET
                                    ATSF014_EMAIL      = :EMAIL,
                                    ATSF014_TIPOPESSOA = :TIPOPESSOA,
                                    ATSF014_SENHA      = :SENHA,
                                    ATSF014_NOME       = :NOME ";
                    
                    query += "WHERE PKNI014_IDPESSOA = :IDPESSOA";

                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(":EMAIL", pessoa.Email);
                        cmd.Parameters.Add(":TIPOPESSOA", pessoa.TipoPessoa);
                        cmd.Parameters.Add(":SENHA", pessoa.Senha);
                        cmd.Parameters.Add(":NOME", pessoa.NomePessoa);
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