using System.Globalization;

namespace ConsolaEntregable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Configuración de la cultura para español de España
            CultureInfo culturaEs = new CultureInfo("es-ES");
            DateTime hoy = DateTime.Today;

            // 1. Entrada de nombre completo
            Console.Write("Introduce tu nombre completo: ");
            string nombreCompleto = Console.ReadLine();

            // Extracción del primer nombre usando IndexOf y Substring
            string primerNombre = nombreCompleto.Trim();
            int espacioIndex = primerNombre.IndexOf(' ');
            if (espacioIndex != -1)
            {
                primerNombre = primerNombre.Substring(0, espacioIndex);
            }

            // 2. Entrada y validación de fecha de nacimiento
            DateTime fechaNacimiento;
            while (true)
            {
                Console.Write("Fecha de nacimiento (dd/MM/yyyy): ");
                string inputFecha = Console.ReadLine();

                // Validar formato y cultura
                if (DateTime.TryParse(inputFecha, culturaEs, DateTimeStyles.None, out fechaNacimiento))
                {
                    // Rechazar fechas futuras
                    if (fechaNacimiento > hoy)
                    {
                        Console.WriteLine("Error: La fecha de nacimiento no puede ser en el futuro.");
                    }
                    else
                    {
                        break; // Fecha válida
                    }
                }
                else
                {
                    Console.WriteLine("Formato no válido. Por favor, usa el formato dd/MM/yyyy.");
                }
            }

            Console.WriteLine("\n-----------------------------------------");
            Console.WriteLine($"Hola, {primerNombre}!");

            // 3. Cálculo de la edad exacta
            int edad = hoy.Year - fechaNacimiento.Year;
            // Restar un año si aún no ha llegado el día del cumple este año
            if (hoy < fechaNacimiento.AddYears(edad)) edad--;
            Console.WriteLine($"Tienes {edad} años.");

            // 4. Fecha en formato largo
            string fechaLarga = fechaNacimiento.ToString("dddd, d 'de' MMMM 'de' yyyy", culturaEs);
            Console.WriteLine($"Tu cumpleaños es el {fechaLarga}.");

            // 5. Signo del zodiaco (Aproximación por mes/día)
            string signo = ObtenerSignoZodiaco(fechaNacimiento.Day, fechaNacimiento.Month);
            Console.WriteLine($"Tu signo del zodiaco es {signo}.");

            // 6. Días hasta el próximo cumpleaños
            DateTime proximoCumple = new DateTime(hoy.Year, fechaNacimiento.Month, fechaNacimiento.Day);

            // Si ya pasó este año, calculamos para el año que viene
            if (proximoCumple < hoy) proximoCumple = proximoCumple.AddYears(1);

            if (proximoCumple == hoy)
            {
                Console.WriteLine("¡Felicidades! ¡Hoy es tu cumpleaños! 🎂");
            }
            else
            {
                int diasRestantes = (proximoCumple - hoy).Days;
                Console.WriteLine($"Faltan {diasRestantes} días para tu próximo cumpleaños.");
            }

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        // Método para determinar el signo según el día y mes
        static string ObtenerSignoZodiaco(int dia, int mes)
        {
            if ((mes == 3 && dia >= 21) || (mes == 4 && dia <= 19)) return "Aries";
            if ((mes == 4 && dia >= 20) || (mes == 5 && dia <= 20)) return "Tauro";
            if ((mes == 5 && dia >= 21) || (mes == 6 && dia <= 20)) return "Géminis";
            if ((mes == 6 && dia >= 21) || (mes == 7 && dia <= 22)) return "Cáncer";
            if ((mes == 7 && dia >= 23) || (mes == 8 && dia <= 22)) return "Leo";
            if ((mes == 8 && dia >= 23) || (mes == 9 && dia <= 22)) return "Virgo";
            if ((mes == 9 && dia >= 23) || (mes == 10 && dia <= 22)) return "Libra";
            if ((mes == 10 && dia >= 23) || (mes == 11 && dia <= 21)) return "Escorpio";
            if ((mes == 11 && dia >= 22) || (mes == 12 && dia <= 21)) return "Sagitario";
            if ((mes == 12 && dia >= 22) || (mes == 1 && dia <= 19)) return "Capricornio";
            if ((mes == 1 && dia >= 20) || (mes == 2 && dia <= 18)) return "Acuario";
            return "Piscis";
        }
    }
}
