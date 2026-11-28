namespace GestionFinances;

public class TransactionView
{
    public static string AfficherMenu()
    {
        Console.Clear();
        Console.WriteLine("====== GESTION DES TRANSACTIONS ======");
        Console.WriteLine("1. Ajouter une transaction");
        Console.WriteLine("2. Afficher l'historique");
        Console.WriteLine("3. Filtrer les transactions");
        Console.WriteLine("4. Modifier une transaction");
        Console.WriteLine("5. Supprimer une transaction");
        Console.WriteLine("0. Retour");
        Console.Write("Votre choix : ");
        return Console.ReadLine() ?? "";
    }

    public static string AfficherMenuFiltres()
    {
        Console.Clear();
        Console.WriteLine("====== FILTRER LES TRANSACTIONS ======");
        Console.WriteLine("1. Par type (Revenu / Dépense)");
        Console.WriteLine("2. Par période (dates)");
        Console.WriteLine("3. Par catégorie");
        Console.WriteLine("0. Retour");
        Console.Write("Votre choix : ");
        return Console.ReadLine() ?? "";
    }

    // ── Saisie d'une nouvelle transaction ──────────────────────────────────────
    public static (string description, decimal montant, TypeTransaction type, Categorie? categorie, DateTime date)? SaisirTransaction()
    {
        Console.Clear();
        Console.WriteLine("====== AJOUTER UNE TRANSACTION ======");

        Console.Write("Description : ");
        string description = Console.ReadLine()?.Trim() ?? "";
        if (string.IsNullOrEmpty(description))
        {
            AfficherErreur("La description ne peut pas être vide.");
            return null;
        }

        Console.Write("Montant (FCFA) : ");
        if (!decimal.TryParse(Console.ReadLine()?.Replace(",", "."), System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out decimal montant) || montant <= 0)
        {
            AfficherErreur("Montant invalide. Entrez un nombre positif.");
            return null;
        }

        Console.WriteLine("Type : 1. Revenu   2. Dépense");
        Console.Write("Votre choix : ");
        string typeChoix = Console.ReadLine() ?? "";
        TypeTransaction type = typeChoix == "1" ? TypeTransaction.Revenu : TypeTransaction.Depense;

        Categorie? categorie = ChoisirCategorie(type);

        Console.Write("Date (JJ/MM/AAAA) [Entrée = aujourd'hui] : ");
        string dateSaisie = Console.ReadLine() ?? "";
        DateTime date = DateTime.Today;
        if (!string.IsNullOrEmpty(dateSaisie) &&
            !DateTime.TryParseExact(dateSaisie, "dd/MM/yyyy", null,
                System.Globalization.DateTimeStyles.None, out date))
        {
            AfficherErreur("Format de date invalide. Utilisez JJ/MM/AAAA.");
            return null;
        }

        return (description, montant, type, categorie, date);
    }

    // ── Saisie de modification ─────────────────────────────────────────────────
    public static (string? description, decimal? montant, TypeTransaction? type, Categorie? categorie, DateTime? date)? SaisirModification(Transaction t)
    {
        Console.Clear();
        Console.WriteLine("====== MODIFIER UNE TRANSACTION ======");
        Console.WriteLine($"Transaction actuelle : {t}");
        Console.WriteLine("(Laissez vide pour conserver la valeur actuelle)");
        Console.WriteLine();

        Console.Write($"Nouvelle description [{t.Description}] : ");
        string input = Console.ReadLine() ?? "";
        string? description = string.IsNullOrEmpty(input) ? null : input.Trim();

        Console.Write($"Nouveau montant [{t.Montant:F2}] : ");
        input = Console.ReadLine() ?? "";
        decimal? montant = null;
        if (!string.IsNullOrEmpty(input))
        {
            if (!decimal.TryParse(input.Replace(",", "."), System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal m) || m <= 0)
            {
                AfficherErreur("Montant invalide.");
                return null;
            }
            montant = m;
        }

        Console.Write($"Nouveau type [{t.Type}] (1=Revenu / 2=Dépense / Entrée=inchangé) : ");
        input = Console.ReadLine() ?? "";
        TypeTransaction? type = input switch
        {
            "1" => TypeTransaction.Revenu,
            "2" => TypeTransaction.Depense,
            _ => null
        };

        TypeTransaction typeEffectif = type ?? t.Type;
        Console.Write("Changer la catégorie ? (O/N) : ");
        string changeCat = Console.ReadLine() ?? "";
        Categorie? categorie = changeCat.ToUpper() == "O" ? ChoisirCategorie(typeEffectif) : null;

        Console.Write($"Nouvelle date [{t.Date:dd/MM/yyyy}] (JJ/MM/AAAA / Entrée=inchangé) : ");
        input = Console.ReadLine() ?? "";
        DateTime? date = null;
        if (!string.IsNullOrEmpty(input))
        {
            if (!DateTime.TryParseExact(input, "dd/MM/yyyy", null,
                    System.Globalization.DateTimeStyles.None, out DateTime d))
            {
                AfficherErreur("Format de date invalide.");
                return null;
            }
            date = d;
        }

        return (description, montant, type, categorie, date);
    }

