﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using WebApplication2.DTO;

namespace WebApplication2.DAL
{
    public class UsuarioDAL
    {
        public static Usuario CarregaUsuario(string login, string senha)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
            {
                string query = @"SELECT PKNI001_IDUSUARIO,
                                      ATSF001_NOME,
                                      ATSF001_LOGIN,
                                      ATSF001_EMAIL,
                                      ATSF001_SENHA
                                    FROM ALC001T_USUARIO
                                    WHERE ATSF001_LOGIN = :LOGIN
                                    AND ATSF001_SENHA - :SENHA";

                conn.Open();

                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(":LOGIN", login);
                    cmd.Parameters.Add(":SENHA", senha);

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Usuario dbUsuario = new Usuario();

                            dbUsuario.Login = reader[1].ToString();
                            dbUsuario.Senha = reader[2].ToString();
                            dbUsuario.Email = reader[3].ToString();
                            dbUsuario.IdEmpresa = Convert.ToInt16(reader[4]);
                            dbUsuario.Status = Convert.ToInt16(reader[5]);
                            dbUsuario.Nome = reader[6].ToString();
                            dbUsuario.IdPerfil = Convert.ToInt16(reader[7]);

                            return dbUsuario;
                        }
                        return null;
                    }
                }
            }
        }

        public Usuario ValidaUsuario(string login, string senha)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
            {
                string query = @"SELECT PKNI001_IDUSUARIO,
                                          ATSF001_NOME,
                                          ATSF001_LOGIN,
                                          ATSF001_EMAIL,
                                          ATSF001_SENHA
                                        FROM ALC001T_USUARIO
                                    WHERE ATSF001_LOGIN = :LOGIN
                                    AND ATSF001_SENHA = :SENHA";

                conn.Open();

                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(":LOGIN", login);
                    cmd.Parameters.Add(":SENHA", senha);

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Usuario objUsuario = new Usuario();

                            objUsuario.idUsuario = Convert.ToInt32(reader["PKNI001_IDUSUARIO"]);
                            objUsuario.Login = reader["ATSF001_LOGIN"].ToString();
                            return objUsuario;
                        }
                        return null;
                    }
                }
            }
        }

        public bool InserirUsuario(Usuario usuario, ref string erro)
        {
            bool sucesso = false;
            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
                {
                    string query = @"INSERT
                                        INTO ALC001T_USUARIO
                                          (
                                            ATSF001_NOME,
                                            ATSF001_LOGIN,
                                            ATSF001_EMAIL,
                                            ATSF001_SENHA
                                          )
                                          VALUES
                                          (
                                            :NOME,
                                            :LOGIN,
                                            :EMAIL,
                                            :SENHA
                                          )";
                    conn.Open();

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(":LOGIN", usuario.Login);
                        cmd.Parameters.Add(":SENHA", usuario.Senha);
                        cmd.Parameters.Add("EMAIL", usuario.Email);
                        cmd.Parameters.Add(":NOME", usuario.Nome);
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