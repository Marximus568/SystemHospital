namespace VetPetcare.Utils;

public static class MenuDoctor
{
    public static void ShowDoctor()
    {
        bool control = true;

        do
        {
            Console.WriteLine("==========================");
            Console.WriteLine("   Doctor Management");
            Console.WriteLine("==========================");
            Console.WriteLine("1. Register a doctor.");
            Console.WriteLine("2. Show all doctors.");
            Console.WriteLine("3. Find a doctor by document.");
            Console.WriteLine("4. Update a doctor.");
            Console.WriteLine("5. Delete a doctor.");
            Console.WriteLine("6. Leave.");
            Console.WriteLine("==========================");
            Console.Write("Choose an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                {
                    ServiceDoctor.CreateDoctor();
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
                        string document = Console.ReadLine()?.Trim() ?? "";

                        if (!string.IsNullOrEmpty(document))
                        {
                            ServiceDoctor.FindDocument(document);
                        }
                        else
                        {
                            Console.WriteLine("Document cannot be empty.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occurred while searching the doctor.");
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;
                }

                case "4":
                {
                    try
                    {
                        Console.Write("Enter doctor document to update: ");
                        string document = Console.ReadLine()?.Trim() ?? "";

                        if (!string.IsNullOrEmpty(document))
                        {
                            ServiceDoctor.UpdateDoctor(document);
                        }
                        else
                        {
                            Console.WriteLine("Document cannot be empty.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occurred while updating the doctor.");
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;

                    
                }

                case "5":
                {
                    try
                    {
                        Console.Write("Enter doctor document to delete: ");
                        string document = Console.ReadLine()?.Trim() ?? "";

                        if (!string.IsNullOrEmpty(document))
                        {
                            ServiceDoctor.DeleteDoctor(document);
                        }
                        else
                        {
                            Console.WriteLine("Document cannot be empty.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occurred while deleting the doctor.");
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;

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