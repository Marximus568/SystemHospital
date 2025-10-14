namespace VetPetcare.Utils;

public static class MenuDoctor
{
    public static void ShowVeterinary()
    {
        bool control = true;

        do
        {
            Console.WriteLine("==========================");
            Console.WriteLine("   Veterinary Management");
            Console.WriteLine("==========================");
            Console.WriteLine("1. Register a veterinary.");
            Console.WriteLine("2. Show all veterinaries.");
            Console.WriteLine("3. Find a veterinary by ID.");
            Console.WriteLine("4. Update a veterinary.");
            Console.WriteLine("5. Delete a veterinary.");
            Console.WriteLine("6. Leave.");
            Console.WriteLine("==========================");
            Console.Write("Choose an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                {
                    ServiceDoctor.CreateVeterinary();
                    break;
                }

                case "2":
                {
                    ServiceDoctor.ShowList();
                    break;
                }

                case "3":
                {
                    try
                    {
                        Console.Write("Enter doctor document: ");
                        if (int.TryParse(Console.ReadLine(), out int document))
                            ServiceDoctor.FindVeterinary(document);
                        else
                            Console.WriteLine("Invalid document. Please enter a number.");
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                case "4":
                {
                    try
                    {
                        Console.Write("Enter patient document to update: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                            ServiceDoctor.UpdateVeterinary(id);
                        else
                            Console.WriteLine("Invalid document. Please enter a number.");
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    
                }

                case "5":
                {
                    try
                    {
                        Console.Write("Enter veterinary ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                            ServiceDoctor.DeleteVeterinary(id);
                        else
                            Console.WriteLine("Invalid ID. Please enter a number.");
                        break;
                    
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                case "6":
                {
                    Console.WriteLine("Returning to previous menu...");
                    control = false;
                    break;
                }

                default:
                {
                    Console.WriteLine("Invalid option. Try again.");
                    break;
                }
            }

            if (control)
            {
                Console.WriteLine("\nPress ENTER to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        } while (control);
    }
}