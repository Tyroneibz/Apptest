using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apptest
{
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

        public void CheckIdleTimeout()
        {
            if (!IsSessionActive())
            {
                Console.WriteLine("Session durch Inaktivität abgelaufen.");
                Environment.Exit(0); // Beendet die Anwendung
            }
        }
    }

}
