using System;

namespace projetopr2
{
    public static class SessaoUsuari
    {
        public static int ID_usuario { get; private set; }
        public static string Nome { get; private set; }

        // Retorna true se houver um usuário logado
        public static bool IsLoggedIn => ID_usuario > 0;

        // Inicia a sessão com os dados do usuário logado
        public static void Login(int id, string nome)
        {
            ID_usuario = id;
            Nome = nome;
        }

        // Encerra a sessão
        public static void Logout()
        {
            ID_usuario = 0;
            Nome = string.Empty;
        }
    }
}
