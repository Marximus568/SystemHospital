
public interface IPatientRepository
{
    Patient Create(Patient patient);
    Patient GetByDocument(int document);
    IEnumerable<Patient> GetAll();
    bool Update(Patient patient, int document);
    bool DeleteById(int document);
}