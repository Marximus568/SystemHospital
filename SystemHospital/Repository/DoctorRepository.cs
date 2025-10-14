
using VetPetcare.Models;

namespace VetPetcare.Repository;

public class DoctorRepository : IDoctorRepository
{
    public Doctor Create(Doctor doctor)
    {
        Database.Database.Doctors.Add(doctor);
        return doctor;
    }

    public Doctor GetById(int document)
    {
        return Database.Database.Doctors.First(v => v.DoctorId == document);
    }

    public IEnumerable<Doctor> GetAll()
    {
        return Database.Database.Doctors;
    }

    public bool Update(Doctor doctor, int id)
    {
        int index = Database.Database.Doctors.FindIndex(v => v.DoctorId == id);

        if (index == -1)
        {
            Console.WriteLine("Veterinary not found.");
            return false;
        }

        doctor.DoctorId = id;
        Database.Database.Doctors[index] = doctor;

        return true;
    }

    public bool DeleteById(int document)
    {
        var veterinary = Database.Database.Doctors.FirstOrDefault(v => v.DoctorId == document);

        if (veterinary == null)
        {
            Console.WriteLine("Veterinary not found.");
            return false;
        }

        Database.Database.Doctors.Remove(veterinary);
        return true;
    }
}
