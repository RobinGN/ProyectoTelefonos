using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

internal class Program
{
    public static char[] guion = { '-' };
    public static char[] espacio = { ' ' };
    public static char[] numeros = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    public static char[] parIzq = { '(' };
    public static char[] parDer = { ')' };
    public static char[] letras = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
        'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    public static char[] caracteres = { '-', '_' };
    public static char[] aroba = { '@' };
    public static char[] punto = { '.' };
    public static Dictionary<(string, char), string> transitionsA = new()
    {
        {("q0", '('), "q1"}, {("q0", '0'), "q7"},
        {("q1", '0'), "q2"},
        {("q2", '0'), "q3"},
        {("q3", '0'), "q4"},
        {("q4", ')'), "q5"},
        {("q5", '-'), "q6"}, {("q5", ' '), "q10"},
        {("q6", '0'), "q22"},
        {("q7", '0'), "q8"},
        {("q8", '0'), "q9"},
        {("q9", '0'), "q19"}, {("q9", '-'), "q6"}, {("q9", ' '), "q10"},
        {("q10", '0'), "q11"},
        {("q11", '0'), "q12"},
        {("q12", '0'), "q13"},
        {("q13", ' '), "q14"},
        {("q14", '0'), "q15"},
        {("q15", '0'), "q16"},
        {("q16", '0'), "q17"},
        {("q17", '0'), "q18"},
        {("q19", '0'), "q20"},
        {("q20", '0'), "q21"},
        {("q21", '0'), "q15"},
        {("q22", '0'), "q23"},
        {("q23", '0'), "q24"},
        {("q24", '-'), "q14"}
    };

    public static Dictionary<(string, char), string> transitionsB = new()
    {
        {("q0", 'a'), "q4"}, {("q0", '0'), "q4"},
        {("q1", 'a'), "q2"},
        {("q2", 'a'), "q2"}, {("q2", '.'), "q3"},
        {("q4", 'a'), "q4"}, {("q4", '0'), "q4"}, {("q4", '@'), "q1"}, {("q4", '.'), "q5"}, {("q5", 'b'), "q4"},
        {("q5", 'a'), "q4"}, {("q5", '0'), "q4"},
        {("q3", 'a'), "q6"},
        {("q6", 'a'), "q6"}, {("q6", '.'), "q7"},
        {("q8", 'a'), "q8"},
        {("q7", 'a'), "q8"}
    };

    public static string[] finalStatesA = { "q18" };

    public static string[] finalStatesB = { "q6", "q8" };

    public static List<string> ProcessInputA(string input)
    {
        List<string> telefonos = new List<string>();
        char x = 'x';
        string numero = "";
        string initialState = "q0";
        string currentState = initialState;
        foreach (char c in input)
        {
            x = 'x';

            if (numeros.Contains(c))
                x = '0';
            else if (guion.Contains(c))
                x = '-';
            else if (espacio.Contains(c))
                x = ' ';
            else if (parIzq.Contains(c))
                x = '(';
            else if (parDer.Contains(c))
                x = ')';

            if (transitionsA.TryGetValue((currentState, x), out string nextState))
            {
                currentState = nextState;
                numero += c;
            }
            else
            {
                if (!string.IsNullOrEmpty(numero) && finalStatesA.Contains(currentState))
                    telefonos.Add(numero);

                numero = "";
                currentState = initialState;
            }
        }
        if (finalStatesA.Contains(currentState))
            telefonos.Add(numero);

        return telefonos;
    }

    public static List<string> ProcessInputB(string input)
    {
        List<string> correos = new List<string>();
        char x = 'x';
        string correo = "";
        string initialState = "q0";
        string currentState = initialState;
        foreach (char c in input)
        {
            x = 'x';

            if (letras.Contains(c))
                x = 'a';
            else if (numeros.Contains(c))
                x = '0';
            else if (caracteres.Contains(c))
                x = 'b';
            else if (aroba.Contains(c))
                x = '@';
            else if (punto.Contains(c))
                x = '.';

            if (transitionsB.TryGetValue((currentState, x), out string nextState))
            {
                currentState = nextState;
                correo += c;
            }
            else
            {
                if (!string.IsNullOrEmpty(correo) && finalStatesB.Contains(currentState))
                    correos.Add(correo);

                correo = "";
                currentState = initialState;
            }
        }
        if (finalStatesB.Contains(currentState))
            correos.Add(correo);

        return correos;
    }

    static async Task Main()
    {
        string paginasFile = @"C:\Users\jqr_0\source\repos\AutomataNumeroTelefonico\paginas.txt";

        string[] paginas = File.ReadAllLines(paginasFile);

        if (paginas.Length == 0)
        {
            Console.WriteLine("El archivo de páginas está vacío.");
            return;
        }

        Console.WriteLine("-------------------------");
        Console.WriteLine("-- Projecto Automatas --");
        Console.WriteLine("-------------------------");
        Console.WriteLine("Extraer:");
        Console.WriteLine(" 1. Telefonos");
        Console.WriteLine(" 2. Email");

        string opc = Console.ReadLine();

        if (!(opc == "1" || opc == "2"))
        {
            Console.WriteLine("Opcion no permitida");
            return;
        }

        List<string> accepted = new List<string>();

        foreach (string pagina in paginas)
        {
            string texto = await GetWebPageText(pagina);

            if (opc == "1")
                accepted.AddRange(ProcessInputA(texto));
            else if (opc == "2")
                accepted.AddRange(ProcessInputB(texto));
        }

        if (accepted.Count > 0)
        {
            if (opc == "1")
            {
                File.WriteAllLines(@"C:\Users\jqr_0\source\repos\AutomataNumeroTelefonico\telefonos.txt", accepted);
                accepted.ForEach(Console.WriteLine);
                Console.WriteLine("Se guardaron los datos en telefonos.txt");
            }
            else if (opc == "2")
            {
                File.WriteAllLines(@"C:\Users\jqr_0\source\repos\AutomataNumeroTelefonico\emails.txt", accepted);
                accepted.ForEach(Console.WriteLine);
                Console.WriteLine("Se guardaron los datos en emails.txt");
            }
        }
        else
        {
            Console.WriteLine("No se encontraron datos.");
        }
    }

    static async Task<string> GetWebPageText(string url)
    {
        try
        {
            HttpClient client = new();
            string html = await client.GetStringAsync(url);

            HtmlDocument doc = new();
            doc.LoadHtml(html);

            return string.Join(" ", doc.DocumentNode
            .Descendants()
                .Where(n => n.NodeType == HtmlNodeType.Text && !string.IsNullOrWhiteSpace(n.InnerText))
                .Select(n => n.InnerText.Trim()));
        }
        catch (Exception ex)
        {
            return "";
        }
    }
}