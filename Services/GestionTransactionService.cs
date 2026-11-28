namespace GestionFinances;

public class GestionTransactionService
{
    // ── Ajouter ────────────────────────────────────────────────────────────────
    public Transaction Ajouter(string description, decimal montant, TypeTransaction type, Categorie? categorie, DateTime date)
    {
        var t = new Transaction(ApplicationData.ProchainIdTransaction(), description, montant, type, categorie, date);
        ApplicationData.Transactions.Add(t);
        return t;
    }

    // ── Lister (toutes) ────────────────────────────────────────────────────────
    public List<Transaction> ObtenirTout() => ApplicationData.Transactions;

    // ── Lister par type ────────────────────────────────────────────────────────
    public List<Transaction> ObtenirParType(TypeTransaction type) =>
        ApplicationData.Transactions.Where(t => t.Type == type).ToList();

    // ── Lister par période ─────────────────────────────────────────────────────
    public List<Transaction> ObtenirParPeriode(DateTime debut, DateTime fin) =>
        ApplicationData.Transactions
            .Where(t => t.Date >= debut && t.Date <= fin)
            .OrderBy(t => t.Date)
            .ToList();

    // ── Lister par catégorie ───────────────────────────────────────────────────
    public List<Transaction> ObtenirParCategorie(int categorieId) =>
        ApplicationData.Transactions
            .Where(t => t.Categorie?.Id == categorieId)
            .ToList();

    // ── Chercher par id ────────────────────────────────────────────────────────
    public Transaction? ObtenirParId(int id) =>
        ApplicationData.Transactions.FirstOrDefault(t => t.Id == id);

    // ── Modifier ───────────────────────────────────────────────────────────────
    public bool Modifier(int id, string? description, decimal? montant, TypeTransaction? type, Categorie? categorie, DateTime? date)
    {
        var t = ObtenirParId(id);
        if (t == null) return false;

        if (description != null) t.Description = description;
        if (montant.HasValue) t.Montant = montant.Value;
        if (type.HasValue) t.Type = type.Value;
        if (categorie != null) t.Categorie = categorie;
        if (date.HasValue) t.Date = date.Value;
        return true;
    }

    // ── Supprimer ──────────────────────────────────────────────────────────────
    public bool Supprimer(int id)
    {
        var t = ObtenirParId(id);
        if (t == null) return false;
        ApplicationData.Transactions.Remove(t);
        return true;
    }

    // ── Statistiques ───────────────────────────────────────────────────────────
    public decimal TotalRevenus() =>
        ApplicationData.Transactions
            .Where(t => t.Type == TypeTransaction.Revenu)
            .Sum(t => t.Montant);

    public decimal TotalDepenses() =>
        ApplicationData.Transactions
            .Where(t => t.Type == TypeTransaction.Depense)
            .Sum(t => t.Montant);

    public decimal Solde() => TotalRevenus() - TotalDepenses();

    public Dictionary<string, decimal> DepensesParCategorie() =>
        ApplicationData.Transactions
            .Where(t => t.Type == TypeTransaction.Depense)
            .GroupBy(t => t.Categorie?.Nom ?? "Sans catégorie")
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Montant));
}
