using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifra
{
    class Nota
    {
        private static readonly decimal MIN_VALUE = 1m;
        private static readonly decimal SEMI_TOM = 0.5m;
        private static readonly decimal TOM = 2 * SEMI_TOM;

        public static readonly Nota INVALID = new Nota("INVÁLIDO", "", -1m);

        public static readonly Nota C = new Nota("C", "", MIN_VALUE);
        public static readonly Nota D = new Nota("D", "", C.Valor + TOM);
        public static readonly Nota E = new Nota("E", "", D.Valor + TOM);
        public static readonly Nota F = new Nota("F", "", E.Valor + SEMI_TOM);
        public static readonly Nota G = new Nota("G", "", F.Valor + TOM);
        public static readonly Nota A = new Nota("A", "", G.Valor + TOM);
        public static readonly Nota B = new Nota("B", "", A.Valor + TOM);

        public static readonly Nota C_BEM = new Nota("C", "b", B.Valor);
        public static readonly Nota D_BEM = new Nota("D", "b", D.Valor - SEMI_TOM);
        public static readonly Nota E_BEM = new Nota("E", "b", E.Valor - SEMI_TOM);
        public static readonly Nota F_BEM = new Nota("F", "b", F.Valor - SEMI_TOM);
        public static readonly Nota G_BEM = new Nota("G", "b", G.Valor - SEMI_TOM);
        public static readonly Nota A_BEM = new Nota("A", "b", A.Valor - SEMI_TOM);
        public static readonly Nota B_BEM = new Nota("B", "b", B.Valor - SEMI_TOM);

        public static readonly Nota C_SUS = new Nota("C", "#", C.Valor + SEMI_TOM);
        public static readonly Nota D_SUS = new Nota("D", "#", D.Valor + SEMI_TOM);
        public static readonly Nota E_SUS = new Nota("E", "#", E.Valor + SEMI_TOM);
        public static readonly Nota F_SUS = new Nota("F", "#", F.Valor + SEMI_TOM);
        public static readonly Nota G_SUS = new Nota("G", "#", G.Valor + SEMI_TOM);
        public static readonly Nota A_SUS = new Nota("A", "#", A.Valor + SEMI_TOM);
        public static readonly Nota B_SUS = new Nota("B", "#", C.Valor);

        public static readonly Nota[] ESCALA_NATURAL = { C, D, E, F, G, A, B };
        public static readonly Nota[] ESCALA_CROMATICA = { C, C_SUS, D, D_SUS, E, F, F_SUS, G, G_SUS, A, A_SUS, B };
        public static readonly Nota[] ESCALA_COMPLETA = { C, C_BEM, C_SUS, D, D_BEM, D_SUS, E, E_BEM, E_SUS, F, F_BEM, F_SUS, G, G_BEM, G_SUS, A, A_BEM, A_SUS, B, B_BEM, B_SUS};

        public decimal Valor { get; set; }
        public string Acidente { get; set; }
        public string Key { get; set; }
        public string Nome { get; set; }

        private Nota(string key, string acidente, decimal valor)
        {
            Key = key;
            Acidente = acidente;
            Valor = valor;
            Nome = Key + Acidente;
        }

        public Nota(string nota)
        {
            Key = INVALID.Key;
            Acidente = INVALID.Acidente;
            Valor = INVALID.Valor;
            Nome = Key + Acidente;
            
            foreach (var n in ESCALA_COMPLETA)
            {
                if (nota.Equals(n.Nome))
                {
                    Key = n.Key;
                    Acidente = n.Acidente;
                    Valor = n.Valor;
                    Nome = Key + Acidente;
                    break;
                }
            }
        }

        public bool IsNatural()
        {
            return string.IsNullOrEmpty(Acidente);
        }

        public List<Nota> Enarmormonicas()
        {
            List<Nota> enarmonicos = new List<Nota>();

            foreach (var nota in ESCALA_COMPLETA)
            {
                if (Valor.Equals(nota.Valor))
                {
                    enarmonicos.Add(nota);
                }
            }

            return enarmonicos;
        }

        public Nota Proxima()
        {
            Nota ret;

            if (this.Equals(INVALID))
            {
                ret = INVALID;
            }else if (this.Equals(C))
            {
                ret = C_SUS;
            }
            else if (this.Equals(D))
            {
                ret = D_SUS;
            }
            else if (this.Equals(E))
            {
                ret = F;
            }
            else if (this.Equals(F))
            {
                ret = F_SUS;
            }
            else if (this.Equals(G))
            {
                ret = G_SUS;
            }
            else if (this.Equals(A))
            {
                ret = A_SUS;
            }
            else if (this.Equals(B))
            {
                ret = C;
            }
            else
            {
                ret = PorValor(this.Valor + SEMI_TOM);
            }

            return ret;
        }

        public Nota Anterior()
        {
            Nota ret;

            if (this.Equals(INVALID))
            {
                ret = INVALID;
            }
            else if (this.Equals(C))
            {
                ret = B;
            }
            else if (this.Equals(D))
            {
                ret = D_BEM;
            }
            else if (this.Equals(E))
            {
                ret = E_BEM;
            }
            else if (this.Equals(F))
            {
                ret = E;
            }
            else if (this.Equals(G))
            {
                ret = G_BEM;
            }
            else if (this.Equals(A))
            {
                ret = A_BEM;
            }
            else if (this.Equals(B))
            {
                ret = B_BEM;
            }
            else
            {
                ret = PorValor(this.Valor - SEMI_TOM);
            }

            return ret;
        }

        /**
         * Retorna um objeto Nota dado um valor decimal.
         * 
         * Se o valor for menor que o valor de da nota "C", retorna "B",
         * Se o valor for maior que o valor de da nota "B", retorna "C",
         * 
         */
        public Nota PorValor(decimal valor)
        {
            Nota ret = INVALID;

            valor %= 8m;

            if (valor < MIN_VALUE)
            {
                valor = B.Valor;
            } else if(valor > B.Valor)
            {
                valor = C.Valor;
            }

            foreach (var nota in ESCALA_CROMATICA)
            {
                if (nota.Valor.Equals(valor))
                {
                    ret = nota;
                    break;
                }
            }

            return ret;
        }

        public override string ToString()
        {
            return Nome;
        }

        public override bool Equals(object obj)
        {
            return obj is Nota nota &&
                   Valor == nota.Valor &&
                   Acidente == nota.Acidente &&
                   Nome == nota.Nome;
        }

        public override int GetHashCode()
        {
            var hashCode = 1885145769;
            hashCode = hashCode * -1521134295 + Valor.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Acidente);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nome);
            return hashCode;
        }
    }

}
