using VetPetcare.Models;
using VetPetcare.Repository;

public static class ServiceDoctor
{
    private static readonly DoctorRepository _repository = new DoctorRepository();

    // ==========================
    // CREATE VETERINARY
    // ==========================
    public static Doctor? CreateDoctor()
    {
        try
        {
            // Ask for first name
            string firstName;
            do
            {
                Console.WriteLine("Enter doctor's first name:");
                firstName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(firstName))
                    Console.WriteLine("First name cannot be empty.");
            } while (string.IsNullOrWhiteSpace(firstName));

            // Ask for last name
            string lastName;
            do
            {
                Console.WriteLine("Enter doctor's last name:");
                lastName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(lastName))
                    Console.WriteLine("Last name cannot be empty.");
            } while (string.IsNullOrWhiteSpace(lastName));

            // Ask for date of birth
            DateTime dateOfBirth;
            int currentYear = DateTime.Now.Year;
            while (true)
            {
                Console.WriteLine("Enter date of birth (yyyy-mm-dd):");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (!DateTime.TryParse(input, out dateOfBirth))
                {
                    Console.WriteLine("Invalid date format. Please use yyyy-mm-dd.");
                    continue;
                }

                int age = currentYear - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Now)
                    Console.WriteLine("Date of birth cannot be in the future.");
                else if (age > 100)
                    Console.WriteLine("Age cannot be greater than 100 years.");
                else
                    break; // valid date
            }

            // Ask for phone number
            string phone;
            do
            {
                Console.WriteLine("Enter doctor's phone number:");
                phone = Console.ReadLine()?.Trim() ?? "";

                if (!phone.All(char.IsDigit))
                    Console.WriteLine("The phone number must contain only digits.");
                else if (phone.Length < 7 || phone.Length > 15)
                    Console.WriteLine("The phone number must be between 7 and 15 digits long.");
                else
                    break; // valid phone
            } while (true);

            // Ask for document number
            string document;
            do
            {
                Console.WriteLine("Enter doctor's document number:");
                document = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(document))
                {
                    Console.WriteLine("Document number cannot be empty.");
                    continue;
                }

                if (!document.All(char.IsDigit))
                {
                    Console.WriteLine("Document must contain only numeric characters.");
                    continue;
                }

                if (document.Length < 6 || document.Length > 10)
                {
                    Console.WriteLine("Document must be between 6 and 10 digits.");
                    continue;
                }

                // Validate uniqueness through repository
                if (_repository.DocumentExists(document))
                {
                    Console.WriteLine("A doctor with this document already exists. Please enter a different one.");
                    continue;
                }

                break; // valid and unique document
            } while (true);

            // Ask for email
            string email;
            do
            {
                Console.WriteLine("Enter doctor's email:");
                email = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                    Console.WriteLine("Invalid email. Please include '@'.");
            } while (string.IsNullOrWhiteSpace(email) || !email.Contains("@"));

            // Ask for address
            string address;
            do
            {
                Console.WriteLine("Enter doctor's address:");
                address = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(address))
                    Console.WriteLine("Address cannot be empty.");
            } while (string.IsNullOrWhiteSpace(address));

            // Ask for speciality
            string speciality;
            do
            {
                Console.WriteLine("Enter doctor's speciality:");
                speciality = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(speciality))
                    Console.WriteLine("Speciality cannot be empty.");
            } while (string.IsNullOrWhiteSpace(speciality));

            // Create new doctor
            var newDoctor = new Doctor(firstName, lastName, dateOfBirth, phone, email, address, speciality, document);
            _repository.Create(newDoctor);

            Console.WriteLine("\n✅ Doctor created successfully!");
            Console.WriteLine($"Doctor ID: {newDoctor.DoctorId}");

            return newDoctor;
        }
        catch (Exception e)
        {
            Console.WriteLine("❌ An error occurred while creating the doctor.");
            Console.WriteLine($"Error details: {e.Message}");
        }

        return null;
    }


    // ==========================
    // GET BY ID
    // ==========================
    public static void FindDocument(string document)
    {
        var doctor = _repository.GetByDocument(document);
        if (doctor == null)
        {
            Console.WriteLine("Doctor not found.");
            return;
        }

        Console.WriteLine("\nDoctor found:");
        Console.WriteLine($"ID: {doctor.DoctorId}");
        Console.WriteLine($"Name: {doctor.FirstName} {doctor.LastName}");
        Console.WriteLine($"Date of Birth: {doctor.DateOfBirth}");
        Console.WriteLine($"Gender: {doctor.Phone}");
        Console.WriteLine($"Email: {doctor.Email}");
        Console.WriteLine($"Address: {doctor.Address}");
        Console.WriteLine($"Speciality: {doctor.speciality}");
    }

    // ==========================
    // GET ALL
    // ==========================
    public static void ShowList()
    {
        var veterinaries = _repository.GetAll().ToList();

        if (veterinaries.Count == 0)
        {
            Console.WriteLine("No doctors registered.");
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

    public static void UpdateDoctor(string document)
    {
        try
        {
            // Ask for current document and fetch doctor automatically
            string currentDocument;
            Doctor existingDoctor;

            do
            {
                Console.WriteLine("Enter the current document number of the doctor you want to update:(It is the same number that you did use)");
                currentDocument = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(currentDocument))
                {
                    Console.WriteLine("Document cannot be empty.");
                    continue;
                }

                if (!currentDocument.All(char.IsDigit))
                {
                    Console.WriteLine("Document must contain only numeric characters.");
                    continue;
                }

                // Fetch doctor automatically from repository
                existingDoctor = _repository.GetByDocument(currentDocument);

                if (existingDoctor == null)
                {
                    Console.WriteLine("No doctor found with this document number. Try again.");
                    continue;
                }

                break; // Doctor found
            } while (true);

            Console.WriteLine($"Doctor found: {existingDoctor.FirstName} {existingDoctor.LastName}");

            // Ask if user wants to update document
            bool wantsToChangeDoc = false;
            while (true)
            {
                Console.WriteLine("Do you want to update the document number? (Y/N)");
                string response = Console.ReadLine()?.Trim().ToUpper();

                if (response == "Y")
                {
                    wantsToChangeDoc = true;
                    break;
                }
                else if (response == "N")
                {
                    wantsToChangeDoc = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter 'Y' or 'N'.");
                }
            }

            // If the user wants to change the document, validate uniqueness
            string newDocument = existingDoctor.Document;
            if (wantsToChangeDoc)
            {
                do
                {
                    Console.WriteLine("Enter the new document number:");
                    string input = Console.ReadLine()?.Trim() ?? "";

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Document cannot be empty.");
                        continue;
                    }

                    if (!input.All(char.IsDigit))
                    {
                        Console.WriteLine("Document must contain only numeric characters.");
                        continue;
                    }

                    if (input.Length < 6 || input.Length > 10)
                    {
                        Console.WriteLine("Document must be between 6 and 10 digits.");
                        continue;
                    }

                    if (_repository.DocumentExists(input))
                    {
                        Console.WriteLine("This document is already registered to another doctor.");
                        continue;
                    }

                    newDocument = input;
                    break;
                } while (true);
            }

            // Collect updated fields
            Console.WriteLine("Enter new first name (leave empty to keep current):");
            string firstName = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(firstName)) firstName = existingDoctor.FirstName;

            Console.WriteLine("Enter new last name (leave empty to keep current):");
            string lastName = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(lastName)) lastName = existingDoctor.LastName;

            DateTime dateOfBirth;
            while (true)
            {
                Console.WriteLine("Enter new date of birth (yyyy-mm-dd) or leave empty to keep current:");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    dateOfBirth = existingDoctor.DateOfBirth;
                    break;
                }

                if (DateTime.TryParse(input, out dateOfBirth))
                    break;

                Console.WriteLine("Invalid date format. Try again.");
            }

            string phone;
            do
            {
                Console.WriteLine("Enter new phone number (leave empty to keep current):");
                phone = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(phone))
                {
                    phone = existingDoctor.Phone;
                    break;
                }

                if (!phone.All(char.IsDigit))
                {
                    Console.WriteLine("Phone number must contain only numeric characters.");
                    continue;
                }

                if (phone.Length < 7 || phone.Length > 15)
                {
                    Console.WriteLine("Phone number must be between 7 and 15 digits.");
                    continue;
                }

                break;
            } while (true);

            string email;
            do
            {
                Console.WriteLine("Enter new email (leave empty to keep current):");
                email = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(email))
                {
                    email = existingDoctor.Email;
                    break;
                }

                if (!email.Contains("@") || !email.Contains("."))
                {
                    Console.WriteLine("Invalid email format.");
                    continue;
                }

                break;
            } while (true);

            Console.WriteLine("Enter new address (leave empty to keep current):");
            string address = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(address)) address = existingDoctor.Address;

            Console.WriteLine("Enter new speciality (leave empty to keep current):");
            string speciality = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(speciality)) speciality = existingDoctor.speciality;

            // Create updated object
            var updatedDoctor = new Doctor(firstName, lastName, dateOfBirth, phone, email, address, speciality,
                newDocument);

            // Update repository
            bool success = _repository.Update(updatedDoctor, currentDocument);

            if (success)
                Console.WriteLine("Doctor updated successfully.");
            else
                Console.WriteLine("Error: doctor not found or could not be updated.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong while updating the doctor.");
            Console.WriteLine($"Error Type: {e.GetType().Name}");
            Console.WriteLine($"Details: {e.Message}");
        }
    }


// ==========================
// DELETE
// ==========================
    public static void DeleteDoctor(string document)
    {
        var deleted = _repository.DeleteByDocument(document);

        if (!deleted)
        {
            Console.WriteLine("Veterinary not found. Nothing was deleted.");
            return;
        }

        Console.WriteLine("Veterinary deleted successfully.");
    }
}