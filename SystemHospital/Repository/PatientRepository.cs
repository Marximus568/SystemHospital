
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

    public Patient GetByDocument(int document)
    {
        return Database.Database.Patients.First(c => c.PatientId == document);
    }

    public IEnumerable<Patient> GetAll()
    {
        return Database.Database.Patients;
    }

    public bool Update(Patient patient, int id)
    {
        int index = Database.Database.Patients.FindIndex(c => c.PatientId == id);

        if (index == -1)
        {
            Console.WriteLine("Client not found.");
            return false;
        }
        patient.PatientId = id;
        Database.Database.Patients[index] = patient;

        return true;
    }


    public bool DeleteById(int document)
    {
        var client = Database.Database.Patients.First(c => c.PatientId == document);
        if (client == null) return false;
        Database.Database.Patients.Remove(client);
        return true;
    }
    
}