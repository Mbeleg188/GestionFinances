namespace GestionFinances;

public class MenuController
{
    public static void Demarrer()
    {
        bool continuer = true;

        while (continuer)
        {
            string saisie = MenuView.AfficherMenu();
            switch (saisie)
            {
                case "1":
                    CategoryView.AfficherMenu();
                    break;
                case "2":
                    TransactionController.Demarrer();
                    break;
                case "3":
                    TableauBord.AfficherMenu();
                    break;
                case "4":
                    BilanView.AfficherMenu();
                    break;
                case "5":
                    ExportView.AfficherMenu();
                    break;
                case "0":
                    Console.WriteLine("Quitter l'application ?");
                    Console.WriteLine("1. Oui");
                    Console.WriteLine("2. Non");

                    string? confirmation = Console.ReadLine();

                    if(confirmation == "1")
                    {
                        Console.WriteLine("Fermeture de l'application...");
                        Console.ReadKey();
                        continuer = false;
                    }
                    
                    break;
            }
        }
    }
}