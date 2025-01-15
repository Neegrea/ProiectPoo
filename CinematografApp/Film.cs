namespace CinematografApp
{
    public class Film
    {
        public string Nume { get; set; }
        public string Descriere { get; set; }
        public List<ProgramRulare> ProgramRulari { get; set; } = new List<ProgramRulare>();

    }
}
