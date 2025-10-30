namespace PsicoAgendaAPI.ViewModels
{
    public class SpecialityViewModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<HealthCareProfissionalViewModel> HealthCareProfissionalsViewModel { get; private set; } = new List<HealthCareProfissionalViewModel>();
        public SpecialityViewModel() { }
        public SpecialityViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}