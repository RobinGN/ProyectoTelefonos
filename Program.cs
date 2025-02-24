namespace ProyectoTelefonos
{
    internal class Program
    {
        public static char[] Nums = ['0','1','2','3','4','5','6','7','8','9'];
        public static char[] Letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
        'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static char[] Dot = { '.' };
        public static char[] At = { '@' };
        public static char[] SLine = ['-'];
        public static char[] Space = [' '];
        public static char[] LeftP = ['('];
        public static char[] RightP = [')'];
        public static char[] DLine = ['_'];

        public static Dictionary<(string, char), string> AutomatonTelephones = new()
        {
            {("q0", 'n'), "q1"}, {("q0", 'l'), "q11"},
            {("q1", 'n'), "q2"},
            {("q2", 'n'), "q3"},
            {("q3", 'n'), "q4"}, {("q3", ' '), "q21"}, {("q3", '-'), "q16"},
            {("q4", 'n'), "q5"},
            {("q5", 'n'), "q6"},
            {("q6", 'n'), "q7"},
            {("q7", 'n'), "q8"},
            {("q8", 'n'), "q9"},
            {("q9", 'n'), "q10"},
            {("q11", 'n'), "q12"},
            {("q12", 'n'), "q13"},
            {("q13", 'n'), "q14"},
            {("q14", 'r'), "q15"},
            {("q15", ' '), "q21"}, {("q15", '-'), "q16"},
            {("q16", 'n'), "q17"},
            {("q17", 'n'), "q18"},
            {("q18", 'n'), "q19"},
            {("q19", '-'), "q20"},
            {("q20", 'n'), "q7"},
            {("q21", 'n'), "q22"},
            {("q22", 'n'), "q23"},
            {("q23", 'n'), "q24"},
            {("q24", ' '), "q20"}
        };

        public static Dictionary<(string, char), string> AutomatonMails = new()
        {
            {("q0", 'n'), "q1"}, {("q0", 'v'), "q1"},
            {("q1", 'n'), "q1"}, {("q1", 'v'), "q1"}, {("q1", '.'), "q2"}, {("q1", '@'), "q3"},
            {("q2", 'n'), "q1"}, {("q2", 'v'), "q1"},
            {("q3", 'v'), "q4"}, 
            {("q4", 'v'), "q4"}, {("q4", '.'), "q5"},
            {("q5", 'v'), "q7"},
            {("q6", 'v'), "q8"},
            {("q7", 'v'), "q7"}, {("q7", '.'), "q6"},
            {("q8", 'v'), "q8"}
        };

        public static string[] TelsFinalState = { "q10" };
        public static string[] MailsFinalState = { };

        static void Main(string[] args)
        {
            
        }
    }
}
