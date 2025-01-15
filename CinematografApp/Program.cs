namespace CinematografApp;

public class Program
{
    public static void Main(string[] args)
    {
        var app = new CinematografApp();

        app.AdaugaFilm(new Film
        {
            Nume = "Film Demo 1",
            Descriere = "Descriere Film Demo 1"
        });

        app.Start();
    }
}