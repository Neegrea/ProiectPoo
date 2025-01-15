namespace CinematografApp
{
    public class CinematografApp
    {
        private List<Film> filme = new List<Film>();
        private List<Sala> sali = new List<Sala>();

        public CinematografApp()
        {
            sali.Add(new Sala { Nume = "Sala 1" });
            sali.Add(new Sala { Nume = "Sala 2" });
            sali.Add(new Sala { Nume = "Sala 3" });
        }
        public void AdaugaFilm(Film film)
        {
            filme.Add(film);
            Console.WriteLine($"Filmul \"{film.Nume}\" a fost adaugat cu succes.");
        }
        public void ScoateFilm(string numeFilm)
        {
            var film = filme.FirstOrDefault(f => f.Nume.Equals(numeFilm, StringComparison.OrdinalIgnoreCase));
            if (film != null)
            {
                filme.Remove(film);
                Console.WriteLine($"Filmul \"{numeFilm}\" a fost scos complet din rulari.");
            }
            else
            {
                Console.WriteLine("Filmul nu a fost gasit.");
            }
        }

        public void AdaugaRulare(string numeFilm, string salaNume, string zi, string intervalOrar, int locuriMaxime)
        {
            var film = filme.FirstOrDefault(f => f.Nume.Equals(numeFilm, StringComparison.OrdinalIgnoreCase));
            if (film == null)
            {
                Console.WriteLine("Filmul nu a fost gasit.");
                return;
            }

            var sala = sali.FirstOrDefault(s => s.Nume.Equals(salaNume, StringComparison.OrdinalIgnoreCase));
            if (sala == null)
            {
                Console.WriteLine("Sala specificata nu exista.");
                return;
            }

            if (sala.Rulari.Any(r => r.Zi.Equals(zi, StringComparison.OrdinalIgnoreCase) &&
                                     r.IntervalOrar.Equals(intervalOrar, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"Intervalul orar {intervalOrar} in ziua {zi} este deja rezervat in {salaNume}.");
                return;
            }

            var rulare = new ProgramRulare
            {
                Film = film.Nume,
                Zi = zi,
                IntervalOrar = intervalOrar,
                LocuriMaxime = locuriMaxime,
                LocuriDisponibile = locuriMaxime
            };
            sala.Rulari.Add(rulare);
            film.ProgramRulari.Add(rulare);

            Console.WriteLine($"Rularea a fost adaugata cu succes in {salaNume} pentru filmul \"{film.Nume}\".");
        }

        public void ModificaNumarLocuri(string numeFilm, string zi, string intervalOrar, int locuriMaxime)
        {
            var film = filme.FirstOrDefault(f => f.Nume.Equals(numeFilm, StringComparison.OrdinalIgnoreCase));
            if (film == null)
            {
                Console.WriteLine("Filmul nu a fost gasit.");
                return;
            }

            var rulare = film.ProgramRulari.FirstOrDefault(r => r.Zi.Equals(zi, StringComparison.OrdinalIgnoreCase) && r.IntervalOrar.Equals(intervalOrar, StringComparison.OrdinalIgnoreCase));

            if (rulare == null)
            {
                Console.WriteLine("Rularea nu a fost gasita.");
                return;
            }

            int locuriOcupate = rulare.LocuriMaxime - rulare.LocuriDisponibile;
            rulare.LocuriMaxime = locuriMaxime;
            rulare.LocuriDisponibile = Math.Max(0, locuriMaxime - locuriOcupate);
            Console.WriteLine($"Numarul maxim de locuri pentru rularea \"{zi} {intervalOrar}\" a fost modificat la {locuriMaxime}.");
        }

        public void VizualizeazaLocuriOcupate()
        {
            Console.WriteLine("\nLocuri ocupate pentru fiecare rulare:");
            foreach (var film in filme)
            {
                foreach (var rulare in film.ProgramRulari)
                {
                    int locuriOcupate = rulare.LocuriMaxime - rulare.LocuriDisponibile;
                    Console.WriteLine($"- Film: {film.Nume}, Zi: {rulare.Zi}, Interval: {rulare.IntervalOrar}, Locuri ocupate: {locuriOcupate}/{rulare.LocuriMaxime}");
                }
            }
        }

        public void CautaFilmePeZi(string zi)
        {
            var rezultate = filme.Where(f => f.ProgramRulari.Any(r => r.Zi.Equals(zi, StringComparison.OrdinalIgnoreCase))).ToList();

            if (!rezultate.Any())
            {
                Console.WriteLine("Nu s-au gasit filme pentru ziua specificata.");
                return;
            }

            Console.WriteLine($"Filme care ruleaza in ziua {zi}:");
            foreach (var film in rezultate)
            {
                Console.WriteLine($"- {film.Nume}");
            }
        }

        public void InspecteazaFilm(string numeFilm)
        {
            var film = filme.FirstOrDefault(f => f.Nume.Equals(numeFilm, StringComparison.OrdinalIgnoreCase));
            if (film == null)
            {
                Console.WriteLine("Filmul nu a fost gasit.");
                return;
            }

            Console.WriteLine($"Detalii pentru filmul \"{film.Nume}\":");
            Console.WriteLine($"Descriere: {film.Descriere}");
            Console.WriteLine("Program rulare:");
            foreach (var rulare in film.ProgramRulari)
            {
                Console.WriteLine($"- Zi: {rulare.Zi}, Interval: {rulare.IntervalOrar}, Locuri disponibile: {rulare.LocuriDisponibile}/{rulare.LocuriMaxime}");
            }
        }

        public void CautaFilmDupaNume(string numeFilm)
        {
            var film = filme.FirstOrDefault(f => f.Nume.Contains(numeFilm, StringComparison.OrdinalIgnoreCase));
            if (film == null)
            {
                Console.WriteLine("Nu s-a gasit niciun film cu acest nume.");
                return;
            }

            Console.WriteLine($"Film gasit: {film.Nume}");
        }

        public void VizualizeazaFilme()
        {
            Console.WriteLine("\nFilmele care ruleaza sunt:");
            foreach (var film in filme)
            {
                Console.WriteLine($"- {film.Nume}");
            }
        }
        
        public void RezervaLocuri(string numeFilm, string zi, string intervalOrar, int nrLocuri)
        {
            var film = filme.FirstOrDefault(f => f.Nume.Equals(numeFilm, StringComparison.OrdinalIgnoreCase));
            if (film == null)
            {
                Console.WriteLine("Filmul nu a fost gasit.");
                return;
            }

            var rulare = film.ProgramRulari.FirstOrDefault(p =>
                p.Zi.Equals(zi, StringComparison.OrdinalIgnoreCase) &&
                p.IntervalOrar.Equals(intervalOrar, StringComparison.OrdinalIgnoreCase));

            if (rulare == null)
            {
                Console.WriteLine("Rularea specificata nu a fost gasita.");
                return;
            }

            if (nrLocuri < 1 || nrLocuri > 5)
            {
                Console.WriteLine("Puteti rezerva intre 1 si 5 locuri.");
                return;
            }

            if (rulare.LocuriDisponibile < nrLocuri)
            {
                Console.WriteLine("Nu sunt suficiente locuri disponibile.");
                return;
            }

            rulare.LocuriDisponibile -= nrLocuri;
            Console.WriteLine($"Rezervare efectuata! {nrLocuri} locuri rezervate la {film.Nume}, {zi} {intervalOrar}.");
        }


        public void MeniuAdministrator()
        {
            while (true)
            {
                Console.WriteLine("\nMeniu Administrator:");
                Console.WriteLine("1. Adauga un film nou");
                Console.WriteLine("2. Adauga rulare pentru un film");
                Console.WriteLine("3. Scoate un film complet");
                Console.WriteLine("4. Modifica numarul maxim de locuri pentru o rulare");
                Console.WriteLine("5. Vizualizeaza locurile ocupate pentru fiecare rulare");
                Console.WriteLine("6. Revenire la meniul principal");
                Console.Write("Alege o optiune: ");
                var optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1":
                        Console.Write("Introduceti numele filmului: ");
                        var numeFilm = Console.ReadLine();
                        Console.Write("Introduceti descrierea filmului: ");
                        var descriere = Console.ReadLine();
                        AdaugaFilm(new Film { Nume = numeFilm, Descriere = descriere });
                        break;
                    case "2":
                        Console.Write("Introduceti numele filmului: ");
                        var filmAdaugat = Console.ReadLine();
                        Console.Write("Introduceti numele salii: ");
                        var salaNume = Console.ReadLine();
                        Console.Write("Introduceti ziua: ");
                        var zi = Console.ReadLine();
                        Console.Write("Introduceti intervalul orar: ");
                        var intervalOrar = Console.ReadLine();
                        Console.Write("Introduceti numarul maxim de locuri: ");
                        if (int.TryParse(Console.ReadLine(), out int locuriMaxime))
                        {
                            AdaugaRulare(filmAdaugat, salaNume, zi, intervalOrar, locuriMaxime);
                        }
                        else
                        {
                            Console.WriteLine("Numar invalid.");
                        }
                        break;
                    case "3":
                        Console.Write("Introduceti numele filmului: ");
                        var filmSters = Console.ReadLine();
                        ScoateFilm(filmSters);
                        break;
                    case "4":
                        Console.Write("Introduceti numele filmului: ");
                        var filmModificat = Console.ReadLine();
                        Console.Write("Introduceti ziua: ");
                        var ziModificata = Console.ReadLine();
                        Console.Write("Introduceti intervalul orar: ");
                        var intervalModificat = Console.ReadLine();
                        Console.Write("Introduceti noul numar maxim de locuri: ");
                        if (int.TryParse(Console.ReadLine(), out int locuriNoi))
                        {
                            ModificaNumarLocuri(filmModificat, ziModificata, intervalModificat, locuriNoi);
                        }
                        else
                        {
                            Console.WriteLine("Numar invalid.");
                        }
                        break;
                    case "5":
                        VizualizeazaLocuriOcupate();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Optiune invalida. Incercati din nou.");
                        break;
                }
            }
        }

        public void MeniuUtilizator()
        {
            while (true)
            {
                Console.WriteLine("\nMeniu Utilizator:");
                Console.WriteLine("1. Vizualizeaza lista filmelor");
                Console.WriteLine("2. Rezerva locuri pentru un film");
                Console.WriteLine("3. Cauta filme intr-o anumita zi");
                Console.WriteLine("4. Inspecteaza un film pentru detalii");
                Console.WriteLine("5. Cauta un film după nume");
                Console.WriteLine("6. Revenire la meniul principal");
                Console.Write("Alege o optiune: ");
                var optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1":
                        VizualizeazaFilme();
                        break;
                    case "2":
                        Console.Write("Introduceti numele filmului: ");
                        var numeFilm = Console.ReadLine();
                        Console.Write("Introduceti ziua: ");
                        var zi = Console.ReadLine();
                        Console.Write("Introduceti intervalul orar: ");
                        var intervalOrar = Console.ReadLine();
                        Console.Write("Introduceti numarul de locuri: ");
                        if (int.TryParse(Console.ReadLine(), out int nrLocuri))
                        {
                            RezervaLocuri(numeFilm, zi, intervalOrar, nrLocuri);
                        }
                        else
                        {
                            Console.WriteLine("Numar invalid.");
                        }
                        break;
                    case "3":
                        Console.Write("Introduceti ziua: ");
                        var ziCautata = Console.ReadLine();
                        CautaFilmePeZi(ziCautata);
                        break;
                    case "4":
                        Console.Write("Introduceti numele filmului: ");
                        var filmInspectat = Console.ReadLine();
                        InspecteazaFilm(filmInspectat);
                        break;
                    case "5":
                        Console.Write("Introduceti numele filmului: ");
                        var filmCautat = Console.ReadLine();
                        CautaFilmDupaNume(filmCautat);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Optiune invalida. Incercati din nou.");
                        break;
                }
            }
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\nMeniu Principal:");
                Console.WriteLine("1. Utilizator");
                Console.WriteLine("2. Administrator");
                Console.WriteLine("3. Iesire");
                Console.Write("Alege o optiune: ");
                var optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1":
                        MeniuUtilizator();
                        break;
                    case "2":
                        MeniuAdministrator();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Optiune invalida. Incercati din nou.");
                        break;
                }
            }
        }
    }
}