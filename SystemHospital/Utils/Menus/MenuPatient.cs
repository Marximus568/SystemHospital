using VetPetcare.Models;


public static class MenuPatient
{
    public static void ShowPatient()
    {
        var control = true;
        do
        {
            Console.WriteLine("==========================");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1 Register for patient.");
            Console.WriteLine("2 Show patients.");
            Console.WriteLine("3 Find patient.");
            Console.WriteLine("4 Update patient.");
            Console.WriteLine("5 Delete patient.");
            Console.WriteLine("6. Leave.");
            var options = Console.ReadLine();
            Console.WriteLine("==========================");

            switch (options)
            {
                case "1":
                {
                    ServicePatient.CreatePatient();
                    break;
                }
                case "2":
                {
                    ServicePatient.ShowList();
                    break;
                }

                case "3":
                {
                    {
                        try
                        {
                            Console.Write("Enter patient document: ");
                            string document = Console.ReadLine()?.Trim() ?? "";

                            if (string.IsNullOrEmpty(document))
                            {
                                Console.WriteLine("Document cannot be empty.");
                            }
                            else if (!document.All(char.IsDigit))
                            {
                                Console.WriteLine("Document must contain only numeric characters.");
                            }
                            else
                            {
                                ServicePatient.FindPatient(document);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An error occurred while searching for the patient.");
                            Console.WriteLine($"Error: {e.Message}");
                        }
                        break;
                    }
                }

                case "4":
                {
                    {
                        try
                        {
                            Console.Write("Enter patient document to update: ");
                            string input = Console.ReadLine()?.Trim() ?? "";

                            if (string.IsNullOrEmpty(input))
                            {
                                Console.WriteLine("Document cannot be empty.");
                            }
                            else if (!input.All(char.IsDigit))
                            {
                                Console.WriteLine("Document must contain only numeric characters.");
                            }
                            else
                            {
                                // If it passes all validations, proceed to update
                                ServicePatient.UpdateUser(input);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An error occurred while trying to update the patient.");
                            Console.WriteLine($"Error: {e.Message}");
                        }
                        break;
                    }
                }
                case "5":
                {
                    {
                        try
                        {
                            Console.Write("Enter patient document to delete: ");
                            string input = Console.ReadLine()?.Trim() ?? "";

                            if (string.IsNullOrEmpty(input))
                            {
                                Console.WriteLine("Document cannot be empty.");
                            }
                            else if (!input.All(char.IsDigit))
                            {
                                Console.WriteLine("Document must contain only numeric characters.");
                            }
                            else
                            {
                                // Call the service if input is valid
                                ServicePatient.DeleteClient(input);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An error occurred while trying to delete the patient.");
                            Console.WriteLine($"Error: {e.Message}");
                        }
                        break;

                    }
                }
                case "6":
                {
                    Console.WriteLine("You will return page past.");
                    control = false;
                    return;
                }
                default:
                {
                    Console.WriteLine("Invalid option. Try again.");
                    break;
                }
            }
        } while (control);
    }
}
