namespace GestionFinances;

public class ExportView
{
    public static string AfficherMenu()
    {
        
        Console.WriteLine("====== EXPORTATION CSV ======");
        Console.WriteLine("Le fichier contiendra :");
        Console.WriteLine("- Les transactions de la session");
        Console.WriteLine("- Le bilan du mois courant");
        Console.WriteLine("Confirmer l'exportation ?");
        Console.WriteLine("1. Oui");
        Console.WriteLine("2. Non");
        Console.WriteLine("0. Retour");
        Console.WriteLine("9. Menu principal");
        Console.Write("Votre choix : _");
        return Console.ReadLine() ?? ""; // Renvoie la saisie au contrôleur
        
    }
}