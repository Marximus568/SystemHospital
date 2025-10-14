
using System.Xml;
using VetPetcare.Models;

namespace VetPetcare.Repository;

public class PatientRepository : IPatientRepository
{
    public Patient Create(Patient patient)
    {
        Database.Database.Patients.Add(patient);
        return patient;
    }

    public Patient GetByDocument(string document)
    {
        return Database.Database.Patients
            .FirstOrDefault(p => p.Document.Equals(document, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Patient> GetAll()
    {
        return Database.Database.Patients;
    }

    public bool Update(Patient patient, string document)
    {
        // Find the index of the patient using the document number
        int index = Database.Database.Patients
            .FindIndex(c => c.Document.Equals(document, StringComparison.OrdinalIgnoreCase));

        // If not found, show message and return false
        if (index == -1)
        {
            Console.WriteLine("Patient not found.");
            return false;
        }

        // Keep the same document to maintain consistency
        patient.Document = document;

        // Replace the existing record with the new data
        Database.Database.Patients[index] = patient;

        Console.WriteLine($"Patient with document '{document}' was successfully updated.");
        return true;
    }



    public bool DeleteByDocument(string document)
    {
        // Search for the patient with the given document
        var patient = Database.Database.Patients
            .FirstOrDefault(c => c.Document.Equals(document, StringComparison.OrdinalIgnoreCase));

        // Validate if the patient exists
        if (patient == null)
        {
            Console.WriteLine("Patient not found.");
            return false;
        }

        // Remove the patient from the database
        Database.Database.Patients.Remove(patient);

        Console.WriteLine($"Patient with document '{document}' was successfully deleted.");
        return true;
    }
    public bool DocumentExists(string document)
    {
        return Database.Database.Patients.Any(p => p.Document == document);
    }
}