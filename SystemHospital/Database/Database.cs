using VetPetcare.Models;

namespace VetPetcare.Database;

public static class Database
{
    public static List<Patient> Patients = new List<Patient>();

    public static List<Doctor> Doctors = new List<Doctor>();
    
    public static Dictionary<object, object> MedicalAppointment = new Dictionary<object, object>();
}