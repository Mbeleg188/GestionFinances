namespace GestionFinances;

public class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Montant { get; set; }
    public TypeTransaction Type { get; set; }
    public Categorie? Categorie { get; set; }
    public DateTime Date { get; set; }

    public Transaction(int id, string description, decimal montant, TypeTransaction type, Categorie? categorie, DateTime date)
    {
        Id = id;
        Description = description;
        Montant = montant;
        Type = type;
        Categorie = categorie;
        Date = date;
    }

    public override string ToString()
    {
        string categorie = Categorie?.Nom ?? "Sans catégorie";
        string signe = Type == TypeTransaction.Revenu ? "+" : "-";
        return $"[{Id}] {Date:dd/MM/yyyy} | {Description,-25} | {signe}{Montant,10:F2} FCFA | {categorie}";
    }
}
