namespace GestionFinances;

public class MenuView
{
    public static string AfficherMenu()
    {
        Console.Clear(); // Nettoie la console à chaque retour au menu
        Console.WriteLine("====== GESTION FINANCES ======");
        Console.WriteLine("1. Gestion des catégories");
        Console.WriteLine("2. Gestion des transactions");
        Console.WriteLine("3. Tableau de bord financier");
        Console.WriteLine("4. Bilan mensuel");
        Console.WriteLine("5. Exporter les données");
        Console.WriteLine("0. Quitter l'application");
        Console.Write("Votre choix : _");
        return Console.ReadLine() ?? ""; // Renvoie la saisie au contrôleur
        
    }
}