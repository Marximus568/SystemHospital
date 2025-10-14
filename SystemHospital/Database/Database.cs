using VetPetcare.Models;

namespace VetPetcare.Database;

public static class Database
{
    public static List<Patient> Patients = new List<Patient>(){
        // Sample patient 1
        new Patient(
            "John",
            "Doe",
            new DateTime(1990, 5, 14),
            "3124567890",
            "john.doe@email.com",
            "123 Main Street",
            "100123456"),

        // Sample patient 2
        new Patient(
            "Maria",
            "Gonzalez",
            new DateTime(1985, 3, 22),
            "3157896541",
            "maria.gonzalez@email.com",
            "456 Elm Avenue",
            "100654789"),

        // Sample patient 3
        new Patient(
            "Carlos",
            "Ramirez",
            new DateTime(1998, 12, 2),
            "3009876543",
            "carlos.ramirez@email.com",
            "789 Oak Boulevard",
            "100789123"),

        // Sample patient 4
        new Patient(
            "Laura",
            "Fernandez",
            new DateTime(2000, 7, 19),
            "3012345678",
            "laura.fernandez@email.com",
            "321 Pine Road",
            "100321654"),

        // Sample patient 5
        new Patient(
            "David",
            "Martinez",
            new DateTime(1993, 9, 5),
            "3168527419",
            "david.martinez@email.com",
            "654 Maple Lane",
            "100963258")
    };

    public static List<Doctor> Doctors = new List<Doctor>
    {
        new Doctor(
            FirstName: "Laura",
            LastName: "Martinez",
            DateOfBirth: new DateTime(1985, 3, 12),
            phone: "3001234567",
            Email: "laura.martinez@clinic.com",
            Address: "Calle 10 #23-45, Bogotá",
            speciality: "Dermatology",
            Document: "123456789"
        ),

        new Doctor(
            FirstName: "Carlos",
            LastName: "Ramirez",
            DateOfBirth: new DateTime(1979, 8, 5),
            phone: "3109876543",
            Email: "carlos.ramirez@clinic.com",
            Address: "Av. Las Palmas 122, Medellín",
            speciality: "Cardiology",
            Document: "987654321"
        ),

        new Doctor(
            FirstName: "Andrea",
            LastName: "Gomez",
            DateOfBirth: new DateTime(1990, 11, 22),
            phone: "3206547890",
            Email: "andrea.gomez@clinic.com",
            Address: "Cra 45 #67-21, Cali",
            speciality: "Pediatrics",
            Document: "456789123"
        ),

        new Doctor(
            FirstName: "Miguel",
            LastName: "Torres",
            DateOfBirth: new DateTime(1982, 6, 30),
            phone: "3019988776",
            Email: "miguel.torres@clinic.com",
            Address: "Calle 80 #12-30, Barranquilla",
            speciality: "Neurology",
            Document: "321654987"
        ),

        new Doctor(
            FirstName: "Sofia",
            LastName: "Rojas",
            DateOfBirth: new DateTime(1992, 1, 18),
            phone: "3023344556",
            Email: "sofia.rojas@clinic.com",
            Address: "Cl 100 #20-15, Cartagena",
            speciality: "General Medicine",
            Document: "654987321"
        )
    };

    
    public static Dictionary<int, MedicalAppointment> MedicalAppointment = new Dictionary<int, MedicalAppointment>();

    public static void SeedAppointments()
    {
        var appointment1 = new MedicalAppointment
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            StartTime = new TimeOnly(12, 0),
            EndTime = new TimeOnly(14, 0),
            Doctors = new List<Doctor> { Doctors[0] },
            Clients = new List<Patient> { Patients[0] },
            Reason = "Consultation",
            Symptoms = "Skin rash"
        };

        var appointment2 = new MedicalAppointment
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
            StartTime = new TimeOnly(14, 20),
            EndTime = new TimeOnly(16, 20),
            Doctors = new List<Doctor> { Doctors[1] },
            Clients = new List<Patient> { Patients[1] },
            Reason = "Vaccination",
            Symptoms = "N/A"
        };

        var appointment3 = new MedicalAppointment
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
            StartTime = new TimeOnly(16, 30),
            EndTime = new TimeOnly(18, 30),
            Doctors = new List<Doctor> { Doctors[2] },
            Clients = new List<Patient> { Patients[2] },
            Reason = "Bath",
            Symptoms = "Dirty fur"
        };

        // Agregar al diccionario
        MedicalAppointment.Add(appointment1.AppointmentId, appointment1);
        MedicalAppointment.Add(appointment2.AppointmentId, appointment2);
        MedicalAppointment.Add(appointment3.AppointmentId, appointment3);
    }
}