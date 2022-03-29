using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//pascal casing first letter of every word is capiltal class, methods props
public class DevTeams_Repository
{
    private readonly List<DevTeam> _devTeamDatabase = new List<DevTeam>();
    private int _count = 0;
    public bool AddDevTeamToDatabase(DevTeam devTeam)
    {
        if (devTeam != null)
        {
            _count++;

            devTeam.ID = _count;

            _devTeamDatabase.Add(devTeam);

            return true;
        }
        else
        {
            return false;
        }
    }

    public List<DevTeam> GetAllDevTeams ()
    {
        return _devTeamDatabase;
    }

    
    public DevTeam GetDevTeamByID(int id)
    {
        foreach (DevTeam devTeam in _devTeamDatabase)
        {
            if (devTeam.ID == id)
            {
                return devTeam;
            }
        } 
        return null;
    }

    
    public bool UpdateDevTeamData(int devTeamID, DevTeam newdevTeamData)
    {
    
        DevTeam oldDevTeamData = GetDevTeamByID(devTeamID);

        if (oldDevTeamData != null) 
        {
            oldDevTeamData.Name = newdevTeamData.Name;
            oldDevTeamData.Developers = newdevTeamData.Developers;
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool RemoveDevTeamFromDatabase(int id)
    {
        var devTeam = GetDevTeamByID(id);
        if (devTeam != null)
        {
            _devTeamDatabase.Remove(devTeam);
            return true;
        }
        else
        {
            return false;
        }

    }

    public object UpdateDevTeamData(object iD, Developer newDeveloper)
    {
        throw new NotImplementedException();
    }
}