using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lavoro_di_gruppo
{
    class Program
    {
        static public double[] quantitàMateriePrime = new double[14]; //riga "speciale", non si capisce il motivo ma bisogna inserire 1 valore in più

        static public int[] produzione = new int[5]; //array per contenere i dati sulla produzione

        static public string[] materiePrime = new string[] { "farina", "uova", "burro", "lievito in polvere per dolci", "latte intero", "lievito di birra", "zucchero", "cioccolata fondente", "tuorli d'uovo", "buste di vaniglia", "confettura", "bacello di vaniglia", "bicarbonato", "miele" };
        // Farina = 0
        // Uova = 1
        // Burro = 2
        // Lievito in polvere per dolci = 3
        // Latte intero = 4
        // Lievito di birra = 5
        // Zucchero = 6
        // Cioccolata fondente = 7
        // Tuorli d'uovo = 8
        // Buste di vaniglia = 9
        // Confettura = 10
        // Bacello di vaniglia = 11
        // Bicarbonato = 12
        // Miele = 13

        static public string[] materiePrimeQuant = new string[14];//array pubblico dove poter inserire le quantità delle materie prime che successivamente verranno salvate in un file

        static void Main(string[] args)
        {
            creareSpazioDiMemorizzazioneDeiDati(); //richiamo di una funzione

            letturaDelFile();

            guadagnoGiornaliero();

            calcoli();

            Console.ReadKey();
        }
        static void creareSpazioDiMemorizzazioneDeiDati()
        //funzione che mi crea una cartella con dei file al suo interno
        //controlla se essi non esistono già, in questo caso non li ricrea
        {
            int temp = 0; //variabile a cui vengono assegnati valori temporanei

            string filepath = @"C:\temp\quantitàMateriePrime.txt";//percorso del file in cui verranno salvati i file

            if (!File.Exists(filepath)) //questo comando serve a controllare l'esistenza del file, nel percorso prestabilito
            {
                // Creo il file, nel percorso stabilito, dove vi posso salvare i dati.
                using (StreamWriter sw = File.CreateText(filepath))
                {

                }

                string kili = "KG"; //queste variabili servono per differenziare le materie che si misurano in kg dalle uova, il programma svolferà i calcoli in 'unità' per quest'ultime

                string uni = "unità";

                for (int i = 0; i < 14; i++)
                {
                    if (i == 1)
                    {
                        Console.WriteLine($"Scrivimi la quantità della materia '{materiePrime[i]}' che possiedi in {uni}");
                    }
                    else
                    {
                        Console.WriteLine($"Scrivimi la quantità della materia '{materiePrime[i]}' che possiedi in {kili}");
                    }

                    temp = Int32.Parse(Console.ReadLine());//assegno a temp il valore che l'utente inserisce sulla tastiera

                    while (temp < 0)//verrà presentato a schermo il messaggio, a ripetizione, finchè non verrà inserito un valore positivo o nullo
                    {
                        Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                        temp = Int32.Parse(Console.ReadLine());
                    }

                    if (i == 1)
                    {
                        //non viene effettuato il calcolo solo all'elemento 1, che corrisponde alle uova
                    }
                    else
                    {
                        temp = temp * 1000; //noi per semplicità utilizziamo i dati in grammi
                    }

                    materiePrimeQuant[i] = temp.ToString();//converto i valori 'int' che ho inserito in valori stringa, per poterli scrivere sul mio file
                }

                File.WriteAllLines(filepath, materiePrimeQuant); //mi scrive sul file, definendone il percorso dove esso si trova, i dati contenuti nell'array "materiePrimeQuant"
            }
        }
        static void letturaDelFile()
        {
            string filepath = @"C:\temp\quantitàMateriePrime.txt";

            var data = File.ReadLines(filepath);//memorizzo in data tutti le righe

            //Console.WriteLine(data.ToArray()[3]); //mi permette di leggere una riga specifica del file, convertendo "data" in un array, in questo caso scrive
            //il dato presente alla quarta riga

            int temp = 0; //variabile per copiare il valore di data, che contiene i valori di ogni riga

            for (int i = 0; i < 14; i++)
            {
                temp = Convert.ToInt32(data.ToArray()[i]);

                quantitàMateriePrime[i] = temp;
            }

            Console.WriteLine("Premi il tasto Enter per continuare");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { } //tasto che serve per interrompere il ciclo alla pressione del tasto 'Enter'

            for (int m = 0; m < 14; m++)
            {
                Console.WriteLine($"Hai {quantitàMateriePrime[m]} di questa materia: '{materiePrime[m]}'"); //scrivo i dati presenti nel file
            }
        }
        static void guadagnoGiornaliero()
        {
            int costoVenditaPandoro = 5; //prezzo in euro

            int costoVenditaTortaAlCioccolato = 5; //prezzo al pezzo

            int costoVenditaBrioches = 2; //prezzo al pacchetto da 300 gr, 6 brioches
                                          // 95 g di farina 0, 12 ml di latte intero, 2 g di lievito di birra, 60 g di burro, 1 uovo
                                          //2,5 g di sale, 15 g di zucchero, 34 g d goccie di cioccolato

            int costoVenditaCrostata = 4; //prezzo al pezzo da 350 gr
                                          //95 g di farina 00, 47 g di burro, 37 g di zucchero semolato, 0,3 uova, 0,6 tuorli
                                          //0,3 bustine di vaniglia, 1,7 g di sale, 0,6 g di lievito per dolci, 144 g di confettura

            double costoVenditaBiscotti = 3.20; //prezzo a pacchetto da 500 gr
                                                //45 g di goccie di cioccolato, 230 g di farina 00, 1 uovo, 90 g di zucchero di canna,
                                                //0,5 g di bacello di vaniglia, 1,5 g di bicarbonato, 2,5 g di sale, 82 g di burro

            Console.WriteLine("Quanti pandori hai prodotto oggi?");

            int numeroDiProdottiVendutiPandoro = Int32.Parse(Console.ReadLine());

            double a = (costoVenditaPandoro - 3.94) * numeroDiProdottiVendutiPandoro;

            Console.WriteLine("Quante torte al cioccolato hai prodotto oggi?");

            int numeroDiProdottiVendutiTortaAlCiocciolato = Int32.Parse(Console.ReadLine());

            double b = (costoVenditaTortaAlCioccolato - 1.47) * numeroDiProdottiVendutiTortaAlCiocciolato;

            Console.WriteLine("Quante brioches hai prodotto oggi?");

            int numeroDiProdottiVendutiBrioches = Int32.Parse(Console.ReadLine());

            double c = (costoVenditaBrioches - 0.92) * numeroDiProdottiVendutiBrioches;

            Console.WriteLine("Quante crostate hai prodotto oggi?");

            int numeroDiProdottiVendutiCrostata = Int32.Parse(Console.ReadLine());

            double d = (costoVenditaCrostata - 1.29) * numeroDiProdottiVendutiCrostata;

            Console.WriteLine("Quanti biscotti hai prodotto oggi?");

            int numeroDiProdottiVendutiBiscotti = Int32.Parse(Console.ReadLine());

            double e = (costoVenditaBiscotti - 2.73) * numeroDiProdottiVendutiBiscotti;

            produzione[0] = numeroDiProdottiVendutiPandoro;
            produzione[1] = numeroDiProdottiVendutiTortaAlCiocciolato;
            produzione[2] = numeroDiProdottiVendutiBrioches;
            produzione[3] = numeroDiProdottiVendutiCrostata;
            produzione[4] = numeroDiProdottiVendutiBiscotti;

            Console.Write($"Il tuo guadagno complessivo e': {a + b + c + d + e} EURO");
        }
        static void calcoli()
        {
            string filepath = @"C:\temp\quantitàMateriePrime.txt";

            string temp = "0";

            //Pandori:
            quantitàMateriePrime[0] = quantitàMateriePrime[0] - (450 * produzione[0]);
            quantitàMateriePrime[5] = quantitàMateriePrime[5] - (14 * produzione[0]);
            quantitàMateriePrime[6] = quantitàMateriePrime[6] - (180 * produzione[0]);
            quantitàMateriePrime[1] = quantitàMateriePrime[1] - (4 * produzione[0]);
            quantitàMateriePrime[8] = quantitàMateriePrime[8] - (34 * produzione[0]);
            quantitàMateriePrime[13] = quantitàMateriePrime[13] - (20 * produzione[0]);
            quantitàMateriePrime[2] = quantitàMateriePrime[2] - (185 * produzione[0]);

            //TortaAlCioccolato:
            quantitàMateriePrime[7] = quantitàMateriePrime[7] - (75 * produzione[1]);
            quantitàMateriePrime[0] = quantitàMateriePrime[0] - (90 * produzione[1]);
            quantitàMateriePrime[6] = quantitàMateriePrime[6] - (90 * produzione[1]);
            quantitàMateriePrime[1] = quantitàMateriePrime[1] - (3 * produzione[1]);
            quantitàMateriePrime[2] = quantitàMateriePrime[2] - (100 * produzione[1]);
            quantitàMateriePrime[3] = quantitàMateriePrime[3] - (4 * produzione[1]);

            //Brioches:
            quantitàMateriePrime[0] = quantitàMateriePrime[0] - (95 * produzione[2]);
            quantitàMateriePrime[4] = quantitàMateriePrime[4] - (12 * produzione[2]);
            quantitàMateriePrime[5] = quantitàMateriePrime[5] - (2 * produzione[2]);
            quantitàMateriePrime[2] = quantitàMateriePrime[2] - (60 * produzione[2]);
            quantitàMateriePrime[1] = quantitàMateriePrime[1] - (1 * produzione[2]);
            quantitàMateriePrime[6] = quantitàMateriePrime[6] - (15 * produzione[2]);
            quantitàMateriePrime[7] = quantitàMateriePrime[7] - (34 * produzione[2]);

            //Crostata:
            quantitàMateriePrime[0] = quantitàMateriePrime[0] - (95 * produzione[3]);
            quantitàMateriePrime[2] = quantitàMateriePrime[2] - (47 * produzione[3]);
            quantitàMateriePrime[6] = quantitàMateriePrime[6] - (37 * produzione[3]);
            quantitàMateriePrime[1] = quantitàMateriePrime[1] - (0.3 * produzione[3]);
            quantitàMateriePrime[8] = quantitàMateriePrime[8] - (0.6 * produzione[3]);
            quantitàMateriePrime[9] = quantitàMateriePrime[9] - (0.3 * produzione[3]);
            quantitàMateriePrime[3] = quantitàMateriePrime[3] - (0.6 * produzione[3]);
            quantitàMateriePrime[10] = quantitàMateriePrime[10] - (145 * produzione[3]);

            //Biscotti:
            quantitàMateriePrime[7] = quantitàMateriePrime[7] - (45 * produzione[4]);
            quantitàMateriePrime[0] = quantitàMateriePrime[0] - (230 * produzione[4]);
            quantitàMateriePrime[1] = quantitàMateriePrime[1] - (1 * produzione[4]);
            quantitàMateriePrime[6] = quantitàMateriePrime[6] - (90 * produzione[4]);
            quantitàMateriePrime[11] = quantitàMateriePrime[11] - (0.5 * produzione[4]);
            quantitàMateriePrime[12] = quantitàMateriePrime[12] - (1.5 * produzione[4]);
            quantitàMateriePrime[2] = quantitàMateriePrime[2] - (82 * produzione[4]);

            for (int i = 0; i < 14; i++)
            {
                temp = Convert.ToString(quantitàMateriePrime[i]);

                materiePrimeQuant[i] = temp;
            }

            File.WriteAllLines(filepath, materiePrimeQuant);
        }
    }
}