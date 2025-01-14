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
                    Console.WriteLine("\nSelectati o optiune:");
                    Console.WriteLine("1. Login In");
                    Console.WriteLine("2. Adaugati utilizator");
                    Console.WriteLine("3. Iesire");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Introduceti Email: ");
                            var email = Console.ReadLine();
                            Console.Write("Introduceti Parola: ");
                            var password = Console.ReadLine();
                            authenticatedUser = autoService.Authenticate(email, password);
                            if (authenticatedUser == null)
                            {
                                Console.WriteLine("Autentificarea a esuat");
                            }
                            break;
                        case "2":
                            Console.Write("Introduceti rol (admin/mecanic): ");
                            var role = Console.ReadLine().ToLower();
                            User user = role switch
                            {
                                "admin" => new Administrator("code", "firstName", "lastName", "email", "password"),
                                "mecanic" => new Mechanic("code", "firstName", "lastName", "email", "password"),
                                _ => null
                            };
                            user.AddUser(autoService);
                            break;
                        case "3":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Optiune invalida.");
                            break;
                    }
                }
                else
                {
                    if (authenticatedUser is Administrator)
                    {
                        Console.WriteLine("\nSelectati o optiune:");
                        Console.WriteLine("1. Adaugati o noua cerere");
                        Console.WriteLine("2. Vizualizati toate cererile");
                        Console.WriteLine("3. Vizualizati toate comenzile de piese");
                        Console.WriteLine("4. Finalizati o comanda de piese");
                        Console.WriteLine("5. Salvati starea");
                        Console.WriteLine("6. Log Out");
                        Console.WriteLine("7. Iesire");

                        var choice = Console.ReadLine();

                        switch (choice)
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
                                Console.WriteLine("Optiune invalida. Va rugam sa incercati din nou.");
                                break;
                        }
                    }
                    else if (authenticatedUser is Mechanic)
                    {
                        Console.WriteLine("\nSelectati o optiune:");
                        Console.WriteLine("1. Preluati urmatoarea cerere");
                        Console.WriteLine("2. Investigati problema");
                        Console.WriteLine("3. Adaugati o comanda de piese");
                        Console.WriteLine("4. Rezolvati problema");
                        Console.WriteLine("5. Vizualizati toate comenzile de piese");
                        Console.WriteLine("6. Salvati starea");
                        Console.WriteLine("7. Log Out");
                        Console.WriteLine("8. Iesire");

                        var choice = Console.ReadLine();

                        switch (choice)
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
                                Console.WriteLine("Optiune invalida. Va rugam sa incercati din nou.");
                                break;
                        }
                    }
                }
            }
        }
    }
}


