namespace GestionFinances;

public class BilanView
{
    public static string AfficherMenu()
    {
        
        Console.WriteLine("====== BILAN MENSUEL ======");
        Console.WriteLine("1. Afficher le résumé financier");
        Console.WriteLine("2. Consulter la répartition des dépenses");
        Console.WriteLine("0. Retour");
        Console.WriteLine("9. Menu principal");
        Console.Write("Votre choix : _");
        return Console.ReadLine() ?? ""; // Renvoie la saisie au contrôleur
        
    }
}