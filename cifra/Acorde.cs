using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cifra
{
    class Acorde
    {
        public static readonly Acorde INVALID = new Acorde(Nota.INVALID, "", Nota.INVALID);
        private static readonly char[] VARIATIONS_CHARS = { 'C', 'D', 'E', 'F', 'G', 'A', 'B', '#', 'b', 'M', 'm', '+', '-', '°', '7', '9', '1', '3', '4', '5', '6', '/', 's', 'u', 'a', 'g', '(', ')' };
                     
        private string Nome { get; set; } = "";
        private Nota Key { get; set; } = Nota.INVALID;
        private string Variation { get; set; } = "";
        private Nota Inversao { get; set; } = Nota.INVALID;

        public Acorde(string token) : this(DescobreKey(token), DescobreVariacao(token), DescobreInversao(token))
        {
        }

        public Acorde(Nota key, string variation, Nota inversao)
        {
            Key = key;
            Variation = variation;
            Inversao = inversao;

            string inv = Inversao.ToString();
            if (Inversao.Equals(Nota.INVALID)) {
                inv = "";
            }
            
            Nome = Key + Variation + inv;
        }

        private static string DescobreVariacao(string token)
        {
            string ret = "";

            if (!string.IsNullOrEmpty(token))
            {
                Nota key = DescobreKey(token);

                if (!key.Equals(Nota.INVALID))
                {
                    ret = token.Replace(key.Nome, "");

                    Nota inversao = DescobreInversao(token);
                    if (!inversao.Equals(Nota.INVALID))
                    {
                        ret = ret.Replace(inversao.Nome, "");
                    }
                }
            }
            
            return ret;
        }

        private static Nota DescobreInversao(string token)
        {
            Nota ret = Nota.INVALID;

            int variacao = token.IndexOf('/');
            if (variacao >= 0)
            {
                ret = new Nota(token.Substring(variacao + 1));
            }

            return ret;
        }

        private static Nota DescobreKey(string token)
        {
            Nota key = Nota.INVALID;

            Array.Sort(Nota.ESCALA_COMPLETA, (x, y) => x.Nome.Length.CompareTo(y.Nome.Length));
            foreach (var nota in Nota.ESCALA_COMPLETA.Reverse())
            {
                if (token.StartsWith(nota.Nome))
                {
                    key = nota;
                    break;
                }
            }

            return key;
        }
        public bool IsMaior()
        {
            bool ret;

            if (Key.IsNatural())
            {
                ret = Nome.Equals(Key.Nome) || Nome[1] != 'm';
            }
            else
            {
                ret = Nome.Equals(Key.Nome) || Nome[2] != 'm';
            }

            return ret;
        }

        public static string SubirLinha(string linha, int semiTons)
        {
            string ret = linha;
            List<Acorde> acordesAntigos = new List<Acorde>();
            List<Acorde> acordesNovos = new List<Acorde>();
            Dictionary<string, string> subs = new Dictionary<string, string>();

            if (IsChordLine(ret))
            {
                string aux = Regex.Replace(linha, " {2,}", " ").Trim();
                string[] tokens = aux.Split(' ');

                for (int i = 0; i < tokens.Length; i++)
                {
                    if (IsChord(tokens[i]))
                    {
                        try {
                            Acorde acordeVelhoTemp = new Acorde(tokens[i]);
                            Acorde acordeNovoTemp = new Acorde(tokens[i]).Subir(semiTons);
                            subs.Add(acordeVelhoTemp.ToString(), acordeNovoTemp.ToString());
                            acordesAntigos.Add(acordeVelhoTemp);
                            acordesNovos.Add(acordeNovoTemp);
                        }
                        catch (ArgumentException) { }
                    }
                }
            }
            
            return SubstituiAcorde(linha, subs);
        }

        private static string SubstituiAcorde(string linha, Dictionary<string, string> acordes)
        {
            string ret = "";
            string subsRet;
            string restoDaLinha = linha;
            int inicioAcorde;
            int fimAcorde;
            int tamanho;

            string[] tokens = Regex.Replace(linha, " {2,}", " ").Trim().Split(' ');

            foreach (var token in tokens)
            {
                if(acordes.Keys.Contains(token))
                {
                    inicioAcorde = restoDaLinha.IndexOf(token);
                    fimAcorde = inicioAcorde + token.Length;
                    tamanho = restoDaLinha.Length - fimAcorde;

                    subsRet = restoDaLinha.Substring(0, inicioAcorde) + acordes[token];
                    ret += subsRet;
                    restoDaLinha = restoDaLinha.Substring(fimAcorde, tamanho);
                }

            }

            return ret + "\r";
        }

        public Acorde Subir(int semiTons)
        {
            Acorde ret = this;

            for (int i = 0; i < semiTons; i++)
            {
                ret = ret.SubirMeioTom();
            }

            return ret;
        }

        public Acorde MudarPara(Nota nota)
        {
            Acorde ret = this;

           

            return ret;
        }

        public Acorde IrPara(Nota nota)
        {
            return INVALID;
        }

        public Acorde SubirMeioTom()
        {
            return new Acorde(Key.Proxima(), Variation, Inversao.Proxima());
        }

        public static string DescerLinha(string line, int semitTons)
        {
            string ret = line;
            List<Acorde> novosAcordes = new List<Acorde>();
            List<Acorde> acordes = new List<Acorde>();

            if (IsChordLine(ret))
            {
                string aux = Regex.Replace(line, " {2,}", " ").Trim();
                string[] tokens = aux.Split(' ');

                for (int i = 0; i < tokens.Length; i++)
                {
                    if (IsChord(tokens[i]))
                    {
                        acordes.Add(new Acorde(tokens[i]));
                        novosAcordes.Add(new Acorde(tokens[i]).Descer(semitTons));
                    }
                }

                for (int i = 0; i < acordes.Count(); i++)
                {
                    ret = Regex.Replace(ret, Regex.Escape(acordes[i].ToString()), novosAcordes[i].ToString());
                }
            }


            return ret;
        }

        public Acorde Descer(int semiTons)
        {
            Acorde ret = this;
            for (int i = 0; i < semiTons; i++)
            {
                ret = ret.DescerMeioTom();
            }

            return ret;
        }

        public Acorde DescerMeioTom()
        {
            return new Acorde(Key.Anterior(), Variation, Inversao.Anterior());
        }

        public static bool IsChord(string token)
        {
            bool ret = false;
            if (string.IsNullOrEmpty(token))
            {
                return ret;
            }

            string candidato = token.Replace("( )+", " ").Trim();
            int match = -1;
            int lastIndex = candidato.Length;

            foreach (var nota in Nota.ESCALA_COMPLETA)
            {
                if (candidato.StartsWith(nota.Nome))
                {
                    match = nota.Nome.Length;

                    string candidato_auxiliar = candidato.Replace(nota.Nome, "");
                    for (int i = 0; i < candidato_auxiliar.Length; i++)
                    {
                        foreach (var variacao in VARIATIONS_CHARS)
                        {
                            if (candidato_auxiliar[i].Equals(variacao))
                            {
                                match++;
                                break;
                            }
                        }
                    }
                }
            }

            return match == lastIndex;
        }

        public static bool IsChordLine(string line)
        {
            bool ret = false;
            if (string.IsNullOrEmpty(line))
            {
                return ret;
            }

            string aux = Regex.Replace(line, " {2,}", " ").Trim();
            string[] tokens = aux.Split(' ');
            int numeroDeTokens = tokens.Length;
            decimal count = 0;

            foreach (var token in tokens)
            {
                if (IsChord(token))
                {
                    count += 1.0m;
                }
            }

            decimal probabilidade = count / numeroDeTokens;

            return probabilidade > 0.5m;
        }

        public override string ToString()
        {
            return Nome;
        }

        public string ToFullString()
        {
            return "Nome: " + Nome + "\nKey: " + Key +" (" + Key.Valor + ")" + "\nÉ maior: "+ IsMaior() + "\nVariacao: " + Variation + "\nInversao: " + Inversao + "\nAnterior: " + DescerMeioTom() + "\nProximo: " + SubirMeioTom() + "\n\n";
        }

        public bool EssencialmenteIgual(Acorde acorde)
        {
            return this.Key.Equals(acorde.Key) && this.IsMaior().Equals(acorde.IsMaior());

        }

        public override bool Equals(object obj)
        {
            return obj is Acorde acorde &&
                   EqualityComparer<Nota>.Default.Equals(Key, acorde.Key) &&
                   Variation == acorde.Variation &&
                   EqualityComparer<Nota>.Default.Equals(Inversao, acorde.Inversao);
        }

        public override int GetHashCode()
        {
            var hashCode = 86873187;
            hashCode = hashCode * -1521134295 + EqualityComparer<Nota>.Default.GetHashCode(Key);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Variation);
            hashCode = hashCode * -1521134295 + EqualityComparer<Nota>.Default.GetHashCode(Inversao);
            return hashCode;
        }
    }
}
