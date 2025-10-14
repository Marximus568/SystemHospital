using VetPetcare.Repository;

public static class Query
{
    private static readonly DoctorRepository _repository = new DoctorRepository();

    public static void ListDoctors()
    {
        try
        {
            // Get all doctors from the repository
            var doctors = _repository.GetAll().ToList();

            if (!doctors.Any())
            {
                Console.WriteLine("No doctors registered in the system.");
                return;
            }

            // Get unique specialities from the registered doctors
            var uniqueSpecialities = doctors
                .Select(d => d.speciality)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Display unique specialities
            Console.WriteLine("\n=== Registered Specialities ===");
            for (int i = 0; i < uniqueSpecialities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {uniqueSpecialities[i]}");
            }

            // Ask user to select a speciality
            string chosenSpeciality = null;
            while (true)
            {
                Console.WriteLine("\nEnter the number of the speciality you want to see (or press Enter to see all):");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    // User pressed Enter: show all doctors
                    break;
                }

                if (int.TryParse(input, out int specialityIndex) &&
                    specialityIndex >= 1 && specialityIndex <= uniqueSpecialities.Count)
                {
                    chosenSpeciality = uniqueSpecialities[specialityIndex - 1];
                    break;
                }

                Console.WriteLine("Invalid option. Please enter a valid number.");
            }

            // Filter doctors by selected speciality if applicable
            var filteredDoctors = string.IsNullOrEmpty(chosenSpeciality)
                ? doctors
                : doctors.Where(d => d.speciality.Equals(chosenSpeciality, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            // Display doctors
            Console.WriteLine("\n=== List of Registered Doctors ===");
            foreach (var doc in filteredDoctors)
            {
                Console.WriteLine($"Name: {doc.FirstName} {doc.LastName}");
                Console.WriteLine($"Document: {doc.Document}");
                Console.WriteLine($"Speciality: {doc.speciality}");
                Console.WriteLine($"Phone: {doc.Phone}");
                Console.WriteLine($"Email: {doc.Email}");
                Console.WriteLine($"Address: {doc.Address}");
                Console.WriteLine($"Date of Birth: {doc.DateOfBirth:yyyy-MM-dd}");
                Console.WriteLine("--------------------------------");
            }

            // Wait for user to press Enter before continuing
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error listing doctors: {e.Message}");
        }
    }
}
