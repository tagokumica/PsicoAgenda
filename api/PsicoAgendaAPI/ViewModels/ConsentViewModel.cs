
namespace PsicoAgendaAPI.ViewModels
{
    public class ConsentViewModel
    {
        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }
        public string ConsentType { get; private set; }
        public DateTime GivenAt { get; private set; }
        public string Version { get; private set; }
        public PatientViewModel PatientViewModel { get; private set; }
        public ConsentViewModel() { }
        public ConsentViewModel(Guid id, Guid patientId, string consentType, DateTime givenAt, string version)
        {
            Id = id;
            PatientId = patientId;
            ConsentType = consentType;
            GivenAt = givenAt;
            Version = version;
        }

    }
}