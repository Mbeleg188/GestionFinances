namespace GestionFinances;

public class TransactionController
{
    private static readonly GestionTransactionService _service = new();

    public static void Demarrer()
    {
        bool continuer = true;
        while (continuer)
        {
            string choix = TransactionView.AfficherMenu();
            switch (choix)
            {
                case "1":
                    Ajouter();
                    break;
                case "2":
                    AfficherHistorique();
                    break;
                case "3":
                    FiltrerTransactions();
                    break;
                case "4":
                    Modifier();
                    break;
                case "5":
                    Supprimer();
                    break;
                case "0":
                    continuer = false;
                    break;
            }
        }
    }

    // ── Ajouter ────────────────────────────────────────────────────────────────
    private static void Ajouter()
    {
        var saisie = TransactionView.SaisirTransaction();
        if (saisie == null) return;

        var (description, montant, type, categorie, date) = saisie.Value;
        var t = _service.Ajouter(description, montant, type, categorie, date);
        TransactionView.AfficherSucces($"Transaction #{t.Id} ajoutée avec succès !");
    }

    // ── Historique complet ─────────────────────────────────────────────────────
    private static void AfficherHistorique()
    {
        var liste = _service.ObtenirTout()
            .OrderByDescending(t => t.Date)
            .ToList();
        TransactionView.AfficherListe(liste);
    }

    // ── Filtres ────────────────────────────────────────────────────────────────
    private static void FiltrerTransactions()
    {
        bool continuer = true;
        while (continuer)
        {
            string choix = TransactionView.AfficherMenuFiltres();
            switch (choix)
            {
                case "1":
                    FiltrerParType();
                    break;
                case "2":
                    FiltrerParPeriode();
                    break;
                case "3":
                    FiltrerParCategorie();
                    break;
                case "0":
                    continuer = false;
                    break;
            }
        }
    }

    private static void FiltrerParType()
    {
        TypeTransaction? type = TransactionView.DemanderTypeFiltre();
        if (type == null)
        {
            TransactionView.AfficherErreur("Choix de type invalide.");
            return;
        }
        var liste = _service.ObtenirParType(type.Value);
        string titre = type == TypeTransaction.Revenu ? "REVENUS" : "DÉPENSES";
        TransactionView.AfficherListe(liste, titre);
    }

    private static void FiltrerParPeriode()
    {
        var periode = TransactionView.DemanderPeriode();
        if (periode == null) return;

        var liste = _service.ObtenirParPeriode(periode.Value.debut, periode.Value.fin);
        string titre = $"DU {periode.Value.debut:dd/MM/yyyy} AU {periode.Value.fin:dd/MM/yyyy}";
        TransactionView.AfficherListe(liste, titre);
    }

    private static void FiltrerParCategorie()
    {
        Console.Clear();
        Console.WriteLine("====== FILTRER PAR CATÉGORIE ======");
        var cats = ApplicationData.Categories;
        foreach (var c in cats)
            Console.WriteLine($"  {c.Id}. {c.Nom} ({c.Type})");
        Console.Write("ID de la catégorie : ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            TransactionView.AfficherErreur("ID invalide.");
            return;
        }

        var cat = cats.FirstOrDefault(c => c.Id == id);
        if (cat == null)
        {
            TransactionView.AfficherErreur("Catégorie introuvable.");
            return;
        }

        var liste = _service.ObtenirParCategorie(id);
        TransactionView.AfficherListe(liste, $"CATÉGORIE : {cat.Nom.ToUpper()}");
    }

    // ── Modifier ───────────────────────────────────────────────────────────────
    private static void Modifier()
    {
        AfficherHistorique();

        int id = TransactionView.DemanderIdTransaction("modifier");
        if (id == -1)
        {
            TransactionView.AfficherErreur("ID invalide.");
            return;
        }

        var t = _service.ObtenirParId(id);
        if (t == null)
        {
            TransactionView.AfficherErreur($"Aucune transaction avec l'ID {id}.");
            return;
        }

        var saisie = TransactionView.SaisirModification(t);
        if (saisie == null) return;

        var (desc, montant, type, categorie, date) = saisie.Value;
        _service.Modifier(id, desc, montant, type, categorie, date);
        TransactionView.AfficherSucces("Transaction modifiée avec succès !");
    }

    // ── Supprimer ──────────────────────────────────────────────────────────────
    private static void Supprimer()
    {
        AfficherHistorique();

        int id = TransactionView.DemanderIdTransaction("supprimer");
        if (id == -1)
        {
            TransactionView.AfficherErreur("ID invalide.");
            return;
        }

        var t = _service.ObtenirParId(id);
        if (t == null)
        {
            TransactionView.AfficherErreur($"Aucune transaction avec l'ID {id}.");
            return;
        }

        Console.WriteLine($"Voulez-vous vraiment supprimer : {t} ?");
        Console.WriteLine("1. Oui   2. Non");
        string confirm = Console.ReadLine() ?? "";
        if (confirm != "1") return;

        _service.Supprimer(id);
        TransactionView.AfficherSucces("Transaction supprimée.");
    }
}
