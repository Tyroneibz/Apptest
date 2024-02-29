using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apptest
{
    public static class InputValidator
    {
        public static bool ValidateInput(string input)
        {
            // Fügen Sie hier spezifische Validierungsregeln hinzu
            return !string.IsNullOrEmpty(input) && input.Length < 255;
        }
    }

}
