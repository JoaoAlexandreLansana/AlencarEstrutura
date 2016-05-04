using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;

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

        public bool InserirPessoaFisica(Pessoa pessoa, Endereco endereco, Telefone telefone, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("INSERIRPESSOAFISICA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("pIDPESSOA", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add("pEMAIL", OracleDbType.Char).Value = pessoa.Email;
                        cmd.Parameters.Add("pTIPOPESSOA", OracleDbType.Char).Value = pessoa.TipoPessoa;
                        cmd.Parameters.Add("pSENHA", OracleDbType.Char).Value = pessoa.Senha;
                        cmd.Parameters.Add("pNOME", OracleDbType.Char).Value = pessoa.NomePessoa;
                        cmd.Parameters.Add("pCPF", OracleDbType.Int64).Value = pessoa.CNPJ;
                        cmd.Parameters.Add("pRG", OracleDbType.Char).Value = pessoa.RG;
                        cmd.Parameters.Add("pIDESTADOCIVIL", OracleDbType.Int32).Value = pessoa.Status;
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

        public bool InserirPessoaJuridica(Pessoa pessoa, Endereco endereco, Telefone telefone, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("INSERIRPESSOAJURIDICA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("pIDPESSOA", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add("pEMAIL", OracleDbType.Char).Value = pessoa.Email;
                        cmd.Parameters.Add("pTIPOPESSOA", OracleDbType.Char).Value = pessoa.TipoPessoa;
                        cmd.Parameters.Add("pSENHA", OracleDbType.Char).Value = pessoa.Senha;
                        cmd.Parameters.Add("pNOME", OracleDbType.Char).Value = pessoa.NomePessoa;
                        cmd.Parameters.Add("pCNPJ", OracleDbType.Int64).Value = pessoa.CNPJ;
                        cmd.Parameters.Add("pRAZAOSOCIAL", OracleDbType.Char).Value = pessoa.RazaoSocial;
                        cmd.Parameters.Add("pINSCESTADUAL", OracleDbType.Char).Value = pessoa.InscricaoEstadual;
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

        public DataTable ObterListaPessoa(ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT
                                      TB1.PKNI014_IDPESSOA as CODIGO,
                                      TB1.ATSF014_NOME AS NOME,
                                      TB1.ATSF014_TIPOPESSOA AS TIPOPESSOA
                                    FROM ALC014T_PESSOA TB1
                                    INNER JOIN ALC015T_PESSOAFISICA TB2
                                    ON TB2.FKNI015_IDPESSOA = TB1.PKNI014_IDPESSOA"
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

        public Pessoa ObterPessoaFisicaPorID(int idPessoa, ref string erro)
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
                                      TB2.ATNI015_CPF,
                                      TB2.ATNI015_IDESTADOCIVIL,
                                      TB2.ATSF015_RG,
                                      TB2.PKNI015_IDPESSOAFISICA,
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
                                    INNER JOIN ALC015T_PESSOAFISICA TB2
                                    ON TB1.PKNI014_IDPESSOA = TB2.FKNI015_IDPESSOA
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
                                    WHERE TB1.PKNI014_IDPESSOA = :IDPESSOA"
                                    ;

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
                                objPessoa.CPF = Convert.ToInt64(reader["ATNI015_CPF"]);
                                objPessoa.IdEstadoCivil = Convert.ToInt32(reader["ATNI015_IDESTADOCIVIL"]);
                                objPessoa.RG = reader["ATSF015_RG"].ToString();
                                objPessoa.IdPessoaFisica = Convert.ToInt32(reader["PKNI015_IDPESSOAFISICA"]);
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

        public Pessoa ObterPessoaJuridicaPorID(int idPessoa, ref string erro)
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
                                      TB2.ATNI016_CNPJ,
                                      TB2.ATSF016_INSCESTADUAL,
                                      TB2.PKNI016_IDPESSOAJURIDICA,
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
                                    INNER JOIN ALC016T_PESSOAJURIDICA TB2
                                    ON TB1.PKNI014_IDPESSOA = TB2.FKNI015_IDPESSOA
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
                                    WHERE TB1.PKNI014_IDPESSOA = :IDPESSOA"
                                    ;

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
                                objPessoa.CNPJ = Convert.ToInt64(reader["ATNI016_CNPJ"]);
                                objPessoa.InscricaoEstadual = reader["ATSF016_INSCESTADUAL"].ToString();
                                objPessoa.IdPessoaJuridica = Convert.ToInt32(reader["PKNI016_IDPESSOAJURIDICA"]);
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

        public bool ExcluirPessoa(Pessoa pessoa, Endereco endereco, Telefone telefone, ref string erro)
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
        public Pessoa ObterPessoaID(int idPessoa, ref string erro)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"SELECT PKNI014_IDPESSOA,
                                      ATSF014_EMAIL,
                                      ATSF014_TIPOPESSOA,
                                      ATSF014_SENHA,
                                      ATSF014_NOME
                                    FROM ALC014T_PESSOA 
                                    WHERE PKNI014_IDPESSOA = :IDPESSOA"
                                    ;

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

        public bool ExcluirPessoa(int idPessoa, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("EXCLUIRPESSOA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.Add("pIDPESSOA", OracleDbType.Int32).Value = idPessoa;

                        int result = cmd.ExecuteNonQuery();

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
    }
}