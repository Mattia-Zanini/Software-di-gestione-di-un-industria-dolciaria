/* Autori: Mattia Zanini, Enrico Toso Paolo, Daniel Veronese
 * 3F 2020-2021
   Software di gestione di un industria dolciaria */
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lavoro_di_gruppo
{
    class Program
    {
        static public string[] materiePrime = new string[] { "farina", "uova", "burro", "lievito in polvere per dolci", "latte intero", "lievito di birra", "zucchero", "cioccolata fondente", "tuorli d'uovo", "buste di vaniglia", "confettura", "bicarbonato", "miele" };
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
        // Bicarbonato = 11
        // Miele = 12

        static public double[] costoMateriePrime = new double[13] { 0.34, 8, 0.30, 18, 0.35, 6, 0.30, 3, 6, 203, 2.60, 1.56, 5.50 }; //costo al kg/unità di ogni materia prima, presente nel programma, uova 8€/100 unità

        static public double[] quantitàMateriePrime = new double[13]; //riga "speciale", non si capisce il motivo ma bisogna inserire 1 valore in più

        static public int[] produzione = new int[5]; //array per contenere i dati sulla produzione

        static public string[] materiePrimeQuant = new string[13];//array pubblico dove poter inserire le quantità delle materie prime che successivamente verranno salvate in un file
        static public double[] materieMaxMagazzino = new double[13];//array che contiene i dati relativi al magazzino quando è pieno

        static public string[] nomiProdotti = new string[5] { "pandori", "torte", "brioches", "crostate", "biscotti" };

        static public string filename = @"quantitàMateriePrime.txt"; //nome del file dove sono presenti i file della quantità delle materie prime

        static public string filepath = AppDomain.CurrentDomain.BaseDirectory + filename;//percorso del file in cui verranno salvati i file
        //Percorso ----> "Cartella del progetto"/Lavoro di gruppo/bin/Debug/netcoreapp2.1/quantitàMateriePrime.txt

        static public string filename2 = @"materieMaxMagazzino.txt"; //nome del file dove sono presenti i file della capienza massima del magazzino

        static public string filepath2 = AppDomain.CurrentDomain.BaseDirectory + filename2;
        //Percorso ----> "Cartella del progetto"/Lavoro di gruppo/bin/Debug/netcoreapp2.1/materieMaxMagazzino.txt

        static public int risposta = 0;

        static public bool controllo = false;

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

                controllo = true;

                Console.WriteLine("E' il tuo primo accesso, i dati inseriti verranno considerati come la  dimensione massima possibile dal tuo magazzino");

                for (int i = 0; i < 13; i++)//ciclo che mi serve per l'inserimento dei dati sull'array
                {
                    if (i == 1)
                    {
                        Console.WriteLine($"Scrivimi la quantità della materia '{materiePrime[i]}' che possiedi in unità");//questo messaggio verrà mostrato a schermo per la materia "uova", "buste di vaniglia"
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

                    if (i == 1)//non effettuo la moltiplicazione per mantenere il valoer della materia "uova" in unità
                    {
                        //non viene effettuato il calcolo solo all'elemento 1, che corrisponde alle uova, al 9 che sono le buste di vaniglia
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

            for (int i = 0; i < 13; i++) //questo ciclo serve per passare i dati del file in un array double per poterli utilizzare nei calcoli di sottrazione delle materie prime
            {
                temp = Convert.ToDouble(data.ToArray()[i]);

                quantitàMateriePrime[i] = temp;
            }

            for (int m = 0; m < 13; m++)//mostra quante materie si possiedono
            {
                if (m == 1)
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
            int costoVenditaPandoro = 3; //prezzo al pezzo

            int costoVenditaTortaAlCioccolato = 2; //prezzo al pezzo

            int costoVenditaBrioches = 1; //prezzo al pacchetto da 300 gr, 6 brioches circa

            double costoVenditaCrostata = 2.5; //prezzo al pezzo da 350 gr

            double costoVenditaBiscotti = 1.25; //prezzo a pacchetto da 500 gr

            //calcoli che informano l'utente sui guadagni, calcolati in base a dei prezzi prestabiliti
            //per ogni prodotto è presente un ciclo while che controlla se il numero inserito dall'utnte è accettabile

            Console.WriteLine("Quanti pandori hai prodotto oggi?");

            int numeroDiProdottiVendutiPandoro = Int32.Parse(Console.ReadLine());

            while (numeroDiProdottiVendutiPandoro < 0)
            {
                Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                numeroDiProdottiVendutiPandoro = Int32.Parse(Console.ReadLine());
            }

            double guadagnoPandoro = (costoVenditaPandoro - 0.87) * numeroDiProdottiVendutiPandoro;

            Console.WriteLine("Quante torte al cioccolato hai prodotto oggi?");

            int numeroDiProdottiVendutiTortaAlCiocciolato = Int32.Parse(Console.ReadLine());

            while (numeroDiProdottiVendutiTortaAlCiocciolato < 0)
            {
                Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                numeroDiProdottiVendutiTortaAlCiocciolato = Int32.Parse(Console.ReadLine());
            }

            double guadagnoTorte = (costoVenditaTortaAlCioccolato - 0.65) * numeroDiProdottiVendutiTortaAlCiocciolato;

            Console.WriteLine("Quante brioches hai prodotto oggi?");

            int numeroDiProdottiVendutiBrioches = Int32.Parse(Console.ReadLine());

            while (numeroDiProdottiVendutiBrioches < 0)
            {
                Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                numeroDiProdottiVendutiBrioches = Int32.Parse(Console.ReadLine());
            }

            double guadagnoBrioches = (costoVenditaBrioches - 0.15) * numeroDiProdottiVendutiBrioches;

            Console.WriteLine("Quante crostate hai prodotto oggi?");

            int numeroDiProdottiVendutiCrostata = Int32.Parse(Console.ReadLine());

            while (numeroDiProdottiVendutiCrostata < 0)
            {
                Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                numeroDiProdottiVendutiCrostata = Int32.Parse(Console.ReadLine());
            }

            double guadagnoCrostate = (costoVenditaCrostata - 0.50) * numeroDiProdottiVendutiCrostata;

            Console.WriteLine("Quanti biscotti hai prodotto oggi?");

            int numeroDiProdottiVendutiBiscotti = Int32.Parse(Console.ReadLine());

            while (numeroDiProdottiVendutiBiscotti < 0)
            {
                Console.WriteLine("Devi inserire un valore maggiore o uguale a 0");

                numeroDiProdottiVendutiBiscotti = Int32.Parse(Console.ReadLine());
            }

            double guadagnoBiscotti = (costoVenditaBiscotti - 0.35) * numeroDiProdottiVendutiBiscotti;

            //variabile che contengono il costo di produzione di ogni prodotto
            double costoProduzionePandoro = 0.87 * numeroDiProdottiVendutiPandoro;
            double costoProduzioneTorte = 0.65 * numeroDiProdottiVendutiTortaAlCiocciolato;
            double costoProduzioneBrioches = 0.15 * numeroDiProdottiVendutiBrioches;
            double costoProduzioneCrostate = 0.50 * numeroDiProdottiVendutiCrostata;
            double costoProduzioneBiscotti = 0.35 * numeroDiProdottiVendutiBiscotti;

            //salva la quantità di prodotti fabbricati, in un giorno
            produzione[0] = numeroDiProdottiVendutiPandoro;
            produzione[1] = numeroDiProdottiVendutiTortaAlCiocciolato;
            produzione[2] = numeroDiProdottiVendutiBrioches;
            produzione[3] = numeroDiProdottiVendutiCrostata;
            produzione[4] = numeroDiProdottiVendutiBiscotti;

            spaccio();

            Console.WriteLine($"Il costo di produzione complessivo e': {costoProduzionePandoro + costoProduzioneTorte + costoProduzioneBrioches + costoProduzioneCrostate + costoProduzioneBiscotti} EURO"); //costo di produzione
            Console.Write($"Il tuo guadagno complessivo e': {guadagnoPandoro + guadagnoTorte + guadagnoBrioches + guadagnoCrostate + guadagnoBiscotti} EURO"); //guadagno
        }
        static void spaccio()//funzione che gestisce lo spaccio
        {
            Console.WriteLine("Seleziona modalità di vendita:\nDigita 'singolo pezzo' o 'a lotti'");
            string risposta = Console.ReadLine();//prendo in ingresso la risposta dell'utente
            while (risposta != "singolo pezzo" && risposta != "a lotti")//controlla che l'utente non abbia mandato in input risposte differenti rispetto alle uniche 2 possibili
            {
                Console.Write("Modalità non selezionata\nRiscrivi la tua decisione:");
                risposta = Console.ReadLine();//ripropone la domanda nel caso l'utente abbia scritto qualcos'altro di differente rispetto a quello chiesto
            }
            switch (risposta)
            {
                case "singolo pezzo": break;
                case "a lotti": aLotti(); break;
            }
        }
        static void aLotti()//funzione che viene eseguita nel caso l'utente voglia vendere a lotti i propri prodotti
        {
            if (controllo == true)
            {
                Console.WriteLine("Da quanti pezzi è composto ogni lotto?");
                risposta = Int32.Parse(Console.ReadLine());
                while(risposta <= 1) //deve essere maggiore di 1 perchè sennò sarebbe a singolo pezzo
                {
                    Console.WriteLine("Inserisci un valore valido");
                    risposta = Int32.Parse(Console.ReadLine());
                }

                Array.Resize(ref materieMaxMagazzino, materieMaxMagazzino.Length + 1);//aumenta l'array di 1 per inserire la risposta dell'utente nel file, salvandola

                materieMaxMagazzino[13] = risposta;

                string[] materieMaxMagazzinoString = new string[14];

                for (int i = 0; i < 14; i++)//sovrascrive i dati presenti nell'array
                {
                    materieMaxMagazzinoString[i] = Convert.ToString(materieMaxMagazzino[i]);
                }

                File.WriteAllLines(filepath2, materieMaxMagazzinoString);//aggiorno la quantità di materie presenti nel magazzino virtuale
            }
            else
            {
                string temp2;
                var data = File.ReadAllLines(filepath2);//leggo i dati presenti nell'array
                temp2 = data.ToArray()[13];//preleva unicamente la risposta dell'utente, salvata la prima volta dell'avvio del programma
                risposta = Convert.ToInt32(temp2);
            }

            int[] temp = new int[5];

            for (int i = 0; i < 5; i++)//calcola i prodotti in eccesso, che verranno venduti allo spaccio
            {
                temp[i] = produzione[i];

                while(temp[i] >= risposta)
                {
                    temp[i] = temp[i] - risposta;
                }

                Console.WriteLine($"Il numero di {nomiProdotti[i]} destinati allo spaccio aziendale e': {temp[i]}");
            }
        }
        static void calcoli()//funzione che sottrae le materie utilizzate per fabbricare i prodotti dal magazzino
        {
            //Pandori:
            quantitàMateriePrime[0] = quantitàMateriePrime[0] - (450 * produzione[0]);
            quantitàMateriePrime[5] = quantitàMateriePrime[5] - (14 * produzione[0]);
            quantitàMateriePrime[6] = quantitàMateriePrime[6] - (180 * produzione[0]);
            quantitàMateriePrime[1] = quantitàMateriePrime[1] - (4 * produzione[0]);
            quantitàMateriePrime[8] = quantitàMateriePrime[8] - (34 * produzione[0]);
            quantitàMateriePrime[12] = quantitàMateriePrime[12] - (20 * produzione[0]);
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
            quantitàMateriePrime[11] = quantitàMateriePrime[11] - (1.5 * produzione[4]);
            quantitàMateriePrime[2] = quantitàMateriePrime[2] - (82 * produzione[4]);
        }
        static void rifornimenti()//funzione che mi controlla se la quantità di materie nel magazzino arriva ad un punto in cui bisogna effettuare il rifornimento delle merci
        {
            double costoRifornimento = 0;

            var data2 = File.ReadAllLines(filepath2);//preleva i dati dal file

            double temp = 0;

            string temp2;

            for (int i = 0; i < 13; i++)
            {
                temp = Convert.ToInt32(data2.ToArray()[i]);

                materieMaxMagazzino[i] = temp;
            }

            for (int i = 0; i < 13; i++)
            {
                if (quantitàMateriePrime[i] <= (materieMaxMagazzino[i] * 0.25))//controlla se le materie presenti nel magazzino siano al di sotto o al 25% rispetto alla massima capienza
                {
                    Console.WriteLine($"\nla materia {materiePrime[i]} e' sotto il 25%\nDevi rifornirti");

                    temp = materieMaxMagazzino[i] - quantitàMateriePrime[i];

                    if (i == 1)
                    {
                        costoRifornimento = costoRifornimento + ((temp * costoMateriePrime[i]) / 100);//calcolo differrente per le uovo, in unità
                    }
                    else
                    {
                        costoRifornimento = costoRifornimento + ((temp * costoMateriePrime[i]) / 1000); //calcolo del costo di rifornimento
                    }

                    quantitàMateriePrime[i] = materieMaxMagazzino[i];
                }
            }

            Console.WriteLine($"\nIl costo di rifornimento è: {costoRifornimento} EURO");

            for (int i = 0; i < 13; i++)//sovrascrive i dati presenti nell'array
            {
                temp2 = Convert.ToString(quantitàMateriePrime[i]);

                materiePrimeQuant[i] = temp2;
            }

            File.WriteAllLines(filepath, materiePrimeQuant);//aggiorno la quantità di materie presenti nel magazzino virtuale
        }
    }
}
