namespace GestionFinances;

/// <summary>
/// Stockage central en mémoire pour toute la session.
/// </summary>
public static class ApplicationData
{
    private static int _nextTransactionId = 1;
    private static int _nextCategorieId = 1;

    public static List<Transaction> Transactions { get; } = new();
    public static List<Categorie> Categories { get; } = new();

    static ApplicationData()
    {
        // Catégories par défaut
        Categories.Add(new Categorie(_nextCategorieId++, "Alimentation", TypeTransaction.Depense));
        Categories.Add(new Categorie(_nextCategorieId++, "Logement", TypeTransaction.Depense));
        Categories.Add(new Categorie(_nextCategorieId++, "Transport", TypeTransaction.Depense));
        Categories.Add(new Categorie(_nextCategorieId++, "Loisirs", TypeTransaction.Depense));
        Categories.Add(new Categorie(_nextCategorieId++, "Santé", TypeTransaction.Depense));
        Categories.Add(new Categorie(_nextCategorieId++, "Salaire", TypeTransaction.Revenu));
        Categories.Add(new Categorie(_nextCategorieId++, "Freelance", TypeTransaction.Revenu));
        Categories.Add(new Categorie(_nextCategorieId++, "Autre revenu", TypeTransaction.Revenu));
    }

    public static int ProchainIdTransaction() => _nextTransactionId++;
    public static int ProchainIdCategorie() => _nextCategorieId++;
}
