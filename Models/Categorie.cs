namespace GestionFinances;

public class Categorie
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public TypeTransaction Type { get; set; }

    public Categorie(int id, string nom, TypeTransaction type)
    {
        Id = id;
        Nom = nom;
        Type = type;
    }

    public override string ToString() => $"[{Id}] {Nom} ({Type})";
}
