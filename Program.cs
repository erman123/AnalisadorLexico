using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexico
{
    class Program
    {
        static int MAX = 650;
        static int MAX_RES = 3;
        static string []palabrasResevadas = { "del", "for", "is", "raise", "assert", "if", "else", "elif", "from", "lambda", "global", "not", "try", "class", "except", "or", "while", "continue", "exec", "import", "yield", "def", "finally", "in", "print" ,"int","string"};
        static char []tokenSimbolos= new char [MAX];
        //static char []auxWord =new char[MAX];  //Una variable auxiliar para guardar dos veces la palabra(token) que se vaya formando
        static string auxWord = "";
        static string []tokenNumeros= new string[MAX];
        static string []tokenIdentificadores= new string[MAX];
        static string []tokenReservadas= new string [MAX];
        static string []tokensNoValidos= new string [MAX];
        static bool esNumero = true;
       static  bool esIdentificador = true;

        //Variables auxiliares para guardar en Tokens
        static int auxTR = 0;
        static int auxTI = 0;
        static int auxTN = 0;
        static int auxTNV = 0;
        static int indexSimbolos = 0;
        static int cantidad = 0;
        static void Main(string[] args)
        {
            int count = 0;
             char []entrada= new char [MAX];
            for (int i = 0; i < MAX; i++)
            {
                   entrada[i] = '\0';
            }
            entrada[0] = 'i';
            entrada[1] = 'n';
            entrada[2] = 't';
            entrada[3] = ' ';
            entrada[4] = 'n';
            entrada[5] = 'u';
            entrada[6] = 'm';
            entrada[7] = ' ';
            entrada[8] = '=';
            entrada[9] = '2';
            entrada[10] = '3';
            entrada[11] = '4';
            entrada[12] = ';';
            entrada[13] = ' ';
            entrada[14] = 's';
            entrada[15] = 't';
            entrada[16] = 'r';
            entrada[17] = 'i';
            entrada[18] = 'n';
            entrada[19] = 'g';
           
            entrada[20] = ' ';
            entrada[21] = 'c';
            entrada[22] = 'a';
            entrada[23] = 'd';
            entrada[24] = ' ';
            entrada[25] = '=';
            entrada[26] = 'h';
            entrada[27] = 'o';
            entrada[28] = 'l';
            entrada[29] = 'a';
            entrada[30] = ';';
            entrada[31] = ' ';
            entrada[32] = '1';
            entrada[33] = 'f';
            
            char p =  ' ';
           // for (int i = 0; i < MAX; i++)
            //{
              //  p = entrada[i];
            //}

         
            string palabra = "";
            int indexPalabra = 0;
           // int indexSimbolos = 0;

            //Ciclos para Iniciar diferentes arreglos.
            for (int i = 0; i < MAX; i++)
            {
                tokenReservadas[i] = "";
            } // Fin del ciclo

            for (int i = 0; i < MAX; i++)
            {
                tokensNoValidos[i] = "";
            }

            for (int i = 0; i < MAX; i++)
            {
             //   palabra[i] = '\0';
            }

            //Este bucle es el que recorre cada caracter de la entrada(programa fuente) que ingreso el usuario, el procedimiento dentro del bucle es para formar los tokens necesarios.
            while (cantidad<=33)
            {
                p = entrada[count];
                if ((p != ' ') && (p != ','))     //Primero revisa que sea distinto de un espacio ' ' y una coma ','
                {
                    if ((p == ';') || (p == '"') || (p == '(') || (p == ')') || (p == '+') || (p == '-') || (p == '*') || (p == '/') || (p == '#') || (p == '<') || (p == '>') || (p == '='))
                    {
                        tokenSimbolos[indexSimbolos] = p;
                        indexSimbolos++;
                       // count++;
                    }
                    else
                    {
                        //  palabra[indexPalabra] = p;
                        // auxWord[indexPalabra] = p; //La razon de usar este arreglo es para mandar como parametro al metodo verificarNumero()... debido a un problema de programacion.
                        palabra = palabra + p;
                        auxWord = auxWord + p;
                        indexPalabra++;
                    }
                }
                else if ((p == ' ') || (p == ','))
                {
                    // Guarda la palabra en los respectivos tokens segun las funciones hayan examinado la palabra.
                    if (verificarReservada(palabra) == true)
                    {
                        tokenReservadas[auxTR] =  palabra.ToString();
                        palabra = "";
                        auxWord = "";
                        auxTR++;
                    }
                    else if (verificarIdentificador(palabra) == true)
                    {
                        tokenIdentificadores[auxTI] = palabra.ToString();
                        palabra = "";
                        auxWord = "";
                        auxTI++;
                    }
                    else if (verificarNumero(auxWord) == true)
                    {
                        tokenNumeros[auxTN] = auxWord.ToString();
                        palabra = "";
                        auxWord = "";
                        auxTN++;
                    }
                   
                    indexPalabra = 0;
                }
                count++;
                cantidad++;
            }

            //Esto es para revisar la ultima palabra formada que no se pudo examinar en el bucle while anterior
            if (verificarReservada(palabra) == true)
            {
                tokenReservadas[auxTR] = palabra.ToString();
                auxTR++;
            }
            else if (verificarIdentificador(palabra) == true)
            {
                tokenIdentificadores[auxTI] = palabra.ToString();
                auxTI++;
            }
            else if (verificarNumero(auxWord) == true)
            {
                tokenNumeros[auxTN] = auxWord.ToString();
                auxTN++;
            }

            //FINALMENTE SE LLAMAN A IMPRIMIR LOS RESULTADOS OBTENIDOS.
            string ent = "";
            for (int i = 0; i < entrada.Length; i++)
            {
                ent = ent + entrada[i];
            }
            Console.WriteLine("EXPRESION:{0} \n ", ent);
            imprimirTokenReservadas();
            imprimirTokenSimbolos();
            imprimirTokenIdentificadores();
            imprimirTokenNumeros();
            imprimirIdentificadoresNoValidos();

            Console.ReadKey();
           
        }


        //Inicio del desarrollo de los metodos para imprimir los resultados.
        static void imprimirTokenSimbolos()
        {
            Console.WriteLine("\n----------------\n|TOKENS SIMBOLOS|\n----------------");
            for (int i = 0; i < indexSimbolos; i++)
            {
                Console.WriteLine( tokenSimbolos[i]);
            }
        }

        static void imprimirTokenReservadas()
        {
            Console.WriteLine("\n-----------------------\n|TOKENS Reservadas|\n-----------------------");
            for (int i = 0; i < auxTR; i++)
            {
                Console.WriteLine(tokenReservadas[i]);
            }
        }

        static void imprimirTokenIdentificadores()
        {
            Console.WriteLine("\n-----------------------\n|TOKENS IDENTIFICADORES|\n-----------------------");
            for (int i = 0; i < auxTI; i++)
            {
                Console.WriteLine( tokenIdentificadores[i]);
            }
        }

        static void imprimirTokenNumeros()
        {
            Console.WriteLine("\n-----------------------\n|TOKENS NUMEROS|\n-----------------------");
            for (int i = 0; i < auxTN; i++)
            {
                Console.WriteLine(tokenNumeros[i]);
            }
        }

        static void imprimirIdentificadoresNoValidos()
        {
            Console.WriteLine(" ");
            Console.WriteLine("********************************* MENSAJES *********************************\n");
            if (auxTNV == 0)
            {
                Console.WriteLine("0 Errores, programa fuente exitoso...");
            }
            else
            {
                for (int i = 0; i < auxTNV; i++)
                {
                    string cad = tokensNoValidos[i].ToString();
                    Console.Write("ERROR: No es Valido el Token:{0} \n ", cad);
                }
            }
        }

        //Fin del desarrollo de metodos para imprimir

        // Metodo para verificar si el token formado corresponde a una palabra reservada
        static bool verificarReservada(string palabras)
        {
            int comp;
            bool esReservada = false;
            string palabra = palabras;
            for (int i = 0; i < palabrasResevadas.Length; i++)
            {
                //comp = strcmp(PalabrasReservadas[i], palabra);
                if (Equals(palabrasResevadas[i],palabra))
                {
                    esReservada = true;
                    break;
                }
            }
            return esReservada;
        }

        // Metodo para verificar si el token formado cumple para ser un identificador
        static bool verificarIdentificador(string palabra)
        {
            string auxPalabra = palabra;
            esIdentificador = false;
            short estado = 0;
            //char p = palabra[1];
            char p=' ' ;
            int i = 0;
            while (i < palabra.Length)
            {
                p = palabra[i];
                switch (estado)
                {
                    case 0:
                        if ((char.IsLetter(p)) || (p == '_'))
                        {
                            estado = 1;
                            esIdentificador = true;
                        }
                        else
                        {
                            estado = 2;
                            esIdentificador = false;
                        }
                        i++;
                        break;
                    case 1:
                        if ((char.IsLetter(p)) || (char.IsDigit(p)) || (p == '_'))
                        {
                            estado = 1;
                            esIdentificador = true;
                        }
                        else
                        {
                            estado = 2;
                            esIdentificador = false;
                        }
                        i++;
                        break;
                    case 2:
                        //No es un identificador
                        esIdentificador = false;
                        i++;
                        break;
                }
                
            }
            return esIdentificador;
        }

        //Metodo para revisar si el token formado cumple para ser un numero entero o decimal. Aparte, aqui se guardan los tokens No Validos para mostrarlos en el metodo imprimirIdentificadoresNoValidos()
        static bool verificarNumero(string palabra)
        {
            string auxPalabra = palabra;
            esNumero = false;
            short estado = 0, cont = 0;
            char p = ' ';
            int i = 0;
            while (i < palabra.Length)
            {
                p = palabra[i];
                switch (estado)
                {
                    case 0:
                        if (char.IsDigit(p))
                        {
                            estado = 0;
                            esNumero = true;
                            cont++;
                        }
                        else if (((p == '.') && (cont == 0)) || (char.IsLetter(p)))
                        {
                            estado = 2;
                            esNumero = false;
                        }
                        else if (p == '.')
                        {
                            estado = 1;
                            esNumero = false;
                        }
                        i++;
                        break;
                    case 1:
                        if (char.IsDigit(p))
                        {
                            estado = 1;
                            esNumero = true;
                        }
                        else
                        {
                            estado = 2;
                            esNumero = false;
                        }
                        i++;
                        break;
                    case 2:
                        esNumero = false;
                        p = '\0';
                        i++;
                        break;
                }
            }
            if (esNumero == false)
            {
                if (auxPalabra != "")
                {
                    tokensNoValidos[auxTNV] = auxPalabra;
                    auxTNV++;
                }
            }
            return esNumero;
        }

    }
}
