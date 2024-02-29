using Apptest;
using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Willkommen bei Basel Coin!");
        Console.Write("Benutzername: ");
        var username = Console.ReadLine();
        Console.Write("Passwort: ");
        var password = Console.ReadLine();

        if (DatabaseAccess.ValidateUser(username, password))
        {
            Console.WriteLine("Login erfolgreich!");
            Logger.Log(username, "Login erfolgreich");

            var session = new SessionManager();

            // Startet einen Timer, der regelmäßig prüft, ob die Session noch aktiv ist
            var timer = new Timer(
                e => CheckSession(session, username),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(1)); // Überprüft jede Sekunde

            while (session.IsSessionActive())
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    session.RefreshSession();
                    Console.WriteLine("Session aktualisiert.");
                    Logger.Log(username, "Session aktualisiert");
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Logger.Log(username, "Benutzer manuell abgemeldet");
                    break; // Beendet die Schleife und meldet den Benutzer ab
                }
            }

            timer.Dispose(); // Stoppt den Timer, wenn die Session endet oder der Benutzer abgemeldet wird
            Logger.Log(username, "Session abgelaufen oder Benutzer abgemeldet");
            Console.WriteLine("Benutzer abgemeldet.");
        }
        else
        {
            Console.WriteLine("Login fehlgeschlagen!");
            Logger.Log(username, "Login fehlgeschlagen");
        }
    }

    private static void CheckSession(SessionManager session, string username)
    {
        if (!session.IsSessionActive())
        {
            Logger.Log(username, "Session durch Inaktivität abgelaufen");
            Environment.Exit(0); // Beendet die Anwendung
        }
    }
}

public class SessionManager
{
    private DateTime sessionStart;
    private const int idleTimeout = 10; // Idle Timeout in Sekunden für das Beispiel

    public SessionManager()
    {
        sessionStart = DateTime.Now;
    }

    public void RefreshSession()
    {
        sessionStart = DateTime.Now;
    }

    public bool IsSessionActive()
    {
        return (DateTime.Now - sessionStart).TotalSeconds < idleTimeout;
    }
}
