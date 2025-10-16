using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetopr2
{
    public static class SessaoUsuario1
    {
        public static Usuario UsuarioLogado { get; private set; }

        // Método para iniciar a sessão (fazer o login)
        public static void Login(Usuario usuario)
        {
            UsuarioLogado = usuario;
        }

        // Método para encerrar a sessão (fazer o logout)
        public static void Logout()
        {
            UsuarioLogado = null;
        }

        // Uma propriedade para verificar facilmente se há alguém logado
        public static bool IsLoggedIn => UsuarioLogado != null;
    }
}