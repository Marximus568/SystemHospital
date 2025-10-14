using VetPetcare.Repository;

namespace VetPetcare.Models
{
    public static class ServicePatient
    {
        private static readonly PatientRepository _repository = new PatientRepository();

        // Method for created new client
        public static Patient? CreatePatient()
        {
            try
            {
                // Ask for first name
                string firstName;
                do
                {
                    Console.WriteLine("Enter your first name:");
                    firstName = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(firstName))
                        Console.WriteLine("First name cannot be empty.");
                } while (string.IsNullOrWhiteSpace(firstName));

                // Ask for last name
                string lastName;
                do
                {
                    Console.WriteLine("Enter your last name:");
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
                    Console.WriteLine("Enter your phone number:");
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
                    Console.WriteLine("Enter your document number:");
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
                        Console.WriteLine("A patient with this document already exists. Please enter a different one.");
                        continue;
                    }

                    break; // valid and unique document
                } while (true);

                // Ask for email
                string email;
                do
                {
                    Console.WriteLine("Enter your email:");
                    email = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                        Console.WriteLine("Invalid email. Please include '@'.");
                } while (string.IsNullOrWhiteSpace(email) || !email.Contains("@"));

                // Ask for address
                string address;
                do
                {
                    Console.WriteLine("Enter your address:");
                    address = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(address))
                        Console.WriteLine("Address cannot be empty.");
                } while (string.IsNullOrWhiteSpace(address));

                // Create new patient
                var newPatient = new Patient(firstName, lastName, dateOfBirth, phone, email, address, document);
                _repository.Create(newPatient);

                Console.WriteLine("\nYour patient has been created successfully!");
                return newPatient;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again.");
                Console.WriteLine($"Error: {e.Message}");
            }

            return null;
        }

        //Method for search by document
        public static string FindPatient(string document)
        {
            try
            {
                // Try to find the patient by their document number
                var patient = _repository.GetByDocument(document);

                // Validate if patient was found
                if (patient == null)
                {
                    Console.WriteLine("\n⚠️ No patient found with that document number.");
                    return "Not found";
                }

                // If found, display the information
                Console.WriteLine("\n✅ Patient found:");
                Console.WriteLine("-----------------------------");
                Console.WriteLine(patient.GetInfo());
                Console.WriteLine("-----------------------------");

                return "Found";
            }
            catch (InvalidOperationException)
            {
                // Handle case where First() throws an exception (no match found)
                Console.WriteLine("\n⚠️ No patient found with that document number.");
                return "Not found";
            }
            catch (Exception e)
            {
                // Handle any unexpected error
                Console.WriteLine($"\n❌ An error occurred while searching for the patient: {e.Message}");
                return "Error";
            }
        }

        // Method for show all client
        public static void ShowList()
        {
            // Retrieve all patients from the repository
            var patients = _repository.GetAll()?.ToList();

            // Validate that the list is not null or empty
            if (patients == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Error: Failed to retrieve patients from the repository.");
                Console.ResetColor();
                return;
            }

            if (!patients.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  No patients are currently registered in the system.");
                Console.ResetColor();
                return;
            }

            // Display header
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== 🏥 Registered Patients ===");
            Console.ResetColor();

            // Display patients in a clean formatted table
            foreach (var patient in patients)
            {
                Console.WriteLine(
                    $"• ID: {patient.Id,-4} | Name: {patient.FirstName} {patient.LastName,-15} | Document: {patient.Document}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Total patients found: {patients.Count}");
            Console.ResetColor();
        }


        //Update to client
        public static void UpdateUser(string id)
        {
            try
            {
                // Ask for current document and fetch patient automatically
                string currentDocument;
                Patient existingPatient;
                do
                {
                    Console.WriteLine("Enter the current document number of the patient you want to update:");
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

                    // Fetch patient automatically from repository
                    existingPatient = _repository.GetByDocument(currentDocument);

                    if (existingPatient == null)
                    {
                        Console.WriteLine("No patient found with this document number. Try again.");
                        continue;
                    }

                    break; // Patient found
                } while (true);

                Console.WriteLine($"Patient found: {existingPatient.FirstName} {existingPatient.LastName}");

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
                string newDocument = existingPatient.Document;
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
                            Console.WriteLine("This document is already registered to another patient.");
                            continue;
                        }

                        newDocument = input;
                        break;
                    } while (true);
                }

                // Collect other updated fields
                Console.WriteLine("Enter first name:");
                string firstName = Console.ReadLine()?.Trim() ?? existingPatient.FirstName;

                Console.WriteLine("Enter last name:");
                string lastName = Console.ReadLine()?.Trim() ?? existingPatient.LastName;

                Console.WriteLine("Enter date of birth (yyyy-mm-dd):");
                DateTime dateOfBirth;
                while (!DateTime.TryParse(Console.ReadLine()?.Trim(), out dateOfBirth))
                    Console.WriteLine("Invalid date. Please try again.");

                Console.WriteLine("Enter phone:");
                string phone = Console.ReadLine()?.Trim() ?? existingPatient.Phone;

                Console.WriteLine("Enter email:");
                string email = Console.ReadLine()?.Trim() ?? existingPatient.Email;

                Console.WriteLine("Enter address:");
                string address = Console.ReadLine()?.Trim() ?? existingPatient.Address;

                // Create updated patient object
                var updatedPatient = new Patient(firstName, lastName, dateOfBirth, phone, email, address, newDocument);

                // Update using repository
                bool success = _repository.Update(updatedPatient, currentDocument);

                if (success)
                    Console.WriteLine("Patient updated successfully.");
                else
                    Console.WriteLine("Error: patient not found or could not be updated.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again.");
                Console.WriteLine($"Error Type: {e.GetType().Name}");
                Console.WriteLine($"Details: {e.Message}");
            }
        }

        // Method for delete client by ID
        public static void DeleteClient(string document)
        {
            // Try to delete the client by document
            var deleted = _repository.DeleteByDocument(document);

            // Validate if deletion was successful
            if (!deleted)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n⚠️  Client with document {document} not found. No records were deleted.");
                Console.ResetColor();
                return;
            }

            // Success message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Client with document {document} was successfully deleted from the system.");
            Console.ResetColor();
        }
    }
}