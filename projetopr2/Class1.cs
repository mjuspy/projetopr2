using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetopr2
{
    public static class SessaoUsuario
    {
        public static int Id { get; set; }
        public static string Nome { get; set; }
        public static string Email { get; set; }
        public static bool Logado { get; set; } = false;
    }
}
