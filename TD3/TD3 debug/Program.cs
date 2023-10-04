using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = "Etudiants.txt"; //Fichier Etudiant

        //Le menu
        while (true)
        {
            Console.WriteLine("Menu :");
            Console.WriteLine("1. Lister les renseignements du fichier.");
            Console.WriteLine("2. Lister les renseignements du fichier avec mise en forme.");
            Console.WriteLine("3. Calcul statistique.");
            Console.WriteLine("4. Quitter");


            //Numéro invalide
            int choix;
            if (!int.TryParse(Console.ReadLine(), out choix))
            {
                Console.WriteLine("Veuillez entrer un numéro valide.");
                continue;
            }

            //Choix 

            switch (choix)
            {
                case 1:
                    ListEtudiants(filePath);
                    break;
                case 2:
                    ListEtudiantsAvecMiseEnForme(filePath);
                    break;
                case 3:
                    CalculStatistique(filePath);
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez entrer un numéro valide.");
                    break;
            }
        }
    }

    //Case 1 
    static void ListEtudiants(string filePath)
    {
        Console.WriteLine("Liste des étudiants :");
        foreach (string line in File.ReadLines(filePath))
        {
            Console.WriteLine(line);
        }
    }


    //Case 2
    static void ListEtudiantsAvecMiseEnForme(string filePath)
    {
        Console.WriteLine("Liste des étudiants avec mise en forme :");
        bool couleur = false;

        try
        {
            foreach (string line in File.ReadLines(filePath))
            {
                string[] elements = line.Split('\t'); // Utilisation de la tabulation comme délimiteur

                if (elements.Length >= 5)
                {
                    string nom = elements[0];
                    string prenom = elements[1];
                    string dateNaissanceStr = elements[2];
                    string sexe = elements[3];
                    string baccalaureat = elements[4];

                    if (DateTime.TryParse(dateNaissanceStr, out DateTime dateNaissance))
                    {
                        Console.ForegroundColor = couleur ? ConsoleColor.White : ConsoleColor.Green;

                        string dateFormatee = dateNaissance.ToString("dddd dd MMMM yyyy");

                        Console.WriteLine($"{nom} {prenom.ToUpper()} {dateFormatee} {sexe} {baccalaureat}");

                        couleur = !couleur;
                    }
                    else
                    {
                        Console.WriteLine($"Date de naissance invalide pour {nom} {prenom} : {dateNaissanceStr}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
        }
        finally
        {
            // Réinitialisation de la couleur
            Console.ResetColor();
        }
    }


    //Case 3
    static void CalculStatistique(string filePath)
    {
        int total = 0;
        int G = 0;
        int F = 0;

        try
        {
            foreach (string line in File.ReadLines(filePath))
            {
                string[] elements = line.Split('\t'); 

                if (elements.Length >= 4)
                {
                    string sexe = elements[3];

                    total++;

                    if (sexe.Equals("G", StringComparison.OrdinalIgnoreCase))
                    {
                        G++;
                    }
                    else if (sexe.Equals("F", StringComparison.OrdinalIgnoreCase))
                    {
                        F++;
                    }
                }
            }

            double pourcentageMasculin = (double)G / total * 100;
            double pourcentageFeminin = (double)F / total * 100;

            Console.WriteLine($"Nombre d'étudiants de sexe Masculin : {G} Hommes soit {pourcentageMasculin:F2}% des étudiants");
            Console.WriteLine($"Nombre d'étudiants de sexe Féminin : {F} Femmes soit {pourcentageFeminin:F2}% des étudiants");
            Console.WriteLine($"Nombre total d'étudiants : {total}");
        }
        catch
        {

        }

    }
}
