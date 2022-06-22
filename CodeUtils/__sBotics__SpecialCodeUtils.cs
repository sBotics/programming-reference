// Comments:
// [External Class] sBotics.__sBotics__Programming
//     ↪ Manages user programs

using System.Collections.Generic;

namespace sBotics
{
    namespace CodeUtils
    {
        public static class __sBotics__SpecialCodeUtils
        {
            public static Dictionary<Colors, string> ColorsPTBR = new Dictionary<Colors, string>
            {
                { Colors.Black, "Preto" },
                { Colors.White, "Branco" },
                { Colors.Green, "Verde" },
                { Colors.Red, "Vermelho" },
                { Colors.Blue, "Azul" },
                { Colors.Yellow, "Amarelo" },
                { Colors.Magenta, "Magenta" },
                { Colors.Cyan, "Ciano" },
            };

            public static Dictionary<Notes, double> NoteHz = new Dictionary<Notes, double>
            {
                { Notes.C, 32 },
                { Notes.D, 36 },
                { Notes.E, 41 },
                { Notes.F, 43 },
                { Notes.G, 49 },
                { Notes.A, 55 },
                { Notes.B, 61 }
            };

            public static string Locale
            {
                get => __sBotics__Programming.instance != null?
                    (__sBotics__Programming.Program != null?
                    __sBotics__Programming.Program.Locale.ToString() : "en_US") : "en_US";
            }
        }
    }
}

