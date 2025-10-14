using VetPetcare.Models;


public static class MenuPatient
{
    public static void ShowClient()
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
                            Console.WriteLine("Write a document");
                            int document = int.Parse(Console.ReadLine());
                            ServicePatient.FindPatient(document);
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }

                case "4":
                {
                    {
                        try
                        {
                            Console.WriteLine("Write a document");
                            int document = int.Parse(Console.ReadLine());
                            ServicePatient.UpdateUser(document);
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }
                case "5":
                {
                    {
                        try
                        {
                            Console.WriteLine("Write a document");
                            int document = int.Parse(Console.ReadLine());
                            ServicePatient.DeleteClient(document);
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
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
