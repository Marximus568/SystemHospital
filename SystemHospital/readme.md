
```markdown
# VetPetcare Hospital System

## Overview

VetPetcare Hospital System is a C# console application designed to manage patient records in a veterinary hospital. It allows users to create, search, update, list, and delete patient information efficiently.

## Required Technologies

- \*.NET SDK 6.0 or higher\* (for building and running the project)
- \*C#\* (main programming language)
- \*JetBrains Rider\* or any compatible IDE (recommended for development)
- \*Linux\* (tested and supported OS)

## Architecture & Design Pattern

The project follows a \*layered architecture\*:

- \*Models Layer\*: Contains domain entities like `Patient`.
- \*Repository Layer\*: Handles data access and persistence (`PatientRepository`).
- \*Services Layer\*: Implements business logic and user interaction (`ServicePatient`).

The main design pattern used is the \*Repository Pattern\*, which abstracts data operations and separates business logic from data access.

## Step-by-Step Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/vetpetcare-hospital-system.git
   cd vetpetcare-hospital-system
   ```

2. **Install .NET SDK**
   - Download it from: https://dotnet.microsoft.com/download

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Build the project**
   ```bash
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run --project SystemHospital
   ```

## How to Use

- **Create patient**: Follow the prompts to enter patient details. The system validates each field and ensures document uniqueness.
- **Find patient**: Search for a patient by document number.
- **Show all patients**: List all registered patients in a formatted table.
- **Update patient**: Update patient information by providing the current document number.
- **Delete patient**: Remove a patient record by document number.

All operations are performed via console prompts and feedback messages.

## Functionalities

With VetPetcare Hospital System, you can:

- **Add new patients**: Register a new patient by entering their details. The system checks for unique document numbers and validates all fields.
- **Search for patients**: Find a patient using their document number to quickly access their information.
- **List all patients**: Display all registered patients in a formatted table for easy review.
- **Update patient information**: Modify the details of an existing patient by providing their current document number.
- **Delete patients**: Remove a patient record from the system using their document number.

All these operations are performed through interactive console prompts, making the application easy to use for managing veterinary patient records.
  
## Project Structure

```
SystemHospital/
│
├─ Database/
│ └─ Database.cs # Simulates the database using in-memory collections
│
├─ Docs/
│ └─ Class_Diagram.png # Diagram of classes in the project
│
├─ Dtos/ # Data Transfer Objects (if applicable)
│
├─ Interface/
│ ├─ IDoctorRepository.cs # Interface for doctor repository
│ ├─ IMedicalAppointment.cs # Interface for medical appointment repository
│ └─ IPatientRepository.cs # Interface for patient repository
│
├─ Models/
│ ├─ Doctor.cs # Doctor model
│ ├─ MedicalAppointment.cs # Appointment model with status management
│ ├─ Patient.cs # Patient model
│ └─ People.cs # Base class for common person properties
│
├─ Repository/
│ ├─ DoctorRepository.cs # Repository for CRUD operations of doctors
│ ├─ MedicalAppointment.cs # Repository for appointments
│ └─ PatientRepository.cs # Repository for patients
│
├─ Services/
│ ├─ ServiceDoctor.cs # Business logic for doctor operations
│ ├─ ServiceMedicalAppointment.cs # Business logic for appointments
│ └─ ServicePatient.cs # Business logic for patient operations
│
├─ Utils/
│ ├─ Menus/
│ │ ├─ MenuDoctor.cs
│ │ ├─ MenuMain.cs
│ │ ├─ MenuMedicalAppointment.cs
│ │ └─ MenuPatient.cs
│ └─ Query/
│ └─ LINQ.cs # LINQ helper queries
│
├─ Program.cs # Entry point of the application
└─ readme.md # Project documentation
```

## Notes

- Make sure you have the correct .NET SDK installed.
- The application is designed for console use; no GUI is provided.
- All data is managed in-memory unless otherwise configured in the repository.
```

