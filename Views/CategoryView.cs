namespace GestionFinances;

public class CategoryView()
{
    public static string AfficherMenu()
    {
        
        Console.WriteLine("====== GESTION DES CATÉGORIES ======");
        Console.WriteLine("1. Ajouter une catégorie");
        Console.WriteLine("2. Afficher les catégories");
        Console.WriteLine("3. Modifier une catégorie");
        Console.WriteLine("4. Supprimer une catégorie");
        Console.WriteLine("0. Retour");
        Console.WriteLine("9. Menu principal");
        Console.Write("Votre choix : _");
        return Console.ReadLine() ?? ""; // Renvoie la saisie au contrôleur
    }
}