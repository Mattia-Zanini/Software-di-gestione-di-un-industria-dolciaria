using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lavoro_di_gruppo
{
    class Program
    {
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
    }
}
