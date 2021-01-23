using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lavoro_di_gruppo
{
    class Program
    {
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

        static public double[] costoMateriePrime = new double[14] { 0.85, 8, 0.30, 25, 1.44, 6.40, 0.30, 7.99, 6.60, 295, 6, 2, 2, 5.50 }; //costo al kg di ogni materia prima, presente nel programma

        static public double[] quantitàMateriePrime = new double[14]; //riga "speciale", non si capisce il motivo ma bisogna inserire 1 valore in più

        static public int[] produzione = new int[5]; //array per contenere i dati sulla produzione

        static public string[] materiePrimeQuant = new string[14];//array pubblico dove poter inserire le quantità delle materie prime che successivamente verranno salvate in un file
        static public double[] materieMaxMagazzino = new double[14];//array che contiene i dati relativi al magazzino quando è pieno

        static public string filename = @"quantitàMateriePrime.txt"; //nome del file dove sono presenti i file della quantità delle materie prime

        static public string filepath = AppDomain.CurrentDomain.BaseDirectory + filename;//percorso del file in cui verranno salvati i file
        //Percorso ----> "Cartella del progetto"/Lavoro di gruppo/bin/Debug/netcoreapp2.1/quantitàMateriePrime.txt

        static public string filename2 = @"materieMaxMagazzino.txt"; //nome del file dove sono presenti i file della capienza massima del magazzino

        static public string filepath2 = AppDomain.CurrentDomain.BaseDirectory + filename2;
        //Percorso ----> "Cartella del progetto"/Lavoro di gruppo/bin/Debug/netcoreapp2.1/materieMaxMagazzino.txt

        static void Main(string[] args)
        {
            //Richiamo le funzioni che caratterizzano il progetto
            creareSpazioDiMemorizzazioneDeiDati();

            letturaDelFile();

            guadagnoGiornaliero();

            calcoli();

            rifornimenti();

            Console.ReadKey();
        }
        static void creareSpazioDiMemorizzazioneDeiDati()
        //funzione che mi crea una cartella con dei file al suo interno
        //controlla se essi non esistono già, in questo caso non li ricrea
        {
            int temp = 0; //variabile a cui vengono assegnati valori temporanei

            if (!File.Exists(filepath)) //questo comando serve a controllare l'esistenza del file, nel percorso prestabilito
            {
                // Creo il file, nel percorso stabilito, dove vi posso salvare i dati relativi alla quantità delle materie prime che l'utente possiede
                using (StreamWriter sw = File.CreateText(filepath))
                {

                }

                // Creo il file, che mi salva i dati relativi alla capienza massima del magazzino
                using (StreamWriter sw = File.CreateText(filepath2))
                {

                }

                Console.WriteLine("E' il tuo primo accesso, i dati inseriti verranno considerati come la  dimensione massima possibile dal tuo magazzino");

                for (int i = 0; i < 14; i++)//ciclo che mi serve per l'inserimento dei dati sull'array
                {
                    if (i == 1 || i == 9 || i == 11)
                    {
                        Console.WriteLine($"Scrivimi la quantità della materia '{materiePrime[i]}' che possiedi in unità");//questo messaggio verrà mostrato a schermo per la materia "uova", "bacello di vaniglia", "buste di vaniglia"
                    }
                    else
                    {
                        Console.WriteLine($"Scrivimi la quantità della materia '{materiePrime[i]}' che possiedi in KG");
                    }

                    temp = Int32.Parse(Console.ReadLine());//assegno a temp il valore che l'utente inserisce sulla tastiera

                    while (temp < 0)//verrà presentato a schermo il messaggio, a ripetizione, finchè non verrà inserito un valore positivo o nullo
                    {
                        Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                        temp = Int32.Parse(Console.ReadLine());
                    }

                    if (i == 1 || i == 9 || i == 11)//non effettuo la moltiplicazione per mantenere il valoer della materia "uova" in unità
                    {
                        //non viene effettuato il calcolo solo all'elemento 1, che corrisponde alle uova, al 9 e al 11, che sono rispettivamente le buste di vaniglia e il bacello di vaniglia
                    }
                    else
                    {
                        temp = temp * 1000; //per semplicità il programma lavora i dati in grammi
                    }

                    materiePrimeQuant[i] = temp.ToString();//converto i valori 'int' che ho inserito in valori stringa, per poterli scrivere sul mio file
                }

                File.WriteAllLines(filepath, materiePrimeQuant); //scrive sul file, definendone il percorso dove esso si trova, i dati contenuti nell'array "materiePrimeQuant"

                File.WriteAllLines(filepath2, materiePrimeQuant); //scrive sul file la capienza massima del magazzino
            }
        }
        static void letturaDelFile()
        {
            var data = File.ReadLines(filepath);//memorizza in data tutti le righe

            double temp = 0; //variabile per copiare il valore di data, che contiene i valori di ogni riga, considerando ogni riga come un elemento dell'array

            for (int i = 0; i < 14; i++) //questo ciclo serve per passare i dati del file in un array double per poterli utilizzare nei calcoli di sottrazione delle materie prime
            {
                temp = Convert.ToDouble(data.ToArray()[i]);

                quantitàMateriePrime[i] = temp;
            }

            for (int m = 0; m < 14; m++)//mostra quante materie si possiedono
            {
                if (m == 1 || m == 9 || m == 11)
                {
                    Console.WriteLine($"Hai {quantitàMateriePrime[m]} unità di questa materia: '{materiePrime[m]}'");
                }
                else
                {
                    Console.WriteLine($"Hai {quantitàMateriePrime[m] / 1000} KG di questa materia: '{materiePrime[m]}'");
                }
            }
        }
        static void guadagnoGiornaliero()
        {
            int costoVenditaPandoro = 5; //prezzo al pezzo

            int costoVenditaTortaAlCioccolato = 5; //prezzo al pezzo

            int costoVenditaBrioches = 2; //prezzo al pacchetto da 300 gr, 6 brioches circa

            int costoVenditaCrostata = 4; //prezzo al pezzo da 350 gr

            double costoVenditaBiscotti = 3.20; //prezzo a pacchetto da 500 gr

            //calcoli che informano l'utente sui guadagni, calcolati in base a dei prezzi prestbiliti
            //per ogni prodotto è presente un ciclo while che controlla se il numero inserito dall'utnte è accettabile

            Console.WriteLine("Quanti pandori hai prodotto oggi?");

            int numeroDiProdottiVendutiPandoro = Int32.Parse(Console.ReadLine());

            while (numeroDiProdottiVendutiPandoro < 0)
            {
                Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                numeroDiProdottiVendutiPandoro = Int32.Parse(Console.ReadLine());
            }

            double a = (costoVenditaPandoro - 3.94) * numeroDiProdottiVendutiPandoro;

            Console.WriteLine("Quante torte al cioccolato hai prodotto oggi?");

            int numeroDiProdottiVendutiTortaAlCiocciolato = Int32.Parse(Console.ReadLine());

            while (numeroDiProdottiVendutiTortaAlCiocciolato < 0)
            {
                Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                numeroDiProdottiVendutiTortaAlCiocciolato = Int32.Parse(Console.ReadLine());
            }

            double b = (costoVenditaTortaAlCioccolato - 1.47) * numeroDiProdottiVendutiTortaAlCiocciolato;

            Console.WriteLine("Quante brioches hai prodotto oggi?");

            int numeroDiProdottiVendutiBrioches = Int32.Parse(Console.ReadLine());

            while (numeroDiProdottiVendutiBrioches < 0)
            {
                Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                numeroDiProdottiVendutiBrioches = Int32.Parse(Console.ReadLine());
            }

            double c = (costoVenditaBrioches - 0.92) * numeroDiProdottiVendutiBrioches;

            Console.WriteLine("Quante crostate hai prodotto oggi?");

            int numeroDiProdottiVendutiCrostata = Int32.Parse(Console.ReadLine());

            while (numeroDiProdottiVendutiCrostata < 0)
            {
                Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                numeroDiProdottiVendutiCrostata = Int32.Parse(Console.ReadLine());
            }

            double d = (costoVenditaCrostata - 1.29) * numeroDiProdottiVendutiCrostata;

            Console.WriteLine("Quanti biscotti hai prodotto oggi?");

            int numeroDiProdottiVendutiBiscotti = Int32.Parse(Console.ReadLine());

            while (numeroDiProdottiVendutiBiscotti < 0)
            {
                Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                numeroDiProdottiVendutiBiscotti = Int32.Parse(Console.ReadLine());
            }

            double e = (costoVenditaBiscotti - 2.73) * numeroDiProdottiVendutiBiscotti;

            //variabile che contengono il costo di produzione di ogni prodotto
            double f = 3.94 * numeroDiProdottiVendutiPandoro;
            double g = 1.47 * numeroDiProdottiVendutiTortaAlCiocciolato;
            double h = 0.92 * numeroDiProdottiVendutiBrioches;
            double i = 1.29 * numeroDiProdottiVendutiCrostata;
            double l = 2.73 * numeroDiProdottiVendutiBiscotti;

            produzione[0] = numeroDiProdottiVendutiPandoro;
            produzione[1] = numeroDiProdottiVendutiTortaAlCiocciolato;
            produzione[2] = numeroDiProdottiVendutiBrioches;
            produzione[3] = numeroDiProdottiVendutiCrostata;
            produzione[4] = numeroDiProdottiVendutiBiscotti;

            Console.WriteLine($"Il costo di produzione complessivo e': {f + g + h + i + l} EURO");
            Console.Write($"Il tuo guadagno complessivo e': {a + b + c + d + e} EURO");
        }
        static void calcoli()
        {
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

            for (int i = 0; i < 14; i++)//sovrascrive i dati presenti nell'array
            {
                temp = Convert.ToString(quantitàMateriePrime[i]);

                materiePrimeQuant[i] = temp;
            }

            File.WriteAllLines(filepath, materiePrimeQuant);//aggiorno la quantità di materie presenti nel magazzino virtuale
        }
        static void rifornimenti()//funzione che mi controlla se la quantità di materie nel magazzino arriva ad un punto in cui bisogna effettuare il rifornimento delle merci
        {
            double costoRifornimento = 0;

            var data2 = File.ReadAllLines(filepath2);

            int temp = 0;

            for (int i = 0; i < 14; i++)
            {
                temp = Convert.ToInt32(data2.ToArray()[i]);

                materieMaxMagazzino[i] = temp;
            }

            for (int i = 0; i < 14; i++)
            {
                if (quantitàMateriePrime[i] <= (materieMaxMagazzino[i] * 0.25))
                {
                    Console.WriteLine($"\n la materia {materiePrime[i]} e' sotto il 25%\nDevi rifornirti");

                }
                Console.WriteLine($"Il costo di rifornimento è: {costoRifornimento}");
            }
        }
    }
}