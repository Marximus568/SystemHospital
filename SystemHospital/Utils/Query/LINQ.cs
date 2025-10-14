using VetPetcare.Repository;

public static class Query
{
    public static void ListDoctors(string specialityFilter = null)
    {
        // Private repository instance
        var _repository = new DoctorRepository();

        try
        {
            // Obtener todos los doctores desde el repositorio
            var doctors = _repository.GetAll().ToList();

            if (!doctors.Any())
            {
                Console.WriteLine("No doctors registered in the system.");
                return;
            }

            // Filtrar por especialidad si se proporciona
            if (!string.IsNullOrWhiteSpace(specialityFilter))
            {
                doctors = doctors
                    .Where(d => d.speciality.Equals(specialityFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (!doctors.Any())
                {
                    Console.WriteLine($"No doctors found with speciality '{specialityFilter}'.");
                    return;
                }
            }

            // Mostrar lista de doctores
            Console.WriteLine("\n=== List of Registered Doctors ===");
            foreach (var doc in doctors)
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
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error listing doctors: {e.Message}");
        }
    }
}
