using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe seu Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe seu Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe sua Senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Informe sua Data de Nascimento")]
        public String Data_Nascimento { get; set; }

        public bool ValidarLogin()
        {
            string sql = $"SELECT ID, NOME, DATA_NASCIMENTO FROM USUARIO WHERE EMAIL='{Email}' AND SENHA='{Senha}'";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    Id = int.Parse(dt.Rows[0]["ID"].ToString());
                    Nome = dt.Rows[0]["NOME"].ToString();
                    Data_Nascimento = dt.Rows[0]["DATA_NASCIMENTO"].ToString();
                    return true;
                }
            }

            return false;
        }
        public void RegistrarUsuario()
        {
            String dataNascimento = DateTime.Parse(Data_Nascimento).ToString("yyyy/mm/dd");
            string sql = $"INSERT INTO USUARIO (NOME, EMAIL, SENHA, DATA_NASCIMENTO) VALUES('{Nome}','{Email}','{Senha}','{dataNascimento}')";

            DAL objDAL = new DAL();

            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
