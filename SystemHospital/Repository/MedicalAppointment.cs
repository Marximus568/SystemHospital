using VetPetcare.Interface;
using VetPetcare.Models;

namespace VetPetcare.Repository;

public class MedicalAppointmentRepository : IMedicalAppointment
{
    public MedicalAppointment Create(MedicalAppointment medicalAppointment)
    {
        if (medicalAppointment == null)
        {
            Console.WriteLine("Error: The appointment cannot be null.");
            return null;
        }

        Database.Database.MedicalAppointment.Add(medicalAppointment.AppointmentId, medicalAppointment);
        return medicalAppointment;
    }

    public MedicalAppointment GetById(int id)
    {
        return Database.Database.MedicalAppointment.ContainsKey(id)
            ? (MedicalAppointment)Database.Database.MedicalAppointment[id]
            : null;
    }

    public IEnumerable<MedicalAppointment> GetAll()
    {
        return Database.Database.MedicalAppointment.Values
            .OfType<MedicalAppointment>()
            .ToList();
    }

    public bool Update(MedicalAppointment medicalAppointment, int id)
    {
        if (!Database.Database.MedicalAppointment.ContainsKey(id))
        {
            Console.WriteLine("Appointment not found.");
            return false;
        }

        Database.Database.MedicalAppointment[id] = medicalAppointment;
        return true;
    }

    public bool DeleteById(int id)
    {
        if (!Database.Database.MedicalAppointment.ContainsKey(id))
        {
            Console.WriteLine("Appointment not found.");
            return false;
        }

        Database.Database.MedicalAppointment.Remove(id);
        return true;
    }
}
