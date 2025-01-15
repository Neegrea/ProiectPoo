namespace CinematografApp
{
    public class Sala
    {
        public string Nume { get; set; }
        public List<ProgramRulare> Rulari { get; set; } = new List<ProgramRulare>();
    }
}
