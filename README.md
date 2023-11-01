# API "MyShop"

## Sujet 

Bonjour !

Afin d'avoir un aperçu de vos compétences, nous souhaitons avoir une API répondant aux besoins suivants :

L'api doit nous permettre de gérer des offres qui doivent avoir les données suivantes :

```JSON
{
  "ProductId": 42,
  "ProductName": "T-Shirt",
  "ProductBrand": "Sarenza",
  "ProductSize": "M",
  "Quantity": 100,
  "Price": 42.42
}
```

__routes :__
- /api/offer/all -> Retourne la liste des offres
- /api/offer/add -> Ajoute une nouvelle offre
- /api/offer/update -> Modifie une offre existante

## Data

Le stockage des données doit être découpé de la façon suivante:

- `dbo.product` : doit contenir un identifiant, un nom, une marque et une taille.
- `dbo.price` : doit contenir un prix par produit.
- `dbo.stock` : doit contenir une quantité par produit.

L'idée serait d'avoir une centaine de lignes dans ces tables lors du démarrage de l'application.


## Couche technique obligatoire :
- .NET 6
- Postgres (docker)
- Dapper
- Docker
- Swagger

Cette liste ne limite pas l'utilisation d'autres outils si vous pensez qu'ils sont un plus pour montrer votre savoir-faire.


## Rendu :
- Le rendu doit se faire via une __Pull Request__ sur [GitHub](https://github.com/).

- Il doit être possible de lancer l'API et la BDD en même temps via un docker-compose.

## Lancer le projet :
- Lancer `docker-compose up` sous le répertoire principal.
- Une fois lancé, l'endpoint est accessible via [Swagger](http://localhost/swagger/index.html), et la base Postgres est accessible avec la configuration suivante: 
  - server : 0.0.0.0:5432
  - db:username/password : myshop:postgresusr/postgrespwd