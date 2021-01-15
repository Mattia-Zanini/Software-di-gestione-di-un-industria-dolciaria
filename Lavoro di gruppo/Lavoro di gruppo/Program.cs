using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lavoro_di_gruppo
{
    class Program
    {
        static public int[] quantitàMateriePrime = new int[14]; //riga "speciale", non si capisce il motivo ma bisogna inserire 1 valore in più

        static public int[] produzione = new int[5]; //array per contenere i dati sulla produzione

        static public string[] materiePrime = new string[] { "farina", "uova", "burro", "lievito in polvere per dolci", "latte intero", "lievito di birra", "zucchero", "gocce di cioccolata fondente", "tuorli d'uovo", "buste di vaniglia", "confettura", "bacello di vaniglia", "bicarbonato", "miele" };
        // Farina = 0
        // Uova = 1
        // Burro = 2
        // Lievito in polvere per dolci = 3
        // Latte intero = 4
        // Lievito di birra = 5
        // Zucchero = 6
        // Gocce di cioccolata fondente = 7
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
        static void guadagnoGiornaliero()
        {
            int costoVenditaPandoro = 5; //prezzo in euro

            int costoVenditaTortaAlCioccolato = 5; //prezzo al pezzo

            int costoVenditaBrioches = 2; //prezzo al pacchetto da 300 gr, 6 brioches
                                          //12 ml di latte intero, 2 g di lievito di birra, 95 g di farina 0, 60 g di burro, 1 uovo
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
    }
}
