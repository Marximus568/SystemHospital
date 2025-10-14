
public interface IPatientRepository
{
    Patient Create(Patient patient);
    Patient GetByDocument(string document);
    IEnumerable<Patient> GetAll();
    bool Update(Patient patient, string document);
    bool DeleteByDocument(string document);
}