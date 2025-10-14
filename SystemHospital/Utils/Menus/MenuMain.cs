namespace VetPetcare.Utils;

public static class MenuMain
{
    public static void MainMenu()
    {
        bool exit = false;
        try
        {
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===============================================");
                Console.WriteLine("     🏥  Welcome to Hospital San Vicente!  🏥");
                Console.WriteLine("===============================================\n");
                Console.ResetColor();

                Console.WriteLine("Please select an option below:\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  [1] 🧍  Patient Menu");
                Console.WriteLine("  [2] 🩺  Doctors Menu");
                Console.WriteLine("  [3] 📅  Appointment Menu");
                Console.WriteLine("  [4] 🔍  Query Menu");
                Console.WriteLine("  [5] 🚪  Exit");
                Console.ResetColor();

                Console.WriteLine("\n-----------------------------------------------");
                Console.Write("👉  Enter the number of your choice: ");

                string initial = Console.ReadLine();
                switch (initial)
                {
                    case "1":
                    {
                        MenuPatient.ShowPatient();
                        break;
                    }
                    case "2":
                    {
                        MenuDoctor.ShowDoctor();
                        break;
                    }
                    case "3":
                    {
                        MenuMedicalAppointment.ShowMedicalAppointment();
                        break;
                    }
                    case "4":
                    {
                        Query.ListDoctors();
                        break;
                    }
                    case "5":
                    {
                        exit = true;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("\n===================================");
                        Console.WriteLine("Sorry, you did not enter a valid option.");
                        Console.WriteLine("===================================\n");
                        break;
                    }
                }
            } while (!exit);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}