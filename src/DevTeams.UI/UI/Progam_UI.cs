using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class Program_UI
{
    private readonly Developer_Repository _dRepo = new Developer_Repository();
    private readonly DevTeams_Repository _tRepo = new DevTeams_Repository();
    public void Run()
    {
        SeedData();
        RunApplication();
    }
    private void RunApplication()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            System.Console.WriteLine("=== Welcome to Komodo Insurance ===");
            System.Console.WriteLine(" Please Make a Selection \n" +
            "=== Developer Database ===\n" +
            "1. Add Developers to Database\n" +
            "2. View All Developers \n " +
            "3. View Developers By ID\n" +
            "4. Update Developer Data\n" +
            "5. Delete Developer Data\n" +
            "-----------------------------\n" +
            " === DevTeam Database\n" +
            "6. Add DevTeam to Database\n" +
            "7. View all DevTeams \n" +
            "8. View DevTeam By ID\n" +
            "9. Update DevTeam Data\n" +
            "10. Delete DevTeam Data\n" +
            "-----------------------\n" +
            "11. Close Application\n");

            var userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    AddDevelopersToDatabase();
                    break;
                case "2":
                    ViewAllDevelopers();
                    break;
                case "3":
                    ViewDeveloperByID();
                    break;
                case "4":
                    UpdateDeveloperData();
                    break;
                case "5":
                    DeleteDeveloperData();
                    break;
                case "6":
                    AddDevTeamToDatabase();
                    break;
                case "7":
                    ViewAllDevTeams();
                    break;
                case "8":
                    ViewDevTeamByID();
                    break;
                case "9":
                    UpdateDevTeamData();
                    break;
                case "10":
                    DeleteDevTeamData();
                    break;
                case "11":
                    isRunning = CloseApplication();
                    break;
                default:
                    System.Console.WriteLine("Invalid Selection");
                    PressAnyKeyToContinue();
                    break;
            }
        }
    }

    private void AddDevelopersToDatabase()
        {
        Console.Clear ();
        var newDeveloper = new Developer();
        System.Console.WriteLine("=== Developer Enlisting Form ===\n");

        System.Console.WriteLine("Please Enter a Developer First Name:");
        newDeveloper.FirstName = Console.ReadLine ();

        System.Console.WriteLine("Please Enter a Developer Last Name:");
        newDeveloper.LastName = Console.ReadLine();
        System.Console.WriteLine("Does this user have PluralSight (y/n)");
            var userInputHP= Console.ReadLine();
            if (userInputHP == "Y".ToLower())
            {
                newDeveloper.HasPluralSight = true;
            }
            else
            {
                newDeveloper.HasPluralSight = false;
            }
        bool isSuccessful = _dRepo.AddDeveloper(newDeveloper);
        if (isSuccessful)
        {
            System.Console.WriteLine($"{newDeveloper.FirstName} - {newDeveloper.LastName} was added to the Database.");
        }
        else 
        {
            System.Console.WriteLine("Developer failed to be added to the Database.");
        }
        PressAnyKeyToContinue();
    }

    private void PressAnyKeyToContinue()
    {
        System.Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private bool CloseApplication()
    {
        Console.Clear();
        System.Console.WriteLine("Thank You");
        PressAnyKeyToContinue();
        return false;
    }

    private void DeleteDevTeamData()
    {
        Console.Clear();
        System.Console.WriteLine("=== DevTeam Removal Page===");

        var devTeams = _tRepo.GetAllDevTeams();
        foreach (DevTeam devTeam in devTeams)
        {
            DisplayDevTeamListing(devTeam);
        }

        try
        {
            System.Console.WriteLine("Please selecet a DevTeam but its ID");
            var userInputSelectedDevTeam = int.Parse(Console.ReadLine());
            bool isSuccessful = _tRepo.RemoveDevTeamFromDatabase(userInputSelectedDevTeam);
            if (isSuccessful)
            {
                System.Console.WriteLine("DevTeam was successfully deleted!");
            }
            else
            {
                System.Console.WriteLine("DevTeam failed to be deleted!");
            }
        }
        catch
        {
            System.Console.WriteLine("Sorry, invalid selection!");
        }
            PressAnyKeyToContinue();
    }


    private void UpdateDevTeamData()
    {
        Console.Clear();
        var avialDevTeams = _tRepo.GetAllDevTeams();
        foreach (var devTeam in avialDevTeams)
        {
            DisplayDevTeamListing(devTeam);
        }
        System.Console.WriteLine("Please enter a valid DevTeam ID");
        var userInputDevTeamID = int.Parse(Console.ReadLine());
                                        //this method gives me an existing devTeam...
        var userInputSelectedDevTeam = _tRepo.GetDevTeamByID(userInputDevTeamID);

        if (userInputSelectedDevTeam != null)
        {
            Console.Clear();
            var newDevTeam = new DevTeam();
            var currentDevelopers = _dRepo.GetAllDevelopers();

            System.Console.WriteLine("Please enver a DevTeam Name:");
            newDevTeam.Name = Console.ReadLine();
            bool hasAssignedDevelopers = false;
            while (!hasAssignedDevelopers)
            {
                System.Console.WriteLine("Do you have any Developers? y/n");
                var userInputHasDevelopers = Console.ReadLine();
                if (userInputHasDevelopers == "Y".ToLower())
                {
                    foreach (var developer in currentDevelopers)
                    {
                        System.Console.WriteLine($"{developer.ID} {developer.FirstName} {developer.LastName}");
                    }
                    var userInputDeveloperelection = int.Parse(Console.ReadLine());
                    var selectedDeveloper = _dRepo.GetDeveloperByID(userInputDeveloperelection);

                    if (selectedDeveloper != null)
                    {
                        newDevTeam.Developers.Add(selectedDeveloper);
                        currentDevelopers.Remove(selectedDeveloper);
                    }
                    else
                    {
                        System.Console.WriteLine($"Sorry, the developer with the ID: {userInputDevTeamID} dosen't exist.");
                    }
                }
                else
                {
                    hasAssignedDevelopers = true;
                }
            }
            var isSuccessful =_tRepo.UpdateDevTeamData(userInputSelectedDevTeam.ID, newDevTeam);
            if (isSuccessful)
            {
                System.Console.WriteLine("Success!");
            }
            else
            {
                System.Console.WriteLine("Fail!");
            }
        }
            else
            {
                System.Console.WriteLine($"Sorry the DevTeam with the ID :{userInputDevTeamID} does not exist");
            }
            PressAnyKeyToContinue();
    }

    
    private void ViewDevTeamByID()
    {
        Console.Clear();
        System.Console.WriteLine("=== DevTeam Details ===");

        var devTeams=_tRepo.GetAllDevTeams();
        foreach (DevTeam devTeam in devTeams)
        {
            DisplayDevTeamListing(devTeam);
        }

        try
        {
            System.Console.WriteLine("Please select a DevTeam by its ID:");
            var userInputSelectedDevTeam = int.Parse (Console.ReadLine());
            var selectedDevTeam = _tRepo.GetDevTeamByID(userInputSelectedDevTeam);
            if (selectedDevTeam !=null)
            {
                DisplayDevTeamDetails(selectedDevTeam);
            }
            else
            {
                System.Console.WriteLine($"Sorry the DevTeam with the ID: {userInputSelectedDevTeam} does not exist.");
            }
        }
        catch
        {
            System.Console.WriteLine("Sorry, invalid selection");
        }

        PressAnyKeyToContinue();
    }

    private void DisplayDevTeamDetails(DevTeam selectedDevTeam)
    {
        Console.Clear();
        System.Console.WriteLine($"DevTeamID: {selectedDevTeam.ID}\n"+
        $"DevTeamName: {selectedDevTeam.Name}\n");

        System.Console.WriteLine("--Developers--");
        if (selectedDevTeam.Developers.Count > 0)
        {
            foreach (var developer in selectedDevTeam.Developers)
            {
                DisplayDeveloperInfo(developer);
            }
        }
        else
        {
            System.Console.WriteLine("There are no Developers");
        }
        PressAnyKeyToContinue();
    }


    private void ViewAllDevTeams()
    {
        Console.Clear();
        System.Console.WriteLine("=== DevTeam Listing ===\n");
        var devTeamsInDb = _tRepo.GetAllDevTeams();

        foreach (var devTeam in devTeamsInDb)
        {
            DisplayDevTeamListing(devTeam);
        }
        PressAnyKeyToContinue();
    }

    private void DisplayDevTeamListing(DevTeam devTeam)
    {
        System.Console.WriteLine($"DevTeamID: {devTeam.ID}\n"+
        $"DevTeamName: {devTeam.Name}\n"+
        "---------------------------------\n");
    }

    private void AddDevTeamToDatabase()
    {
        Console.Clear();
        var newDevTeam = new DevTeam();
        
        var currentDevelopers = _dRepo.GetAllDevelopers();

        System.Console.WriteLine("Please enter a DevTeam: ");
        newDevTeam.Name = Console.ReadLine ();

        bool hasAssignedDevelopers=false;
        while (!hasAssignedDevelopers)
        {
            System.Console.WriteLine("Do you have Developers? y/n");
            var userInputHasDevelopers = Console.ReadLine();

            if (userInputHasDevelopers == "Y".ToLower())
            {
                foreach (var developer in currentDevelopers)
                {
                    System.Console.WriteLine($"{developer.ID} {developer.FirstName} {developer.LastName}");
                }
                var userInputDeveloperSelection = int.Parse (Console.ReadLine());
                var selectedDeveloper = _dRepo.GetDeveloperByID(userInputDeveloperSelection);

                if (selectedDeveloper != null)
                {
                    newDevTeam.Developers.Add(selectedDeveloper);
                    currentDevelopers.Remove(selectedDeveloper);
                }
                else
                {
                    System.Console.WriteLine($"Sorry, the developer with the ID: {userInputDeveloperSelection} dosen't exist.");
                }
            }
            else
            {
                hasAssignedDevelopers = true;
            }
        }
        bool isSuccessful = _tRepo.AddDevTeamToDatabase(newDevTeam);
        if (isSuccessful)
        {
            System.Console.WriteLine($"DevTeam: {newDevTeam.Name} was added to the Database");
        }
        else
        {
            System.Console.WriteLine("DevTeam failed to be added to the Database");
        }
        PressAnyKeyToContinue();
    }

    private void DeleteDeveloperData()
    {
        Console.Clear();
        System.Console.WriteLine("=== Devevloper Removal Page===");

        var developers = _dRepo.GetAllDevelopers();
        foreach (Developer developer in developers)
        {
            DisplayDeveloperListing(developer);
        }

        try
        {
            System.Console.WriteLine("Please selecet a Develpoer by its ID");
            var userInputSelectedDeveloper = int.Parse(Console.ReadLine());
            bool isSuccessful = _dRepo.RemoveDeveloperFromDatabase(userInputSelectedDeveloper);
            if (isSuccessful)
            {
                System.Console.WriteLine("Developer was successfully deleted!");
            }
            else
            {
                System.Console.WriteLine("Developer failed to be deleted!");
            }
        }
        catch
        {
            System.Console.WriteLine("Sorry, invalid selection!");
        }
            PressAnyKeyToContinue();
    }

    private void DisplayDeveloperListing(Developer developer)
    {
        {
        System.Console.WriteLine($"DeveloperID: {developer.ID}\n"+
        $"DeveleoperName: {developer.FirstName} {developer.LastName}\n"+
        "---------------------------------------------\n");
        }
    }

    private void UpdateDeveloperData()
    {
        Console.Clear();
        var avialDeveloperss = _dRepo.GetAllDevelopers();
        foreach (var developer in avialDeveloperss)
        {
            DisplayDeveloperListing(developer);
        }
        System.Console.WriteLine("Please enter a valid Developer ID");
        var userInputDeveloperID = int.Parse(Console.ReadLine());
        var userInputSelectedDeveloper = _dRepo.GetDeveloperByID(userInputDeveloperID);

        if (userInputSelectedDeveloper != null)
        {
            Console.Clear();
            var newDeveloper = new Developer();
            var currentDevelopers = _dRepo.GetAllDevelopers();

            System.Console.WriteLine("Please enter a Developer First Name:");
            newDeveloper.FirstName = Console.ReadLine();
            System.Console.WriteLine("Please enter a Developer Last Name:");
            newDeveloper.LastName = Console.ReadLine();
            System.Console.WriteLine("Does this user have PluralSight (y/n)");
            var userInputHP= Console.ReadLine();
            if (userInputHP == "Y".ToLower())
            {
                newDeveloper.HasPluralSight = true;
            }
            else
            {
                newDeveloper.HasPluralSight = false;
            }

            
            var isSuccessful =_dRepo.UpdateDeveloper(userInputSelectedDeveloper.ID, newDeveloper);
            if (isSuccessful)
            {
                System.Console.WriteLine("Success!");
            }
            else
            {
                System.Console.WriteLine("Fail!");
            }
        }
            else
            {
                System.Console.WriteLine($"Sorry the DevTeam with the ID :{userInputDeveloperID} does not exist");
            }
            PressAnyKeyToContinue();
    }
    private void ViewDeveloperByID()
    {
        Console.Clear();
        System.Console.WriteLine("===Developer Detail Menu===\n");
        System.Console.WriteLine("Please Enter Developer ID: \n");
        var userInputDeveloperID = int.Parse(Console.ReadLine());

        var developer = _dRepo.GetDeveloperByID(userInputDeveloperID);

        if (developer !=null)
        {
            DisplayDeveloperInfo(developer);
        }
        else
        {
            System.Console.WriteLine($"The Developer with the ID: {userInputDeveloperID} Does not exist.");
        }
        PressAnyKeyToContinue();
    }

    private void DisplayDeveloperInfo(Developer developer)
    {
        {
        System.Console.WriteLine($"DeveloperID: {developer.ID}\n"+
        $"DeveleoperName: {developer.FirstName} {developer.LastName}\n"+
        $"HasPluralSight: {developer.HasPluralSight}\n"+
        "---------------------------------------------\n");
        }
    }
    private void ViewAllDevelopers()
    {
        Console.Clear();
        List<Developer> developersInDb = _dRepo.GetAllDevelopers();
        if (developersInDb.Count > 0)
        {
            foreach (Developer developer in developersInDb)
            {
                DisplayDeveloperInfo(developer);
            }
        }
        else
        {
            System.Console.WriteLine("There are no developers");
        }
        PressAnyKeyToContinue();
    }


    private void SeedData()
    {
        var johnny = new Developer ("Johnny", "Horne");
        var lauren = new Developer ("Lauren", "Angel");
        var michelle = new Developer ("Michelle", "Gerber");
        var petra = new Developer ("Petra", "Richard");
        var oliver = new Developer ("Oliver", "McGraw");

        _dRepo.AddDeveloper(johnny);
        _dRepo.AddDeveloper(lauren);
        _dRepo.AddDeveloper(michelle);
        _dRepo.AddDeveloper(petra);
        _dRepo.AddDeveloper(oliver);

        var devTeamA = new DevTeam ("Dream Team!!",
        new List<Developer>
        
    {
        lauren,
        petra,
        michelle
    });

    var devTeamB = new DevTeam ("Soft Bois",
    new List<Developer>
    {
        johnny,
        oliver
    });
    _tRepo.AddDevTeamToDatabase(devTeamA);
    _tRepo.AddDevTeamToDatabase(devTeamB);
    }
}
