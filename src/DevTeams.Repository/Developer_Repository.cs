
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Developer_Repository
{
        private readonly List<Developer> _developerDatabase= new List<Developer>();
        private int _count;
    public bool AddDeveloper(Developer developer)
    {
        if (developer != null)
        {
            _count++;
            developer.ID = _count;
            _developerDatabase.Add(developer);
            return true;
        }
        else
        {
            return false;
        }
    }

    

    public Developer GetDeveloperByID(int userInputDeveloperID)
    {
        //_developerDatabase is a collection, the only way to get to the data in the collection is to loop through it
        foreach (var dev in _developerDatabase)
        {
            //dev represtents one developer inside of the _developerDatabase
            if (dev.ID == userInputDeveloperID)
            {
                return dev;
            }
        }
        return null;
    }

    public List<Developer> GetAllDevelopers()
    {
        return _developerDatabase;
    }

    public bool RemoveDeveloperFromDatabase(int userInputSelectedDeveloper)
    {
        var devForDeletion = GetDeveloperByID(userInputSelectedDeveloper);
        if (devForDeletion != null)
        {
            _developerDatabase.Remove(devForDeletion);
            return true;
        }
        return false;
    }

    public bool UpdateDeveloper (int userInputSelectedDeveloper, Developer newDeveloperData)
    {
        var devForUpdate = GetDeveloperByID(userInputSelectedDeveloper);
        if (devForUpdate != null)
        {
            devForUpdate.FirstName = newDeveloperData.FirstName;
            devForUpdate.LastName = newDeveloperData.LastName;
            devForUpdate.HasPluralSight= newDeveloperData.HasPluralSight;
            return true;
        }
        return false;
    }
}

