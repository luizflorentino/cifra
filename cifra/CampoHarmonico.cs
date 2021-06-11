using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cifra
{
    class CampoHarmonico
    {
        public static readonly CampoHarmonico INVALID = new CampoHarmonico(Acorde.INVALID, Acorde.INVALID, Acorde.INVALID, Acorde.INVALID, Acorde.INVALID, Acorde.INVALID, Acorde.INVALID);

        public static readonly CampoHarmonico CAMPO_HAMORNICO_C_MAIOR = new CampoHarmonico(new Acorde("C"), new Acorde("Dm"), new Acorde("Em"), new Acorde("F"), new Acorde("G"), new Acorde("Am"), new Acorde("B°"));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_D_MAIOR = new CampoHarmonico(CAMPO_HAMORNICO_C_MAIOR.Grau_1().Subir(2), CAMPO_HAMORNICO_C_MAIOR.Grau_2().Subir(2), CAMPO_HAMORNICO_C_MAIOR.Grau_3().Subir(2), CAMPO_HAMORNICO_C_MAIOR.Grau_4().Subir(2), CAMPO_HAMORNICO_C_MAIOR.Grau_5().Subir(2), CAMPO_HAMORNICO_C_MAIOR.Grau_6().Subir(2), CAMPO_HAMORNICO_C_MAIOR.Grau_7().Subir(2) );
        public static readonly CampoHarmonico CAMPO_HAMORNICO_E_MAIOR = new CampoHarmonico(CAMPO_HAMORNICO_D_MAIOR.Grau_1().Subir(2), CAMPO_HAMORNICO_D_MAIOR.Grau_2().Subir(2), CAMPO_HAMORNICO_D_MAIOR.Grau_3().Subir(2), CAMPO_HAMORNICO_D_MAIOR.Grau_4().Subir(2), CAMPO_HAMORNICO_D_MAIOR.Grau_5().Subir(2), CAMPO_HAMORNICO_D_MAIOR.Grau_6().Subir(2), CAMPO_HAMORNICO_D_MAIOR.Grau_7().Subir(2));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_F_MAIOR = new CampoHarmonico(CAMPO_HAMORNICO_E_MAIOR.Grau_1().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_2().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_3().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_4().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_5().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_6().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_7().Subir(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_G_MAIOR = new CampoHarmonico(CAMPO_HAMORNICO_F_MAIOR.Grau_1().Subir(2), CAMPO_HAMORNICO_F_MAIOR.Grau_2().Subir(2), CAMPO_HAMORNICO_F_MAIOR.Grau_3().Subir(2), CAMPO_HAMORNICO_F_MAIOR.Grau_4().Subir(2), CAMPO_HAMORNICO_F_MAIOR.Grau_5().Subir(2), CAMPO_HAMORNICO_F_MAIOR.Grau_6().Subir(2), CAMPO_HAMORNICO_F_MAIOR.Grau_7().Subir(2));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_A_MAIOR = new CampoHarmonico(CAMPO_HAMORNICO_G_MAIOR.Grau_1().Subir(2), CAMPO_HAMORNICO_G_MAIOR.Grau_2().Subir(2), CAMPO_HAMORNICO_G_MAIOR.Grau_3().Subir(2), CAMPO_HAMORNICO_G_MAIOR.Grau_4().Subir(2), CAMPO_HAMORNICO_G_MAIOR.Grau_5().Subir(2), CAMPO_HAMORNICO_G_MAIOR.Grau_6().Subir(2), CAMPO_HAMORNICO_G_MAIOR.Grau_7().Subir(2));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_B_MAIOR = new CampoHarmonico(CAMPO_HAMORNICO_A_MAIOR.Grau_1().Subir(2), CAMPO_HAMORNICO_A_MAIOR.Grau_2().Subir(2), CAMPO_HAMORNICO_A_MAIOR.Grau_3().Subir(2), CAMPO_HAMORNICO_A_MAIOR.Grau_4().Subir(2), CAMPO_HAMORNICO_A_MAIOR.Grau_5().Subir(2), CAMPO_HAMORNICO_A_MAIOR.Grau_6().Subir(2), CAMPO_HAMORNICO_A_MAIOR.Grau_7().Subir(2));

        public static readonly CampoHarmonico CAMPO_HAMORNICO_C_MAIOR_BEM = new CampoHarmonico(CAMPO_HAMORNICO_C_MAIOR.Grau_1().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_2().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_3().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_4().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_5().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_6().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_7().Descer(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_C_MAIOR_SUS = new CampoHarmonico(CAMPO_HAMORNICO_C_MAIOR.Grau_1().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_2().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_3().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_4().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_5().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_6().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_7().Subir(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_D_MAIOR_BEM = new CampoHarmonico(CAMPO_HAMORNICO_D_MAIOR.Grau_1().Descer(1), CAMPO_HAMORNICO_D_MAIOR.Grau_2().Descer(1), CAMPO_HAMORNICO_D_MAIOR.Grau_3().Descer(1), CAMPO_HAMORNICO_D_MAIOR.Grau_4().Descer(1), CAMPO_HAMORNICO_D_MAIOR.Grau_5().Descer(1), CAMPO_HAMORNICO_D_MAIOR.Grau_6().Descer(1), CAMPO_HAMORNICO_D_MAIOR.Grau_7().Descer(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_D_MAIOR_SUS = new CampoHarmonico(CAMPO_HAMORNICO_D_MAIOR.Grau_1().Subir(1), CAMPO_HAMORNICO_D_MAIOR.Grau_2().Subir(1), CAMPO_HAMORNICO_D_MAIOR.Grau_3().Subir(1), CAMPO_HAMORNICO_D_MAIOR.Grau_4().Subir(1), CAMPO_HAMORNICO_D_MAIOR.Grau_5().Subir(1), CAMPO_HAMORNICO_D_MAIOR.Grau_6().Subir(1), CAMPO_HAMORNICO_D_MAIOR.Grau_7().Subir(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_E_MAIOR_BEM = new CampoHarmonico(CAMPO_HAMORNICO_E_MAIOR.Grau_1().Descer(1), CAMPO_HAMORNICO_E_MAIOR.Grau_2().Descer(1), CAMPO_HAMORNICO_E_MAIOR.Grau_3().Descer(1), CAMPO_HAMORNICO_E_MAIOR.Grau_4().Descer(1), CAMPO_HAMORNICO_E_MAIOR.Grau_5().Descer(1), CAMPO_HAMORNICO_E_MAIOR.Grau_6().Descer(1), CAMPO_HAMORNICO_E_MAIOR.Grau_7().Descer(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_E_MAIOR_SUS = new CampoHarmonico(CAMPO_HAMORNICO_E_MAIOR.Grau_1().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_2().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_3().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_4().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_5().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_6().Subir(1), CAMPO_HAMORNICO_E_MAIOR.Grau_7().Subir(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_F_MAIOR_BEM = new CampoHarmonico(CAMPO_HAMORNICO_F_MAIOR.Grau_1().Descer(1), CAMPO_HAMORNICO_F_MAIOR.Grau_2().Descer(1), CAMPO_HAMORNICO_F_MAIOR.Grau_3().Descer(1), CAMPO_HAMORNICO_F_MAIOR.Grau_4().Descer(1), CAMPO_HAMORNICO_F_MAIOR.Grau_5().Descer(1), CAMPO_HAMORNICO_F_MAIOR.Grau_6().Descer(1), CAMPO_HAMORNICO_F_MAIOR.Grau_7().Descer(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_F_MAIOR_SUS = new CampoHarmonico(CAMPO_HAMORNICO_F_MAIOR.Grau_1().Subir(1), CAMPO_HAMORNICO_F_MAIOR.Grau_2().Subir(1), CAMPO_HAMORNICO_F_MAIOR.Grau_3().Subir(1), CAMPO_HAMORNICO_F_MAIOR.Grau_4().Subir(1), CAMPO_HAMORNICO_F_MAIOR.Grau_5().Subir(1), CAMPO_HAMORNICO_F_MAIOR.Grau_6().Subir(1), CAMPO_HAMORNICO_F_MAIOR.Grau_7().Subir(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_G_MAIOR_BEM = new CampoHarmonico(CAMPO_HAMORNICO_G_MAIOR.Grau_1().Descer(1), CAMPO_HAMORNICO_G_MAIOR.Grau_2().Descer(1), CAMPO_HAMORNICO_G_MAIOR.Grau_3().Descer(1), CAMPO_HAMORNICO_G_MAIOR.Grau_4().Descer(1), CAMPO_HAMORNICO_G_MAIOR.Grau_5().Descer(1), CAMPO_HAMORNICO_G_MAIOR.Grau_6().Descer(1), CAMPO_HAMORNICO_G_MAIOR.Grau_7().Descer(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_G_MAIOR_SUS = new CampoHarmonico(CAMPO_HAMORNICO_G_MAIOR.Grau_1().Subir(1), CAMPO_HAMORNICO_G_MAIOR.Grau_2().Subir(1), CAMPO_HAMORNICO_G_MAIOR.Grau_3().Subir(1), CAMPO_HAMORNICO_G_MAIOR.Grau_4().Subir(1), CAMPO_HAMORNICO_G_MAIOR.Grau_5().Subir(1), CAMPO_HAMORNICO_G_MAIOR.Grau_6().Subir(1), CAMPO_HAMORNICO_G_MAIOR.Grau_7().Subir(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_A_MAIOR_BEM = new CampoHarmonico(CAMPO_HAMORNICO_A_MAIOR.Grau_1().Descer(1), CAMPO_HAMORNICO_A_MAIOR.Grau_2().Descer(1), CAMPO_HAMORNICO_A_MAIOR.Grau_3().Descer(1), CAMPO_HAMORNICO_A_MAIOR.Grau_4().Descer(1), CAMPO_HAMORNICO_A_MAIOR.Grau_5().Descer(1), CAMPO_HAMORNICO_A_MAIOR.Grau_6().Descer(1), CAMPO_HAMORNICO_A_MAIOR.Grau_7().Descer(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_A_MAIOR_SUS = new CampoHarmonico(CAMPO_HAMORNICO_A_MAIOR.Grau_1().Subir(1), CAMPO_HAMORNICO_A_MAIOR.Grau_2().Subir(1), CAMPO_HAMORNICO_A_MAIOR.Grau_3().Subir(1), CAMPO_HAMORNICO_A_MAIOR.Grau_4().Subir(1), CAMPO_HAMORNICO_A_MAIOR.Grau_5().Subir(1), CAMPO_HAMORNICO_A_MAIOR.Grau_6().Subir(1), CAMPO_HAMORNICO_A_MAIOR.Grau_7().Subir(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_B_MAIOR_BEM = new CampoHarmonico(CAMPO_HAMORNICO_B_MAIOR.Grau_1().Descer(1), CAMPO_HAMORNICO_B_MAIOR.Grau_2().Descer(1), CAMPO_HAMORNICO_B_MAIOR.Grau_3().Descer(1), CAMPO_HAMORNICO_B_MAIOR.Grau_4().Descer(1), CAMPO_HAMORNICO_B_MAIOR.Grau_5().Descer(1), CAMPO_HAMORNICO_B_MAIOR.Grau_6().Descer(1), CAMPO_HAMORNICO_B_MAIOR.Grau_7().Descer(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_B_MAIOR_SUS = new CampoHarmonico(CAMPO_HAMORNICO_B_MAIOR.Grau_1().Subir(1), CAMPO_HAMORNICO_B_MAIOR.Grau_2().Subir(1), CAMPO_HAMORNICO_B_MAIOR.Grau_3().Subir(1), CAMPO_HAMORNICO_B_MAIOR.Grau_4().Subir(1), CAMPO_HAMORNICO_B_MAIOR.Grau_5().Subir(1), CAMPO_HAMORNICO_B_MAIOR.Grau_6().Subir(1), CAMPO_HAMORNICO_B_MAIOR.Grau_7().Subir(1));

        public static readonly CampoHarmonico CAMPO_HAMORNICO_A_MENOR = new CampoHarmonico(CAMPO_HAMORNICO_C_MAIOR.Grau_6(), CAMPO_HAMORNICO_C_MAIOR.Grau_7(), CAMPO_HAMORNICO_C_MAIOR.Grau_1(), CAMPO_HAMORNICO_C_MAIOR.Grau_2(), CAMPO_HAMORNICO_C_MAIOR.Grau_3(), CAMPO_HAMORNICO_C_MAIOR.Grau_4(), CAMPO_HAMORNICO_C_MAIOR.Grau_5());
        public static readonly CampoHarmonico CAMPO_HAMORNICO_B_MENOR = new CampoHarmonico(CAMPO_HAMORNICO_D_MAIOR.Grau_6(), CAMPO_HAMORNICO_D_MAIOR.Grau_7(), CAMPO_HAMORNICO_D_MAIOR.Grau_1(), CAMPO_HAMORNICO_D_MAIOR.Grau_2(), CAMPO_HAMORNICO_D_MAIOR.Grau_3(), CAMPO_HAMORNICO_D_MAIOR.Grau_4(), CAMPO_HAMORNICO_D_MAIOR.Grau_5());
        public static readonly CampoHarmonico CAMPO_HAMORNICO_C_MENOR = new CampoHarmonico(CAMPO_HAMORNICO_E_MAIOR_BEM.Grau_6(), CAMPO_HAMORNICO_E_MAIOR_BEM.Grau_7(), CAMPO_HAMORNICO_E_MAIOR_BEM.Grau_1(), CAMPO_HAMORNICO_E_MAIOR_BEM.Grau_2(), CAMPO_HAMORNICO_E_MAIOR_BEM.Grau_3(), CAMPO_HAMORNICO_E_MAIOR_BEM.Grau_4(), CAMPO_HAMORNICO_E_MAIOR_BEM.Grau_5());
        public static readonly CampoHarmonico CAMPO_HAMORNICO_D_MENOR = new CampoHarmonico(CAMPO_HAMORNICO_F_MAIOR.Grau_6(), CAMPO_HAMORNICO_F_MAIOR.Grau_7(), CAMPO_HAMORNICO_F_MAIOR.Grau_1(), CAMPO_HAMORNICO_F_MAIOR.Grau_2(), CAMPO_HAMORNICO_F_MAIOR.Grau_3(), CAMPO_HAMORNICO_F_MAIOR.Grau_4(), CAMPO_HAMORNICO_F_MAIOR.Grau_5());
        public static readonly CampoHarmonico CAMPO_HAMORNICO_E_MENOR = new CampoHarmonico(CAMPO_HAMORNICO_G_MAIOR.Grau_6(), CAMPO_HAMORNICO_G_MAIOR.Grau_7(), CAMPO_HAMORNICO_G_MAIOR.Grau_1(), CAMPO_HAMORNICO_G_MAIOR.Grau_2(), CAMPO_HAMORNICO_G_MAIOR.Grau_3(), CAMPO_HAMORNICO_G_MAIOR.Grau_4(), CAMPO_HAMORNICO_G_MAIOR.Grau_5());
        public static readonly CampoHarmonico CAMPO_HAMORNICO_F_MENOR = new CampoHarmonico(CAMPO_HAMORNICO_A_MAIOR_BEM.Grau_6(), CAMPO_HAMORNICO_A_MAIOR_BEM.Grau_7(), CAMPO_HAMORNICO_A_MAIOR_BEM.Grau_1(), CAMPO_HAMORNICO_A_MAIOR_BEM.Grau_2(), CAMPO_HAMORNICO_A_MAIOR_BEM.Grau_3(), CAMPO_HAMORNICO_A_MAIOR_BEM.Grau_4(), CAMPO_HAMORNICO_A_MAIOR_BEM.Grau_5());
        public static readonly CampoHarmonico CAMPO_HAMORNICO_G_MENOR = new CampoHarmonico(CAMPO_HAMORNICO_B_MAIOR_BEM.Grau_6(), CAMPO_HAMORNICO_B_MAIOR_BEM.Grau_7(), CAMPO_HAMORNICO_B_MAIOR_BEM.Grau_1(), CAMPO_HAMORNICO_B_MAIOR_BEM.Grau_2(), CAMPO_HAMORNICO_B_MAIOR_BEM.Grau_3(), CAMPO_HAMORNICO_B_MAIOR_BEM.Grau_4(), CAMPO_HAMORNICO_B_MAIOR_BEM.Grau_5());

        public static readonly CampoHarmonico CAMPO_HAMORNICO_A_MENOR_BEM = new CampoHarmonico(CAMPO_HAMORNICO_C_MAIOR.Grau_6().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_7().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_1().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_2().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_3().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_4().Descer(1), CAMPO_HAMORNICO_C_MAIOR.Grau_5().Descer(1));
        public static readonly CampoHarmonico CAMPO_HAMORNICO_A_MENOR_SUS = new CampoHarmonico(CAMPO_HAMORNICO_C_MAIOR.Grau_6().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_7().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_1().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_2().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_3().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_4().Subir(1), CAMPO_HAMORNICO_C_MAIOR.Grau_5().Subir(1));

        public static readonly CampoHarmonico[] CAMPOS_HAMORNICOS_MAIORES = { CAMPO_HAMORNICO_C_MAIOR_BEM, CAMPO_HAMORNICO_C_MAIOR, CAMPO_HAMORNICO_C_MAIOR_SUS, CAMPO_HAMORNICO_D_MAIOR_BEM, CAMPO_HAMORNICO_D_MAIOR, CAMPO_HAMORNICO_D_MAIOR_SUS, CAMPO_HAMORNICO_E_MAIOR_BEM, CAMPO_HAMORNICO_E_MAIOR, CAMPO_HAMORNICO_E_MAIOR_SUS, CAMPO_HAMORNICO_F_MAIOR_BEM, CAMPO_HAMORNICO_F_MAIOR, CAMPO_HAMORNICO_F_MAIOR_SUS, CAMPO_HAMORNICO_G_MAIOR_BEM, CAMPO_HAMORNICO_G_MAIOR, CAMPO_HAMORNICO_G_MAIOR_SUS, CAMPO_HAMORNICO_A_MAIOR_BEM, CAMPO_HAMORNICO_A_MAIOR, CAMPO_HAMORNICO_A_MAIOR_SUS, CAMPO_HAMORNICO_B_MAIOR_BEM, CAMPO_HAMORNICO_B_MAIOR, CAMPO_HAMORNICO_B_MAIOR_SUS};

        private readonly Acorde[] Campo = { Acorde.INVALID, Acorde.INVALID, Acorde.INVALID, Acorde.INVALID, Acorde.INVALID, Acorde.INVALID, Acorde.INVALID};

        public CampoHarmonico(Acorde grau_1, Acorde grau_2, Acorde grau_3, Acorde grau_4, Acorde grau_5, Acorde grau_6, Acorde grau_7)
        {
            Campo[0] = grau_1;
            Campo[1] = grau_2;
            Campo[2] = grau_3;
            Campo[3] = grau_4;
            Campo[4] = grau_5;
            Campo[5] = grau_6;
            Campo[6] = grau_7;
        }

        public Acorde Grau_1()
        {
            return Campo[0];
        }

        public Acorde Grau_2()
        {
            return Campo[1];
        }

        public Acorde Grau_3()
        {
            return Campo[2];
        }

        public Acorde Grau_4()
        {
            return Campo[3];
        }

        public Acorde Grau_5()
        {
            return Campo[4];
        }

        public Acorde Grau_6()
        {
            return Campo[5];
        }

        public Acorde Grau_7()
        {
            return Campo[6];
        }

        public static CampoHarmonico DescobreCampoHarmonico(List<Range> linhasDeCifra)
        {
            HashSet<Acorde> acordes = new HashSet<Acorde>();

            foreach (var range in linhasDeCifra)
            {
                string aux = Regex.Replace(range.Text.TrimEnd(), " {2,}", " ").Trim();
                string[] tokens = aux.Split(' ');

                for (int i = 0; i < tokens.Length; i++)
                {
                    if (Acorde.IsChord(tokens[i]))
                    {
                        acordes.Add(new Acorde(tokens[i]));
                    }
                }
            }

            return DescobreCampoHarmonico(acordes);
        }

        public static CampoHarmonico DescobreCampoHarmonico(HashSet<Acorde> acordes)
        {
            CampoHarmonico ret = INVALID;
            Dictionary<CampoHarmonico, int> contagem = new Dictionary<CampoHarmonico, int>();
            int maiorMatch = 0;

            foreach (var campo in CAMPOS_HAMORNICOS_MAIORES)
            {
                contagem.Add(campo, 0);
            }

            foreach (var acorde in acordes)
            {
                foreach (var campo in CAMPOS_HAMORNICOS_MAIORES)
                {
                    if (campo.ContemAcorde(acorde) >= 0)
                    {
                        contagem[campo] = contagem[campo] + 1;
                        if(contagem[campo] > maiorMatch)
                        {
                            ret = campo;
                            maiorMatch = contagem[campo];
                        }
                    }
                }
            }

            return ret;
        }

        /**
         * Se contiver o agorde, retorna o grau do acorde começando de 0.
         * Caso contrário retorna -1
         */
        public int ContemAcorde(Acorde acorde)
        {
            int ret = -1;

            for (int i = 0; i < Campo.Length; i++)
            {
                if (Campo[i].EssencialmenteIgual(acorde))
                {
                    ret = i;
                    break;
                }
            }

            return ret;
        }

        public override string ToString()
        {
            return Grau_1() + "\t" + Grau_2() + "\t" + Grau_3() + "\t" + Grau_4() + "\t" + Grau_5() + "\t" + Grau_6() + "\t" + Grau_7();
        }
    }
}
