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
                string firstName;
                do
                {
                    Console.WriteLine("Enter your first name:");
                    firstName = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(firstName)) Console.WriteLine("First name cannot be empty.");
                } while (string.IsNullOrWhiteSpace(firstName));

                string lastName;
                do
                {
                    Console.WriteLine("Enter your last name:");
                    lastName = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(lastName)) Console.WriteLine("Last name cannot be empty.");
                } while (string.IsNullOrWhiteSpace(lastName));

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
                    {
                        Console.WriteLine("Date of birth cannot be in the future.");
                    }
                    else if (age > 100)
                    {
                        Console.WriteLine("Age cannot be greater than 100 years.");
                    }
                    else
                    {
                        // Valid date
                        break;
                    }
                }

                string phone;
                do
                {
                    Console.WriteLine("Enter your phone number:");
                    phone = Console.ReadLine()?.Trim() ?? "";

                    // Check if the input contains only digits and has a valid length
                    if (!phone.All(char.IsDigit))
                    {
                        Console.WriteLine("The phone number must contain only digits.");
                    }
                    else if (phone.Length < 7 || phone.Length > 15)
                    {
                        Console.WriteLine("The phone number must be between 7 and 15 digits long.");
                    }
                    else
                    {
                        // Valid phone number
                        break;
                    }
                } while (true);


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

                string email;
                do
                {
                    Console.WriteLine("Enter your email:");
                    email = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                        Console.WriteLine("Invalid email. Please include '@'.");
                } while (string.IsNullOrWhiteSpace(email) || !email.Contains("@"));

                string address;
                do
                {
                    Console.WriteLine("Enter your address:");
                    address = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(address)) Console.WriteLine("Address cannot be empty.");
                } while
                    (string.IsNullOrWhiteSpace(address)); // Crear y guardar cliente

                // Validate that the document does not already exist
                var existingPatient = _repository.GetAll()
                    .FirstOrDefault(p => p.Document == document);

                if (existingPatient != null)
                {
                    Console.WriteLine("\nA patient with this document already exists. Please enter a different one.");
                    return null;
                }

                // Create a new patient if the document is unique
                var newPatient = new Patient(firstName, lastName, dateOfBirth, phone, email, address, document);
                _repository.Create(newPatient);

                Console.WriteLine("\nYour patient has been created successfully!");
                return newPatient;
            }
            catch (Exception e)
            {
                Console.WriteLine(" Something went wrong. Try again.");
                Console.WriteLine(e.Message);
            }

            return null;
        }

        //Method for search by ID
        public static string FindPatient(int document)
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
        public static void UpdateUser(int id)
        {
            try
            {
                string firstName;
                do
                {
                    Console.WriteLine("Enter your first name:");
                    firstName = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(firstName)) Console.WriteLine("First name cannot be empty.");
                } while (string.IsNullOrWhiteSpace(firstName));

                string lastName;
                do
                {
                    Console.WriteLine("Enter your last name:");
                    lastName = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(lastName)) Console.WriteLine("Last name cannot be empty.");
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
                int currentYear = DateTime.Now.Year;

                while (true)
                {
                    Console.WriteLine("Enter your date of birth (yyyy-mm-dd):");
                    string input = Console.ReadLine()?.Trim() ?? "";

                    if (!DateTime.TryParse(input, out dateOfBirth))
                    {
                        Console.WriteLine("Invalid date format. Please use yyyy-mm-dd.");
                        continue;
                    }

                    int age = currentYear - dateOfBirth.Year;

                    if (dateOfBirth > DateTime.Now)
                    {
                        Console.WriteLine("Date of birth cannot be in the future.");
                    }
                    else if (age > 100)
                    {
                        Console.WriteLine("Age cannot be greater than 100 years.");
                    }
                    else
                    {
                        break;
                    }
                }

                string phone;
                do
                {
                    Console.WriteLine("Enter your phone number:");
                    phone = Console.ReadLine()?.Trim() ?? "";

                    // Check if the input contains only digits and has a valid length
                    if (!phone.All(char.IsDigit))
                    {
                        Console.WriteLine("The phone number must contain only digits.");
                    }
                    else if (phone.Length < 7 || phone.Length > 15)
                    {
                        Console.WriteLine("The phone number must be between 7 and 15 digits long.");
                    }
                    else
                    {
                        // Valid phone number
                        break;
                    }
                } while (true);


                string email;
                do
                {
                    Console.WriteLine("Enter your email:");
                    email = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                        Console.WriteLine("Invalid email. Please include '@'.");
                } while (string.IsNullOrWhiteSpace(email) || !email.Contains("@"));

                string address;
                do
                {
                    Console.WriteLine("Enter your address:");
                    address = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(address)) Console.WriteLine("Address cannot be empty.");
                } while
                    (string.IsNullOrWhiteSpace(address));

                var TemporalClient = new Patient(firstName, lastName, dateOfBirth, phone, email, address, document);
                var success = _repository.Update(TemporalClient, id);
                if (success)
                    Console.WriteLine("Client updated successfully.");
                else
                    Console.WriteLine("Error: client not found or could not be updated.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again.");
                Console.WriteLine($"Error Type: {e.GetType().Name}");
                Console.WriteLine($"Details: {e.Message}");
                Console.WriteLine($"StackTrace: {e.StackTrace}");
            }
        }


        // Method for delete client by ID
        public static void DeleteClient(int id)
        {
            // Try to delete the client by ID
            var deleted = _repository.DeleteById(id);

            // Validate if deletion was successful
            if (!deleted)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  Client not found. No records were deleted.");
                Console.ResetColor();
                return;
            }

            // Success message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Client with ID {id} was successfully deleted from the system.");
            Console.ResetColor();
        }
    }
}