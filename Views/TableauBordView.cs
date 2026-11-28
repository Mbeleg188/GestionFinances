namespace GestionFinances;

public class TableauBord
{
    public static string AfficherMenu()
    {
        
        Console.WriteLine("====== TABLEAU DE BORD : ======");
        Console.WriteLine("0. Retour");
        Console.WriteLine("9. Menu principal");
        Console.Write("Votre choix : _");
        return Console.ReadLine() ?? ""; // Renvoie la saisie au contrôleur
        
    }
}