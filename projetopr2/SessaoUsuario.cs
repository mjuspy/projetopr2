using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetopr2
{
    public static class SessaoUsuari
    {
        public static int ID_usuario { get; private set; }
        public static string Nome { get; private set; }

        // Uma propriedade simples para verificar rapidamente se alguém está logado.
        public static bool IsLoggedIn => ID_usuario > 0;

        // Método para "iniciar a sessão".
        public static void Login(int id, string nome)
        {
            ID_usuario = id;
            Nome = nome;
        }

        // Método para "encerrar a sessão".
        public static void Logout()
        {
            ID_usuario = 0;
            Nome = string.Empty;
        }
    }
}