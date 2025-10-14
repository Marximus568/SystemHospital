using VetPetcare.Repository;

namespace VetPetcare.Models;

public static class ServiceMedicalAppointment
{
    private static readonly MedicalAppointmentRepository _repository = new();
    private static readonly DoctorRepository DoctorRepository = new();
    private static readonly PatientRepository PatientRepository = new();

    //Create appointment
    public static void ScheduleAppointment()
    {
        try
        {
            var veterinarians = DoctorRepository.GetAll().ToList();
            var clients = PatientRepository.GetAll().ToList();

            if (!veterinarians.Any())
            {
                Console.WriteLine(
                    "\nNo veterinarians registered in the system. Please register one before scheduling an appointment.");
                return;
            }

            if (!clients.Any())
            {
                Console.WriteLine(
                    "\nNo clients registered in the system. Please register one before scheduling an appointment.");
                return;
            }

            Console.WriteLine("Enter appointment date (yyyy-mm-dd):");
            DateOnly date;
            while (!DateOnly.TryParse(Console.ReadLine(), out date))
                Console.WriteLine("Invalid format. Try again (yyyy-mm-dd):");

            var slots = MedicalAppointment.GetAvailableSlots();

            Console.WriteLine("\nChoose a time slot:");
            foreach (var slot in slots)
                Console.WriteLine($"{slot.Id}. {slot.Range}");

            int slotId;
            while (!int.TryParse(Console.ReadLine(), out slotId) || !slots.Any(s => s.Id == slotId))
                Console.WriteLine("Invalid option. Choose between the available IDs:");

            var chosenSlot = slots.First(s => s.Id == slotId);

            Console.WriteLine("\nEnter veterinary ID:");
            int vetId;
            while (!int.TryParse(Console.ReadLine(), out vetId))
                Console.WriteLine("Invalid ID. Try again:");

            var veterinary = DoctorRepository.GetById(vetId);
            if (veterinary == null)
            {
                Console.WriteLine("Veterinary not found. Canceling appointment.");
                return;
            }

            Console.WriteLine("\nEnter client ID:");
            int clientId;
            while (!int.TryParse(Console.ReadLine(), out clientId))
                Console.WriteLine("Invalid ID. Try again:");

            var client = PatientRepository.GetByDocument(clientId);
            if (client == null)
            {
                Console.WriteLine("Client not found. Canceling appointment.");
                return;
            }

            string reason = MedicalAppointment.SelectAppointmentType();

            // ============================================
            // 🔍 VALIDATION: check if the slot is already taken
            // ============================================
            bool conflictExists = Database.Database.MedicalAppointment
                .Values
                .OfType<MedicalAppointment>()
                .Any(a => a.Date == date && a.StartTime == chosenSlot.Start && a.EndTime == chosenSlot.End);

            if (conflictExists)
            {
                Console.WriteLine(
                    "\n⚠️  Cannot schedule appointment: There is already another appointment at the same date and time.");
                return;
            }

            // ============================================
            // Create appointment (no conflicts)
            // ============================================
            var appointment = new MedicalAppointment
            {
                Date = date,
                StartTime = chosenSlot.Start,
                EndTime = chosenSlot.End,
                Veterinaries = new List<Doctor> { veterinary },
                Clients = new List<Patient> { client },
                Reason = reason,
                Symptoms = "N/A"
            };

            _repository.Create(appointment);

            // Add appointment to the dictionary
            Database.Database.MedicalAppointment.Add(appointment.AppointmentId, appointment);

            Console.WriteLine("\n Appointment successfully scheduled!");
            Console.WriteLine("========================================");
            Console.WriteLine($"Date: {appointment.Date}");
            Console.WriteLine($"Time: {chosenSlot.Range}");
            Console.WriteLine($"Veterinary: {veterinary.FirstName} {veterinary.LastName}");
            Console.WriteLine($"Client: {client.FirstName} {client.LastName}");
            Console.WriteLine($"Type: {reason}");
            Console.WriteLine("========================================");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error scheduling appointment: {e.Message}");
        }
    }
    //Show appointments
    public static void ShowAllAppointments()
    {
        var appointments = _repository.GetAll().ToList();

        if (appointments.Count == 0)
        {
            Console.WriteLine("No appointments found.");
            return;
        }

        Console.WriteLine("\nRegistered Appointments:");
        Console.WriteLine("----------------------------------------");
        foreach (var a in appointments)
        {
            Console.WriteLine($"ID: {a.AppointmentId}");
            Console.WriteLine($"Date: {a.Date}");
            Console.WriteLine($"Time: {a.StartTime:HH:mm} - {a.EndTime:HH:mm}");
            Console.WriteLine(
                $"Veterinary: {a.Veterinaries.FirstOrDefault()?.FirstName} {a.Veterinaries.FirstOrDefault()?.LastName}");
            Console.WriteLine(
                $"Client: {a.Clients.FirstOrDefault()?.FirstName} {a.Clients.FirstOrDefault()?.LastName}");
            Console.WriteLine($"Reason: {a.Reason}");
            Console.WriteLine($"Symptoms: {a.Symptoms}");
            Console.WriteLine("----------------------------------------");
        }
    }
    //Show apoointment by Id
    public static void ShowAppointmentById(int id)
    {
        var appointment = _repository.GetById(id);
        if (appointment == null)
        {
            Console.WriteLine("Appointment not found.");
            return;
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"ID: {appointment.AppointmentId}");
        Console.WriteLine($"Date: {appointment.Date}");
        Console.WriteLine($"Time: {appointment.StartTime:HH:mm} - {appointment.EndTime:HH:mm}");
        Console.WriteLine($"Reason: {appointment.Reason}");
        Console.WriteLine($"Symptoms: {appointment.Symptoms}");
        Console.WriteLine(
            $"Veterinary: {appointment.Veterinaries.FirstOrDefault()?.FirstName} {appointment.Veterinaries.FirstOrDefault()?.LastName}");
        Console.WriteLine(
            $"Client: {appointment.Clients.FirstOrDefault()?.FirstName} {appointment.Clients.FirstOrDefault()?.LastName}");
        Console.WriteLine("----------------------------------------");
    }
    //Update an appointment
    public static void UpdateAppointment(int id)
    {
        var existing = _repository.GetById(id);
        if (existing == null)
        {
            Console.WriteLine("Appointment not found.");
            return;
        }

        Console.WriteLine("Enter new symptoms (leave empty to keep current):");
        string newSymptoms = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newSymptoms))
            existing.Symptoms = newSymptoms;

        Console.WriteLine("Would you like to change the reason? (y/n)");
        if (Console.ReadLine()?.Trim().ToLower() == "y")
            existing.Reason = MedicalAppointment.SelectAppointmentType();

        bool success = _repository.Update(existing, id);

        if (success)
            Console.WriteLine("Appointment updated successfully!");
        else
            Console.WriteLine("Error updating appointment.");
    }
    //Delete appointment by ID
    public static void DeleteAppointment(int id)
    {
        bool success = _repository.DeleteById(id);

        if (success)
            Console.WriteLine("Appointment deleted successfully!");
        else
            Console.WriteLine("Appointment not found.");
    }
}