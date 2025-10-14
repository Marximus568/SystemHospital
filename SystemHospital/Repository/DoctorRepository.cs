using VetPetcare.Models;

namespace VetPetcare.Repository;

public class DoctorRepository : IDoctorRepository
{
    public Doctor Create(Doctor doctor)
    {
        Database.Database.Doctors.Add(doctor);
        return doctor;
    }

    public Doctor GetByDocument(string document)
    {
        // Search doctor by their document number safely
        return Database.Database.Doctors.FirstOrDefault(d => d.Document == document);
    }


    public IEnumerable<Doctor> GetAll()
    {
        return Database.Database.Doctors;
    }

    public bool Update(Doctor doctor, string document)
    {
        // Search for the index of the doctor with the given document
        int index = Database.Database.Doctors.FindIndex(d => d.Document.Equals(document, StringComparison.OrdinalIgnoreCase));

        // Validate if the doctor exists
        if (index == -1)
        {
            Console.WriteLine("Doctor not found.");
            return false;
        }

        // Ensure the document remains unchanged (prevent accidental overwrite)
        doctor.Document = Database.Database.Doctors[index].Document;

        // Update the existing record with the new data
        Database.Database.Doctors[index] = doctor;

        Console.WriteLine($"Doctor with document '{document}' was successfully updated.");
        return true;
    }



    public bool DeleteByDocument(string document)
    {
        // Search for the doctor with the given document
        var doctor = Database.Database.Doctors
            .FirstOrDefault(d => d.Document.Equals(document, StringComparison.OrdinalIgnoreCase));

        // Validate if the doctor exists
        if (doctor == null)
        {
            Console.WriteLine("Doctor not found.");
            return false;
        }

        // Remove the doctor from the database
        Database.Database.Doctors.Remove(doctor);

        Console.WriteLine($"Doctor with document '{document}' was successfully deleted.");
        return true;
    }
    public bool DocumentExists(string document)
    {
        return Database.Database.Doctors.Any(p => p.Document == document);
    }
}