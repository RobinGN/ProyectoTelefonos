﻿namespace ProyectoTelefonos
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using HtmlAgilityPack;
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

        public static string[] TelsFinalState = { "q10"};
        public static string[] MailsFinalStates = {"q7", "q8"};

        public static List<string> InputsMails(string input)
        {
            List<string> Mails = new List<string>();
            string Mail = "";
            string IState = "q0";
            string AState = IState;
            char Initial = 'y';
            foreach (char Current in input)
            {
                Initial = 'y';

                if (Nums.Contains(Current))
                    Initial = 'n';
                else if (Letters.Contains(Current))
                    Initial = 'v';
                else if (At.Contains(Current))
                    Initial = '@';
                else if (SLine.Contains(Current))
                    Initial = '-';
                else if (DLine.Contains(Current))
                    Initial = '_';
                else if (Dot.Contains(Current))
                    Initial = '.';

                if (AutomatonMails.TryGetValue((AState, Initial), out string NState))
                {
                    AState = NState;
                    Mail += Current;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Mail) && MailsFinalStates.Contains(AState))
                    {
                        Mails.Add(Mail);
                    }
                    Mail = "";
                    AState = IState;
                }
            }
            if (MailsFinalStates.Contains(AState))
            {
                Mails.Add(Mail);
            }

            return Mails;
        }

        public static List<string> InputsTelephones(string input)
        {
            List<string> Telephones = new List<string>();
            string Tel = "";
            string IState = "q0";
            string AState = IState;
            char Initial = 'y';
            foreach (char Current in input)
            {
                Initial = 'y';

                if (Nums.Contains(Current))
                    Initial = 'n';
                else if (SLine.Contains(Current))
                    Initial = '-';
                else if (LeftP.Contains(Current))
                    Initial = 'l';
                else if (RightP.Contains(Current))
                    Initial = 'r';
                else if (Space.Contains(Current))
                    Initial = ' ';

                if (AutomatonTelephones.TryGetValue((AState, Initial), out string NState))
                {
                    AState = NState;
                    Tel += Current;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Tel) && TelsFinalState.Contains(AState))
                    {
                        Telephones.Add(Tel);
                    }
                    Tel = "";
                    AState = IState;
                }
            }
            if (TelsFinalState.Contains(AState))
            {
                Telephones.Add(Tel);
            }
            return Telephones;
        }

        static void Main(string[] args)
        {
            
        }
    }
}
