using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ShowTrackerService" in code, svc and config file together.
public class ShowTrackerService : IShowTrackerService
{
    ShowTrackerEntities db = new ShowTrackerEntities();
    public void ShowTrackerServices()
    {

    }

    //A list of all artists (just the names)
    public List<string> GetArtists()
    {
        var artists = from a in db.Artists
                      orderby a.ArtistName
                      select new { a.ArtistName };
        List<string> listArtists = new List<string>();
        foreach (var art in artists)
        {
            listArtists.Add(art.ArtistName.ToString());
        }
        return listArtists;
    }

    //list of all shows (just the names)
    public List<string> GetShows()
    {
        var shows = from s in db.Shows
                      orderby s.ShowName
                      select new { s.ShowName };
        List<string> listShows = new List<string>();
        foreach (var s in shows)
        {
            listShows.Add(s.ShowName.ToString());
        }
        return listShows;
    }

    //list of all venues (just the names of the venues)
    public List<string> GetVenues()
    {
        var venues = from v in db.Venues
                    orderby v.VenueName
                    select new { v.VenueName };
        List<string> listVenues = new List<string>();
        foreach (var v in venues)
        {
            listVenues.Add(v.VenueName.ToString());
        }
        return listVenues;
    }

    //list of all shows for a particular venue (for this list the showname, show date,  show start time)
    public List<ShowInfo> GetShowsByVenue(string venueName)
    {
        var shows = from s in db.Shows
                    orderby s.ShowName
                    where s.Venue.VenueName.Equals(venueName)
                    select new { s.ShowName, s.ShowDate, s.ShowTime, s.Venue.VenueName };
        List<ShowInfo> listShows = new List<ShowInfo>();
        foreach (var s in shows)
        {
            ShowInfo show = new ShowInfo();
            show.name = s.ShowName;
            show.date = s.ShowDate.ToString();
            show.time = s.ShowTime.ToString();
            show.venueName = s.VenueName.ToString();
            listShows.Add(show);
        }
        return listShows;
    }

    //list of all shows for a particular artist (Show name, show date, show time and venue Name)
    public List<ShowInfo> GetShowsByArtist(string artistName)
    {
        var shows = from s in db.Shows
                    from sd in s.ShowDetails
                    orderby s.ShowName
                    where sd.Artist.ArtistName.Equals(artistName)
                    select new { s.ShowName, s.ShowDate, s.ShowTime, s.Venue.VenueName, sd.Artist.ArtistName };
        List<ShowInfo> listShows = new List<ShowInfo>();
        foreach (var s in shows)
        {
            ShowInfo show = new ShowInfo();
            show.name = s.ShowName;
            show.date = s.ShowDate.ToString();
            show.time = s.ShowTime.ToString();
            show.venueName = s.VenueName.ToString();
            show.artistName = s.ArtistName.ToString();
            listShows.Add(show);
        }
        return listShows;
    }

    

    
    
}

