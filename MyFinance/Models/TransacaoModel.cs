using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a Data")]
        public string Data { get; set; }
        public string Tipo { get; set; }
        public double Valor { get; set; }
        [Required(ErrorMessage = "Informe a Descrição")]
        public string Descricao { get; set; }
        public int Conta_Id { get; set; }
        public string NomeConta { get; set; }
        public int Plano_Contas_Id { get; set; }
        public string DescricaoPlanoConta { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public TransacaoModel()
        {

        }
        // Recebe o contexto para acesso as variaveis de sessão
        public TransacaoModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public List<TransacaoModel> ListaTransacao()
        {
            List<TransacaoModel> lista = new List<TransacaoModel>();
            TransacaoModel item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "select t.Id, t.Data, t.Tipo, t.Valor, t.Descricao as historico , t.Conta_Id, c.Nome as conta, " +
                        " t.Plano_Contas_Id, p.Descricao as plano_conta from transacao as t inner join conta c " +
                        " on t.Conta_Id = c.Id inner join Plano_Contas as p " +
                        " on t.Plano_Contas_Id = p.Id " +
                        $" where t.usuario_Id={id_usuario_logado} order by t.Data desc limit 10";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)

            {
                item = new TransacaoModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Data = DateTime.Parse(dt.Rows[i]["Data"].ToString()).ToString("dd/mm/yyyy");
                item.Descricao = dt.Rows[i]["historico"].ToString();
                item.Valor = double.Parse(dt.Rows[i]["Valor"].ToString());
                item.Conta_Id = int.Parse(dt.Rows[i]["Conta_Id"].ToString());
                item.NomeConta = dt.Rows[i]["conta"].ToString();
                item.Plano_Contas_Id = int.Parse(dt.Rows[i]["Plano_Contas_Id"].ToString());
                item.DescricaoPlanoConta = dt.Rows[i]["plano_conta"].ToString();
                item.Tipo = dt.Rows[i]["TIPO"].ToString();
                lista.Add(item);
            }

            return lista;
        }

        public void Insert()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "";
            if (Id == 0)
            {
                sql = $"INSERT INTO TRANSACAO (DATA, TIPO, DESCRICAO, VALOR, CONTA_ID, PLANO_CONTAS_ID, USUARIO_ID) " +
                    $"VALUES ('{DateTime.Parse(Data).ToString("yyyy/mm/dd")}','{Tipo}','{Descricao}','{Valor}','{Conta_Id}','{Plano_Contas_Id}','{id_usuario_logado}')";
            }
            else
            {
                sql = $"UPDATE TRANSACAO SET DATA='{DateTime.Parse(Data).ToString("yyyy/mm/dd")}', " +
                    $"DESCRICAO='{Descricao}', " +
                    $"TIPO='{Tipo}', " +
                    $"VALOR='{Valor}', " +
                    $"CONTA_ID='{Conta_Id}', " +
                    $"PLANO_CONTA_ID='{Plano_Contas_Id}' " +
                    $"WHERE USUARIO_ID='{id_usuario_logado}' AND ID='{Id}'";
            }

            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

        public TransacaoModel CarregarRegistro(int? id)
        {
            TransacaoModel item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "select t.Id, t.Data, t.Tipo, t.Valor, t.Descricao as historico , t.Conta_Id, c.Nome as conta, " +
                        " t.Plano_Contas_Id, p.Descricao as plano_conta from transacao as t inner join conta c " +
                        " on t.Conta_Id = c.Id inner join Plano_Contas as p " +
                        " on t.Plano_Contas_Id = p.Id " +
                        $" where t.Usuario_Id={id_usuario_logado} and t.Id='{id}'";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            item = new TransacaoModel();
            item.Id = int.Parse(dt.Rows[0]["ID"].ToString());
            item.Data = DateTime.Parse(dt.Rows[0]["Data"].ToString()).ToString("dd/mm/yyyy");
            item.Descricao = dt.Rows[0]["historico"].ToString();
            item.Valor = double.Parse(dt.Rows[0]["Valor"].ToString());
            item.Conta_Id = int.Parse(dt.Rows[0]["Conta_Id"].ToString());
            item.NomeConta = dt.Rows[0]["conta"].ToString();
            item.Plano_Contas_Id = int.Parse(dt.Rows[0]["Plano_Contas_Id"].ToString());
            item.DescricaoPlanoConta = dt.Rows[0]["plano_conta"].ToString();
            item.Tipo = dt.Rows[0]["TIPO"].ToString();

            return item;
        }
    }
}
