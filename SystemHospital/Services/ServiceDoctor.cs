using VetPetcare.Models;
using VetPetcare.Repository;

public static class ServiceDoctor
{
    private static readonly DoctorRepository _repository = new DoctorRepository();

    // ==========================
    // CREATE VETERINARY
    // ==========================
    public static Doctor? CreateVeterinary()
    {
        try
        {
            string firstName;
            do
            {
                Console.WriteLine("Enter the veterinary's first name:");
                firstName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(firstName))
                    Console.WriteLine("First name cannot be empty.");
            } while (string.IsNullOrWhiteSpace(firstName));

            string lastName;
            do
            {
                Console.WriteLine("Enter the veterinary's last name:");
                lastName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(lastName))
                    Console.WriteLine("Last name cannot be empty.");
            } while (string.IsNullOrWhiteSpace(lastName));
            int document;
            do
            {
                Console.WriteLine("Enter your document number:");

                string input = Console.ReadLine()?.Trim();

                // Validate that input is not empty
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Document number cannot be empty.");
                    continue;
                }

                // Validate that input is a number
                if (!int.TryParse(input, out document))
                {
                    Console.WriteLine("Document must contain only numeric characters.");
                    continue;
                }

                // Validate length (between 6 and 10 digits)
                if (input.Length < 6 || input.Length > 10)
                {
                    Console.WriteLine("Document must be between 6 and 10 digits.");
                    continue;
                }

