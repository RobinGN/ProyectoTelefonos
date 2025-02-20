namespace ProyectoTelefonos
{
    internal class Program
    {
        public static char[] Nums = ['0','1','2','3','4','5','6','7','8','9'];
        public static char[] Guion = ['-'];
        public static char[] Espacio = [' '];
        public static char[] ParIzq = ['('];
        public static char[] ParDer = [')'];
        public static Dictionary<(string, char), string> transitions = new()
        {
            {("q0", 'n'), "q1"}, {("q0", 'r'), "q11"},
            {("q1", 'n'), "q2"},
            {("q3") }
        };
        static void Main(string[] args)
        {
            
        }
    }
}