    // ── Affichage de la liste ──────────────────────────────────────────────────
    public static void AfficherListe(List<Transaction> transactions, string titre = "HISTORIQUE DES TRANSACTIONS")
    {
        Console.Clear();
        Console.WriteLine($"====== {titre} ======");

        if (transactions.Count == 0)
        {
            Console.WriteLine("Aucune transaction à afficher.");
        }
        else
        {
            Console.WriteLine($"{"ID",-4} {"Date",-12} {"Description",-25} {"Montant",14} {"Catégorie",-20}");
            Console.WriteLine(new string('-', 80));
            foreach (var t in transactions)
            {
                string signe = t.Type == TypeTransaction.Revenu ? "+" : "-";
                string couleur = t.Type == TypeTransaction.Revenu ? "" : "";
                Console.ForegroundColor = t.Type == TypeTransaction.Revenu
                    ? ConsoleColor.Green
                    : ConsoleColor.Red;
                Console.WriteLine($"{t.Id,-4} {t.Date:dd/MM/yyyy}   {t.Description,-25} {signe}{t.Montant,10:F2} FCFA   {t.Categorie?.Nom ?? "Sans catégorie",-20}");
                Console.ResetColor();
            }
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"Total : {transactions.Count} transaction(s)");
        }

        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    // ── Demande d'un ID ────────────────────────────────────────────────────────
    public static int DemanderIdTransaction(string action)
    {
        Console.Write($"ID de la transaction à {action} : ");
        if (int.TryParse(Console.ReadLine(), out int id))
            return id;
        return -1;
    }

    // ── Choix du type pour filtre ──────────────────────────────────────────────
    public static TypeTransaction? DemanderTypeFiltre()
    {
        Console.WriteLine("1. Revenus   2. Dépenses");
        Console.Write("Votre choix : ");
        return Console.ReadLine() switch
        {
            "1" => TypeTransaction.Revenu,
            "2" => TypeTransaction.Depense,
            _ => null
        };
    }

    // ── Choix de période ───────────────────────────────────────────────────────
    public static (DateTime debut, DateTime fin)? DemanderPeriode()
    {
        Console.Write("Date de début (JJ/MM/AAAA) : ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null,
                System.Globalization.DateTimeStyles.None, out DateTime debut))
        {
            AfficherErreur("Date de début invalide.");
            return null;
        }

        Console.Write("Date de fin   (JJ/MM/AAAA) : ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null,
                System.Globalization.DateTimeStyles.None, out DateTime fin))
        {
            AfficherErreur("Date de fin invalide.");
            return null;
        }

        return (debut, fin);
    }

    // ── Utilitaires ────────────────────────────────────────────────────────────
    private static Categorie? ChoisirCategorie(TypeTransaction type)
    {
        var cats = ApplicationData.Categories.Where(c => c.Type == type).ToList();
        if (cats.Count == 0) return null;

        Console.WriteLine("Catégories disponibles :");
        foreach (var c in cats)
            Console.WriteLine($"  {c.Id}. {c.Nom}");
        Console.WriteLine("  0. Sans catégorie");
        Console.Write("Votre choix : ");

        if (int.TryParse(Console.ReadLine(), out int choix) && choix != 0)
            return cats.FirstOrDefault(c => c.Id == choix);

        return null;
    }

    public static void AfficherSucces(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✔ {message}");
        Console.ResetColor();
        Console.ReadKey();
    }

    public static void AfficherErreur(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"✘ Erreur : {message}");
        Console.ResetColor();
        Console.ReadKey();
    }
}
