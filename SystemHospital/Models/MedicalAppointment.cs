namespace VetPetcare.Models;

public class MedicalAppointment
{
    private static int _lastId = 1;

    public int AppointmentId { get; internal set; } = _lastId++;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public List<Doctor> Veterinaries { get; set; } = new();
    public List<Patient> Clients { get; set; } = new();
    public string Reason { get; set; }
    public string Symptoms { get; set; }

    public MedicalAppointment() { }

    public MedicalAppointment(DateOnly date, List<Doctor> veterinaries, List<Patient> clients, string reason, string symptoms)
    {
        Date = date;
        Veterinaries = veterinaries ?? new();
        Clients = clients ?? new();
        Reason = reason;
        Symptoms = symptoms;
    }
    public static List<(int Id, string Range, TimeOnly Start, TimeOnly End)> GetAvailableSlots()
    {
        return new List<(int, string, TimeOnly, TimeOnly)>
        {
            (1, "12:00 PM - 2:00 PM", new TimeOnly(12, 0), new TimeOnly(14, 0)),
            (2, "2:20 PM - 4:20 PM", new TimeOnly(14, 20), new TimeOnly(16, 20)),
            (3, "4:30 PM - 6:30 PM", new TimeOnly(16, 30), new TimeOnly(18, 30))
        };
    }

    public static string SelectAppointmentType()
    {
        Console.WriteLine("\nSelect type of appointment:");
        Console.WriteLine("1. Consultation");
        Console.WriteLine("2. Bath");
        Console.WriteLine("3. Vaccination");

        string type = Console.ReadLine().Trim();

        return type switch
        {
            "1" => "Consultation",
            "2" => "Bath",
            "3" => "Vaccination",
            _ => "General Appointment"
        };
    }
}