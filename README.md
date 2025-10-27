# Simulazione Bancomat - ASP.NET MVC

Applicazione web ASP.NET MVC 8.0 che simula il funzionamento di un bancomat.

## Caratteristiche

- **Autenticazione con PIN**: Schermata di login con inserimento codice PIN a 4 cifre
- **Selezione importo**: Lista di importi predefiniti (20€, 50€, 100€, 200€, 500€) e possibilità di inserire importo personalizzato
- **Verifica saldo**: Controllo automatico della disponibilità prima del prelievo
- **Conferma prelievo**: Schermata di conferma con visualizzazione del nuovo saldo
- **Redirect automatico**: Dopo 5 secondi dalla conferma, ritorno automatico alla schermata iniziale
- **Persistenza dati**: Database SQLite con Entity Framework Core
- **Storico movimenti**: Registrazione di tutti i prelievi effettuati

## Tecnologie utilizzate

- ASP.NET Core MVC 8.0
- Entity Framework Core 9.0
- SQLite Database
- Bootstrap 5
- Session management

## Struttura del database

### Entità ContoCorrente
- Id (Primary Key)
- NumeroContoCorrente
- CodicePin (4 cifre)
- Saldo (decimal)

### Entità Movimento
- Id (Primary Key)
- Importo (decimal)
- DataPrelievo (DateTime)
- ContoCorrenteId (Foreign Key)

## Come eseguire l'applicazione

### Prerequisiti
- .NET SDK 8.0 o superiore

### Installazione e avvio

1. Clona il repository:
```bash
git clone <url-repository>
cd bancomat
```

2. Ripristina i pacchetti NuGet:
```bash
dotnet restore
```

3. Compila il progetto:
```bash
dotnet build
```

4. Esegui l'applicazione:
```bash
dotnet run
```

5. Apri il browser e vai a: `http://localhost:5000`

## Dati di test

L'applicazione viene inizializzata con 2 conti di test:

| Numero Conto    | PIN  | Saldo iniziale |
|-----------------|------|----------------|
| IT0000012345    | 1234 | 1.000,00€      |
| IT0000067890    | 5678 | 500,00€        |

## Flusso dell'applicazione

1. **Login**: L'utente inserisce il PIN a 4 cifre
2. **Selezione importo**: Dopo l'autenticazione, viene mostrato il saldo e le opzioni di prelievo
3. **Prelievo**: L'utente seleziona un importo predefinito o inserisce un importo custom
4. **Conferma**: Viene mostrato un messaggio di conferma con il nuovo saldo
5. **Redirect**: Dopo 5 secondi, si torna automaticamente alla schermata di login

## Struttura del progetto

```
/BancomatApp
├── Controllers/
│   └── BancomatController.cs      # Controller principale
├── Data/
│   └── BancomatDbContext.cs       # DbContext Entity Framework
├── Models/
│   ├── ContoCorrente.cs           # Entità conto corrente
│   ├── Movimento.cs               # Entità movimento
│   └── PrelievoViewModel.cs       # ViewModels
├── Views/
│   └── Bancomat/
│       ├── Login.cshtml           # Vista login
│       ├── SelezionaImporto.cshtml # Vista selezione importo
│       └── Conferma.cshtml        # Vista conferma
├── Program.cs                     # Configurazione applicazione
└── appsettings.json              # Configurazione (connection string)
```

## Note di sicurezza

Questa è un'applicazione di simulazione a scopo didattico. In un'applicazione reale:
- I PIN dovrebbero essere criptati con hash
- Si dovrebbe usare HTTPS
- Implementare rate limiting per prevenire brute force
- Aggiungere autenticazione multi-fattore
- Usare un database più robusto (SQL Server, PostgreSQL, ecc.)

## Licenza

Progetto didattico - Uso libero
