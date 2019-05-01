namespace Serwis.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Serwis.DAL;
    using Serwis.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Serwis.DAL.DefaultConnection>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Serwis.DAL.DefaultConnection context)
        {
            var clients = new List<Client>
{
new
Client{ID=1, Imi�="Maria",Nazwisko="Kowalska",Adres="Kwiatowa 5/23",Miasto="Rzesz�w", Nr_telefonu="567340995",Adres_email="m.kowalska@poczta.pl"},
new
Client{ID=2, Imi�="Pawe�",Nazwisko="Rybak",Adres="Rejtana 18/1",Miasto="Rzesz�w", Nr_telefonu="667980500",Adres_email=""},

    };
            clients.ForEach(s => context.Clients.Add(s));
            context.SaveChanges();

            var employees = new List<Employee>
{
new
Employee{ID=1, Imi�="Kinga",Nazwisko="Pszeniczna", Stanowisko= Stanowisko.Administrator ,Adres_email="kinia6124@wp.pl"},

    };
            employees.ForEach(s => context.Employees.Add(s));
            context.SaveChanges();

            var repair_statuses = new List<Repair_status>
{
new
Repair_status{ID=1, RequestID=1, ClientID=1, Data_rozpocz�cia=DateTime.Parse("2018-04-23"), Data_zako�czenia=DateTime.Parse("2018-05-02"), Opis_naprawy="Wymiana dysku", Status="Zako�czone naprawione"},

    };
            repair_statuses.ForEach(s => context.Repair_statuses.Add(s));
            context.SaveChanges();

            var requests = new List<Request>
{
new
Request{ID=1, ClientID=1, EmployeeID=1, Typ_urz�dzenia=Typ_urz�dzenia.notebook, Model="Lenovo", Opis_usterki="Nie uruchamia si�", Uwagi="Wazne dane na dysku, brak has�a, zasilacz w komplecie", Czy_gwarancja=false},

    };
            requests.ForEach(s => context.Requests.Add(s));
            context.SaveChanges();
        }
    }
}
