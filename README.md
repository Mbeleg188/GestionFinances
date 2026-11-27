

1. Présentation du projet

Le Système de gestion des finances personnelles est une application console développée en C# dans le cadre d'un projet académique.

Elle a pour objectif de permettre à un utilisateur de suivre et d'analyser ses finances personnelles en enregistrant ses revenus, ses dépenses et ses épargnes.

L'application permettra notamment de consulter sa situation financière, de suivre l'évolution de ses opérations et de mieux comprendre la répartition de ses dépenses.
2. Objectifs du projet

Enregistrer et gérer les revenus, les dépenses et les épargnes.

Organiser les opérations financières par catégories.

Consulter l'historique des transactions.

Calculer le résultat financier et le montant restant disponible.

Analyser les dépenses par catégorie.

Consulter un bilan financier mensuel.

Exporter les données au format CSV.

3. Technologies utilisées

Langage : C#

Plateforme : .NET

Type d'application : Application console

Architecture : MVC (Model – View – Controller)

Gestion des données : Collections en mémoire (List<T>)

Manipulation des fichiers : System.IO

Gestion de versions : Git et GitHub

5. Architecture du projet

Le projet adopte l'architecture MVC afin de séparer les responsabilités :

Models : représentation des données et des règles métier.

Views : interfaces console et affichage des informations.

Controllers : coordination des interactions entre les vues et les traitements.

Services : traitements métier et opérations spécialisées.

Exceptions : gestion des exceptions personnalisées lorsque nécessaire.

Exports : stockage des fichiers CSV générés.