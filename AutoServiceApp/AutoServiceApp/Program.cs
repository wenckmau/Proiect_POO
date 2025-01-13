using System;
using System.IO;

namespace AutoServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "state.json");
            var autoService = new AutoService();

            autoService.LoadState(defaultFilePath);

            bool exit = false;
            User authenticatedUser = null;

            while (!exit)
            {
                if (authenticatedUser == null)
                {
                    Console.WriteLine("\nAlegeti o optiune:");
                    Console.WriteLine("1. Log in");
                    Console.WriteLine("2. Adaugare user");
                    Console.WriteLine("3. Exit");

                    var alegere = Console.ReadLine();

                    switch (alegere)
                    {
                        case "1":
                            authenticatedUser = LogIn(autoService);
                            break;
                        case "2":
                            AddUser(autoService);
                            break;
                        case "3":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Alegere invalida. Incercati din nou.");
                            break;
                    }
                }
                else
                {
                    if (authenticatedUser is Administrator)
                    {
                        Console.WriteLine("\nAlegeti o optiune:");
                        Console.WriteLine("1. Adaugati o noua cerere");
                        Console.WriteLine("2. Toate cererile");
                        Console.WriteLine("3. Vizualizare parti auto");
                        Console.WriteLine("4. Finalizare comanda de piese");
                        Console.WriteLine("5. Salvare ");
                        Console.WriteLine("6. Log out");
                        Console.WriteLine("7. Exit");

                        var alegere = Console.ReadLine();

                        switch (alegere)
                        {
                            case "1":
                                ((Administrator)authenticatedUser).AdaugaCerere(autoService, authenticatedUser);
                                break;
                            case "2":
                                ((Administrator)authenticatedUser).VizualizeazaCereri(autoService, authenticatedUser);
                                break;
                            case "3":
                                ((Administrator)authenticatedUser).VizualizeazaComenziPiese(autoService, authenticatedUser);
                                break;
                            case "4":
                                ((Administrator)authenticatedUser).FinalizeazaComandaPiese(autoService, authenticatedUser);
                                break;
                            case "5":
                                autoService.SaveState(defaultFilePath);
                                break;
                            case "6":
                                authenticatedUser = null;
                                break;
                            case "7":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Optiune invalida. Incercati din nou!");
                                break;
                        }
                    }
                    else if (authenticatedUser is Mechanic)
                    {
                        Console.WriteLine("\nAlegeti o optiune:");
                        Console.WriteLine("1. Luati urmatoarea cerere");
                        Console.WriteLine("2. Investigare problema");
                        Console.WriteLine("3. Adaugare comanda de piese");
                        Console.WriteLine("4. Rezolvati problema");
                        Console.WriteLine("5. Vizualizare comenzi de piese");
                        Console.WriteLine("6. Salvare");
                        Console.WriteLine("7. Log out");
                        Console.WriteLine("8. Exit");

                        var Alg = Console.ReadLine();

                        switch (Alg)
                        {
                            case "1":
                                ((Mechanic)authenticatedUser).PreiaCerere(autoService);
                                break;
                            case "2":
                                ((Mechanic)authenticatedUser).InvestigheazaProblema();
                                break;
                            case "3":
                                ((Mechanic)authenticatedUser).AdaugaComandaPiese(autoService, authenticatedUser);
                                break;
                            case "4":
                                ((Mechanic)authenticatedUser).RezolvaProblema(autoService);
                                break;
                            case "5":
                                ((Mechanic)authenticatedUser).VizualizeazaComenziPiese(autoService, authenticatedUser);
                                break;
                            case "6":
                                autoService.SaveState(defaultFilePath);
                                break;
                            case "7":
                                authenticatedUser = null;
                                break;
                            case "8":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Optiune invalida. Incercati din nou!");
                                break;
                        }
                    }
                }
            }
        }

        static User LogIn(AutoService autoService)
        {
            Console.Write("Introduceti email: ");
            var email = Console.ReadLine();
            Console.Write("Introduceti parola: ");
            var password = Console.ReadLine();

            var user = autoService.Authenticate(email, password);
            if (user != null)
            {
                Console.WriteLine("Autentificare reusita.");
                return user;
            }
            else
            {
                Console.WriteLine("Autentificare nereusita.");
                return null;
            }
        }

        static void AddUser(AutoService autoService)
        {
            Console.Write("Introduceti rolul user-ului (admin/mecanic): ");
            var role = Console.ReadLine().ToLower();
            Console.Write("Introducere cod unic: ");
            var code = Console.ReadLine();
            Console.Write("Introduceti nume: ");
            var firstName = Console.ReadLine();
            Console.Write("Introduceti prenume: ");
            var lastName = Console.ReadLine();
            Console.Write("Introduceti email: ");
            var email = Console.ReadLine();
            Console.Write("Introduceti parola: ");
            var password = Console.ReadLine();

            if (role == "admin")
            {
                var admin = new Administrator(code, firstName, lastName, email, password);
                autoService.AddUser(admin);
                Console.WriteLine("Administrator adaugat cu succes.");
            }
            else if (role == "mecanic")
            {
                var mecanic = new Mechanic(code, firstName, lastName, email, password);
                autoService.AddUser(mecanic);
                Console.WriteLine("Mecanic adaugat cu succes.");
            }
            else
            {
                Console.WriteLine("Rol invalid. Nu a fost gasit user.");
            }
        }
    }
}