                // If all validations pass, exit loop
                break;

            } while (true);

            DateTime dateOfBirth;
            while (true)
            {
                Console.WriteLine("Enter the veterinary's date of birth (yyyy-mm-dd):");
                if (DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
                    break;
                Console.WriteLine("Invalid date format. Please use yyyy-mm-dd.");
            }

            string gender;
            do
            {
                Console.WriteLine("Enter the veterinary's gender (M/F):");
                gender = Console.ReadLine()?.Trim().ToUpper();
                if (gender != "M" && gender != "F")
                    Console.WriteLine("Please enter 'M' for male or 'F' for female.");
            } while (gender != "M" && gender != "F");

            string email;
            do
            {
                Console.WriteLine("Enter the veterinary's email:");
                email = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                    Console.WriteLine("Invalid email. Please include '@'.");
            } while (string.IsNullOrWhiteSpace(email) || !email.Contains("@"));

            string address;
            do
            {
                Console.WriteLine("Enter the veterinary's address:");
                address = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(address))
                    Console.WriteLine("Address cannot be empty.");
            } while (string.IsNullOrWhiteSpace(address));

            string speciality;
            do
            {
                Console.WriteLine("Enter the veterinary's speciality:");
                speciality = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(speciality))
                    Console.WriteLine("Speciality cannot be empty.");
            } while (string.IsNullOrWhiteSpace(speciality));
            

            var newVeterinary = new Doctor(firstName, lastName, dateOfBirth, gender, email, address, speciality,document);
            _repository.Create(newVeterinary);

            Console.WriteLine("\nVeterinary created successfully!");
            Console.WriteLine($"Veterinary ID: {newVeterinary.DoctorId}");

            return newVeterinary;
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while creating the veterinary.");
            Console.WriteLine(e.Message);
        }

        return null;
    }

    // ==========================
    // GET BY ID
    // ==========================
    public static void FindVeterinary(int id)
    {
        var veterinary = _repository.GetById(id);
        if (veterinary == null)
        {
            Console.WriteLine("Veterinary not found.");
            return;
        }

        Console.WriteLine("\nVeterinary found:");
        Console.WriteLine($"ID: {veterinary.DoctorId}");
        Console.WriteLine($"Name: {veterinary.FirstName} {veterinary.LastName}");
        Console.WriteLine($"Date of Birth: {veterinary.DateOfBirth}");
        Console.WriteLine($"Gender: {veterinary.Phone}");
        Console.WriteLine($"Email: {veterinary.Email}");
        Console.WriteLine($"Address: {veterinary.Address}");
        Console.WriteLine($"Speciality: {veterinary.speciality}");
    }

    // ==========================
    // GET ALL
    // ==========================
    public static void ShowList()
    {
        var veterinaries = _repository.GetAll().ToList();

        if (veterinaries.Count == 0)
        {
            Console.WriteLine("No veterinaries registered.");
            return;
        }

        foreach (var v in veterinaries)
        {
            Console.WriteLine(v.GetInfo());
            Console.WriteLine($"Speciality: {v.speciality}");
            Console.WriteLine(new string('-', 40));
        }
    }


    // ==========================
    // UPDATE
    // ==========================
    public static void UpdateVeterinary(int id)
    {
        try
        {
            string firstName;
            do
            {
                Console.WriteLine("Enter new first name:");
                firstName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(firstName))
                    Console.WriteLine("First name cannot be empty.");
            } while (string.IsNullOrWhiteSpace(firstName));

            string lastName;
            do
            {
                Console.WriteLine("Enter new last name:");
                lastName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(lastName))
                    Console.WriteLine("Last name cannot be empty.");
            } while (string.IsNullOrWhiteSpace(lastName));
            int document;
            do
            {
                Console.WriteLine("Enter your document number:");

                string input = Console.ReadLine()?.Trim();

                // Validate that input is not empty
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Document number cannot be empty.");
                    continue;
                }

                // Validate that input is a number
                if (!int.TryParse(input, out document))
                {
                    Console.WriteLine("Document must contain only numeric characters.");
                    continue;
                }

                // Validate length (between 6 and 10 digits)
                if (input.Length < 6 || input.Length > 10)
                {
                    Console.WriteLine("Document must be between 6 and 10 digits.");
                    continue;
                }

                // If all validations pass, exit loop
                break;

            } while (true);

            DateTime dateOfBirth;
            while (true)
            {
                Console.WriteLine("Enter new date of birth (yyyy-mm-dd):");
                if (DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
                    break;
                Console.WriteLine("Invalid date format. Try again.");
            }

            string gender;
            do
            {
                Console.WriteLine("Enter new gender (M/F):");
                gender = Console.ReadLine()?.Trim().ToUpper();
                if (gender != "M" && gender != "F")
                    Console.WriteLine("Please enter 'M' or 'F'.");
            } while (gender != "M" && gender != "F");

            string email;
            do
            {
                Console.WriteLine("Enter new email:");
                email = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                    Console.WriteLine("Invalid email format.");
            } while (string.IsNullOrWhiteSpace(email) || !email.Contains("@"));

            string address;
            do
            {
                Console.WriteLine("Enter new address:");
                address = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(address))
                    Console.WriteLine("Address cannot be empty.");
            } while (string.IsNullOrWhiteSpace(address));

            string speciality;
            do
            {
                Console.WriteLine("Enter new speciality:");
                speciality = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(speciality))
                    Console.WriteLine("Speciality cannot be empty.");
            } while (string.IsNullOrWhiteSpace(speciality));

            var tempVeterinary = new Doctor(firstName, lastName, dateOfBirth, gender, email, address, speciality,document);
            var success = _repository.Update(tempVeterinary, id);

            if (success)
                Console.WriteLine("Veterinary updated successfully.");
            else
                Console.WriteLine("Error: veterinary not found or could not be updated.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong while updating the veterinary.");
            Console.WriteLine($"Error Type: {e.GetType().Name}");
            Console.WriteLine($"Details: {e.Message}");
        }
    }

    // ==========================
    // DELETE
    // ==========================
    public static void DeleteVeterinary(int document)
    {
        var deleted = _repository.DeleteById(document);

        if (!deleted)
        {
            Console.WriteLine("Veterinary not found. Nothing was deleted.");
            return;
        }

        Console.WriteLine("Veterinary deleted successfully.");
    }
}
