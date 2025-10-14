using VetPetcare.Models;

namespace VetPetcare.Utils;

public static class MenuMedicalAppointment
{
    public static void ShowMedicalAppointment()
    {
        var control = true;
        do
        {
            Console.WriteLine("==========================");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Schedule appointment.");
            Console.WriteLine("2. Show all appointments.");
            Console.WriteLine("3. Find appointment.");
            Console.WriteLine("4. Update appointment.");
            Console.WriteLine("5. Delete appointment.");
            Console.WriteLine("6. Leave.");
            Console.WriteLine("==========================");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                {
                    ServiceMedicalAppointment.ScheduleAppointment();
                    break;
                }

                case "2":
                {
                    ServiceMedicalAppointment.ShowAllAppointments();
                    break;
                }

                case "3":
                {
                    Console.WriteLine("Write an id:");
                    if (int.TryParse(Console.ReadLine(), out int id))
                        ServiceMedicalAppointment.ShowAppointmentById(id);
                    else
                        Console.WriteLine("Invalid ID. Try again.");
                    break;
                }

                case "4":
                {
                    Console.WriteLine("Write an id:");
                    if (int.TryParse(Console.ReadLine(), out int id))
                        ServiceMedicalAppointment.UpdateAppointment(id);
                    else
                        Console.WriteLine("Invalid ID. Try again.");
                    break;
                }

                case "5":
                {
                    Console.WriteLine("Write an id:");
                    if (int.TryParse(Console.ReadLine(), out int id))
                        ServiceMedicalAppointment.DeleteAppointment(id);
                    else
                        Console.WriteLine("Invalid ID. Try again.");
                    break;
                }

                case "6":
                {
                    Console.WriteLine("You will return page past.");
                    control = false;
                    return;
                }

                default:
                {
                    Console.WriteLine("\n===================================");
                    Console.WriteLine("Sorry, you did not enter a valid option.");
                    Console.WriteLine("===================================\n");
                    break;
                }
            }
        } while (control);
    }
}
