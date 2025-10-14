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
            // Obtener todos los doctores y pacientes
            var doctors = DoctorRepository.GetAll().ToList();
            var patients = PatientRepository.GetAll().ToList();

            if (!doctors.Any())
            {
                Console.WriteLine(
                    "\nNo doctors registered in the system. Please register one before scheduling an appointment.");
                return;
            }

            if (!patients.Any())
            {
                Console.WriteLine(
                    "\nNo clients registered in the system. Please register one before scheduling an appointment.");
                return;
            }

            // ==============================
            // Enter appointment date
            // ==============================
            DateOnly date;
            while (true)
            {
                Console.Write("Enter appointment date (yyyy-mm-dd): ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Date cannot be empty. Please enter a valid date.");
                    continue;
                }

                if (!DateOnly.TryParse(input, out date))
                {
                    Console.WriteLine("Invalid format. Please use yyyy-mm-dd.");
                    continue;
                }

                if (date < DateOnly.FromDateTime(DateTime.Now))
                {
                    Console.WriteLine("Date must be today or in the future. Please try again.");
                    continue;
                }

                break;
            }

            // ==============================
            // Choose time slot
            // ==============================
            var slots = MedicalAppointment.GetAvailableSlots();
            Console.WriteLine("\nChoose a time slot:");
            foreach (var slot in slots)
                Console.WriteLine($"{slot.Id}. {slot.Range}");

            int slotId;
            while (!int.TryParse(Console.ReadLine(), out slotId) || !slots.Any(s => s.Id == slotId))
                Console.WriteLine("Invalid option. Choose between the available IDs:");

            var chosenSlot = slots.First(s => s.Id == slotId);

            // ==============================
            // Select doctor by document
            // ==============================
            Console.WriteLine("\nEnter doctor document:");
            string doctorDocument;
            Doctor doctor;
            do
            {
                doctorDocument = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(doctorDocument))
                {
                    Console.WriteLine("Document cannot be empty. Please enter a valid document:");
                    continue;
                }

                doctor = DoctorRepository.GetByDocument(doctorDocument);
                if (doctor == null)
                {
                    Console.WriteLine("Doctor not found. Try again:");
                    continue;
                }

                break;
            } while (true);

            // ==============================
            // Select client by document
            // ==============================
            Console.WriteLine("\nEnter client document number:");
            string clientDocument;
            Patient client;
            do
            {
                clientDocument = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(clientDocument))
                {
                    Console.WriteLine("Document cannot be empty. Try again:");
                    continue;
                }

                if (!clientDocument.All(char.IsDigit))
                {
                    Console.WriteLine("Document must contain only numeric characters. Try again:");
                    continue;
                }

                client = PatientRepository.GetByDocument(clientDocument);
                if (client == null)
                {
                    Console.WriteLine("No client found with this document number. Try again:");
                    continue;
                }

                break; // valid client found
            } while (true);

            // ==============================
            // Appointment reason/type
            // ==============================
            string reason = MedicalAppointment.SelectAppointmentType();

            // ==============================
            // Validate conflicts using repository
            // ==============================
            var doctorAppointments = _repository.GetByDoctorDocument(doctor.Document);
            bool conflictExists = doctorAppointments.Any(a =>
                a.Date == date && a.StartTime == chosenSlot.Start && a.EndTime == chosenSlot.End
            );

            if (conflictExists)
            {
                Console.WriteLine(
                    $"\n⚠️  Cannot schedule appointment: Doctor {doctor.FirstName} {doctor.LastName} already has an appointment at this date and time.");
                return;
            }

            // ==============================
            // Create appointment object
            // ==============================
            var appointment = new MedicalAppointment
            {
                Date = date,
                StartTime = chosenSlot.Start,
                EndTime = chosenSlot.End,
                Doctors = new List<Doctor> { doctor },
                Clients = new List<Patient> { client },
                Reason = reason,
                Symptoms = "N/A"
            };

            // Guardar cita en el repositorio
            _repository.Create(appointment);

            // ==============================
            // Success message
            // ==============================
            Console.WriteLine("\nAppointment successfully scheduled!");
            Console.WriteLine("========================================");
            Console.WriteLine($"Date: {appointment.Date}");
            Console.WriteLine($"Time: {chosenSlot.Range}");
            Console.WriteLine($"Doctor: {doctor.FirstName} {doctor.LastName}");
            Console.WriteLine($"Client: {client.FirstName} {client.LastName}");
            Console.WriteLine($"Reason: {reason}");
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
                $"Veterinary: {a.Doctors.FirstOrDefault()?.FirstName} {a.Doctors.FirstOrDefault()?.LastName}");
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
            $"Veterinary: {appointment.Doctors.FirstOrDefault()?.FirstName} {appointment.Doctors.FirstOrDefault()?.LastName}");
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

        try
        {
            // ==============================
            // Update appointment date
            // ==============================
            DateOnly newDate;
            while (true)
            {
                Console.Write(
                    $"Enter new appointment date (yyyy-mm-dd) or press Enter to keep current ({existing.Date}): ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(input))
                {
                    newDate = existing.Date;
                    break;
                }

                if (!DateOnly.TryParse(input, out newDate))
                {
                    Console.WriteLine("Invalid format. Please use yyyy-mm-dd.");
                    continue;
                }

                if (newDate < DateOnly.FromDateTime(DateTime.Now))
                {
                    Console.WriteLine("Date must be today or in the future. Please try again.");
                    continue;
                }

                break;
            }

            // ==============================
            // Update time slot
            // ==============================
            var slots = MedicalAppointment.GetAvailableSlots();
            Console.WriteLine("\nChoose a new time slot or press Enter to keep current:");
            foreach (var slot in slots)
                Console.WriteLine($"{slot.Id}. {slot.Range}");

            (int Id, string Range, TimeOnly Start, TimeOnly End) chosenSlot;
            while (true)
            {
                Console.Write($"Enter slot ID (current: {existing.StartTime:hh\\:mm} - {existing.EndTime:hh\\:mm}): ");
                string slotInput = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(slotInput))
                {
                    // Keep current slot
                    chosenSlot = (0, $"{existing.StartTime:hh\\:mm} - {existing.EndTime:hh\\:mm}", existing.StartTime,
                        existing.EndTime);
                    break;
                }

                if (!int.TryParse(slotInput, out int slotId) || !slots.Any(s => s.Id == slotId))
                {
                    Console.WriteLine("Invalid option. Choose between the available IDs:");
                    continue;
                }

                chosenSlot = slots.First(s => s.Id == slotId);
                break;
            }

            // ==============================
            // Update doctor
            // ==============================
            Console.WriteLine("\nEnter new doctor document or press Enter to keep current:");
            string doctorDocument;
            Doctor doctor;
            while (true)
            {
                doctorDocument = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(doctorDocument))
                {
                    doctor = existing.Doctors.First();
                    break;
                }

                doctor = DoctorRepository.GetByDocument(doctorDocument);
                if (doctor == null)
                {
                    Console.WriteLine("Doctor not found. Try again:");
                    continue;
                }

                break;
            }

            // ==============================
            // Update client
            // ==============================
            Console.WriteLine("\nEnter new client document or press Enter to keep current:");
            string clientDocument;
            Patient client;
            while (true)
            {
                clientDocument = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(clientDocument))
                {
                    client = existing.Clients.First();
                    break;
                }

                if (!clientDocument.All(char.IsDigit))
                {
                    Console.WriteLine("Document must contain only numeric characters. Try again:");
                    continue;
                }

                client = PatientRepository.GetByDocument(clientDocument);
                if (client == null)
                {
                    Console.WriteLine("No client found with this document number. Try again:");
                    continue;
                }

                break;
            }

            // ==============================
            // Update reason
            // ==============================
            Console.WriteLine($"Current reason: {existing.Reason}");
            Console.WriteLine("Change reason? (y/n)");
            string reason;
            if (Console.ReadLine()?.Trim().ToLower() == "y")
                reason = MedicalAppointment.SelectAppointmentType();
            else
                reason = existing.Reason;

            // ==============================
            // Update symptoms
            // ==============================
            Console.WriteLine($"Current symptoms: {existing.Symptoms}");
            Console.WriteLine("Enter new symptoms (leave empty to keep current):");
            string symptoms = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(symptoms))
                symptoms = existing.Symptoms;

            // ==============================
            // Validate conflicts for the doctor
            // ==============================
            var doctorAppointments = _repository.GetByDoctorDocument(doctor.Document)
                .Where(a => a.AppointmentId != existing.AppointmentId)
                .ToList();

            bool conflictExists = doctorAppointments.Any(a =>
                a.Date == newDate && a.StartTime == chosenSlot.Start && a.EndTime == chosenSlot.End
            );

            if (conflictExists)
            {
                Console.WriteLine(
                    $"\n⚠️  Cannot update appointment: Doctor {doctor.FirstName} {doctor.LastName} already has an appointment at this date and time.");
                return;
            }

            // ==============================
            // Apply updates
            // ==============================
            existing.Date = newDate;
            existing.StartTime = chosenSlot.Start;
            existing.EndTime = chosenSlot.End;
            existing.Doctors = new List<Doctor> { doctor };
            existing.Clients = new List<Patient> { client };
            existing.Reason = reason;
            existing.Symptoms = symptoms;

            bool success = _repository.Update(existing, id);

            if (success)
                Console.WriteLine("Appointment updated successfully!");
            else
                Console.WriteLine("Error updating appointment.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating appointment: {e.Message}");
        }
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