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
                    Console.WriteLine("\nSelectați o opțiune:");
                    Console.WriteLine("1. Login In");
                    Console.WriteLine("2. Adăugați utilizator");
                    Console.WriteLine("3. Ieșire");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Introduceți Email: ");
                            var email = Console.ReadLine();
                            Console.Write("Introduceți Parola: ");
                            var password = Console.ReadLine();
                            authenticatedUser = autoService.Authenticate(email, password);
                            if (authenticatedUser == null)
                            {
                                Console.WriteLine("Autentificarea a eșuat");
                            }
                            break;
                        case "2":
                            Console.Write("Introduceți rol (admin/mecanic): ");
                            var role = Console.ReadLine().ToLower();
                            User user = role == "admin" ? new Administrator("code", "firstName", "lastName", "email", "password") : (User)new Mechanic("code", "firstName", "lastName", "email", "password");
                            user.AddUser(autoService);
                            break;
                        case "3":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Opțiune invalidă.");
                            break;
                    }
                }
                else
                {
                    if (authenticatedUser is Administrator)
                    {
                        Console.WriteLine("\nSelectați o opțiune:");
                        Console.WriteLine("1. Adăugați o nouă cerere");
                        Console.WriteLine("2. Vizualizați toate cererile");
                        Console.WriteLine("3. Vizualizați toate comenzile de piese");
                        Console.WriteLine("4. Finalizați o comandă de piese");
                        Console.WriteLine("5. Salvați starea");
                        Console.WriteLine("6. Log Out");
                        Console.WriteLine("7. Ieșire");

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
                                Console.WriteLine("Opțiune invalidă. Vă rugăm să încercați din nou.");
                                break;
                        }
                    }
                    else if (authenticatedUser is Mechanic)
                    {
                        Console.WriteLine("\nSelectați o opțiune:");
                        Console.WriteLine("1. Preluați următoarea cerere");
                        Console.WriteLine("2. Investigați problema");
                        Console.WriteLine("3. Adăugați o comandă de piese");
                        Console.WriteLine("4. Rezolvați problema");
                        Console.WriteLine("5. Vizualizați toate comenzile de piese");
                        Console.WriteLine("6. Salvați starea");
                        Console.WriteLine("7. Log Out");
                        Console.WriteLine("8. Ieșire");

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
                                Console.WriteLine("Opțiune invalidă. Vă rugăm să încercați din nou.");
                                break;
                        }
                    }
                }
            }
        }
    }
}