namespace GestionFinances;

public class TransactionView()
{
    public static string AfficherMenu()
    {
        
        Console.WriteLine("====== GESTION DES CATÉGORIES ======");
        Console.WriteLine("1. Ajouter une transaction");
        Console.WriteLine("2. Afficher l'historique");
        Console.WriteLine("3. Modifier une transaction");
        Console.WriteLine("4. Supprimer une transaction");
        Console.WriteLine("0. Retour");
        Console.WriteLine("9. Menu principal");
        Console.Write("Votre choix : _");
        return Console.ReadLine() ?? ""; // Renvoie la saisie au contrôleur
    }
}