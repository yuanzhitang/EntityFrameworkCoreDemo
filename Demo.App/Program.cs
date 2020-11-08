using Demo.Data;
using Demo.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Demo.App
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new DemoContext(null);

            #region 1  Insert One Record
            //var serieA = new League
            //{
            //    Country = "Italy",
            //    Name = "Serie A"
            //};
            //context.Leagues.Add(serieA);
            #endregion

            #region 2  Search One Record and Inset Multi-Records
            var serieA = context.Leagues.Single(x => x.Name == "Serie A");
            var serieB = new League
            {
                Country = "Italy",
                Name = "Serie B"
            };
            var serieC = new League
            {
                Country = "Italy",
                Name = "Serie C"
            };

            var Manlian = new Club
            {
                Name = "AC Manlian",
                City = "Milan",
                DataOfEstablishment = DateTime.Now,
                League = serieA
            };

            context.AddRange(serieB, serieC, Manlian);

            #endregion
            #region Search

            //var italy = "Italy";
            //var leagues = context.Leagues
            //   .Where(x => x.Country == italy)
            //   .ToList();


            //var leaguesContains = context.Leagues
            //   .Where(x => x.Country.Contains("I"))
            //   .ToList();

            //var leaguesLike = context.Leagues
            //    .Where(x =>
            //        EF.Functions.Like(x.Country, "%I%"))
            //    .ToList();

            //var leaguesFirst = context.Leagues
            //  .FirstOrDefault(x =>
            //      EF.Functions.Like(x.Country, "%I%"));

            #endregion

            #region Delete Record

            //var manlian = context.Clubs.Single(x => x.Name == "AC Manlian");

            //Way#1
            //context.Clubs.Remove(manlian);

            //Way#2
            //context.Remove(manlian);

            //Way#3
            //context.Clubs.RemoveRange(manlian, manlian);

            //Way#4
            //context.RemoveRange(manlian, manlian);

            #endregion

            #region Modify Record

            //Way#1
            //var leagueRecordWithTracking = context.Leagues.First();
            //leagueRecordWithTracking.Name = "~";

            //Way#2
            //var league = context.Leagues.AsNoTracking().First();
            //league.Name += "New";
            //context.Leagues.Update(league);

            #endregion


            #region Add Relationship

            //Add Club/Player to SerieA
            //var serieA = context.Leagues.SingleOrDefault(x => x.Name == "Serie A");
            //var juventus = new Club
            //{
            //    League = serieA,
            //    Name = "Juventus",
            //    City = "Torino",
            //    DataOfEstablishment = DateTime.Now,
            //    Players = new List<Player>
            //    {
            //        new Player
            //        {
            //            Name = "C. Ronaldo",
            //            DateOfBirth = new DateTime(1985,1,1)
            //        }
            //    }
            //};


            //Add Player to Club
            //var juventus = context.Clubs.SingleOrDefault(x => x.Name == "Juventus");
            //juventus.Players.Add(new Player
            //{
            //    Name = "Gon",
            //    DateOfBirth = new DateTime(1987,1,2)
            //});


            //Add Resume directly, specify PlayerId directly
            //var resume = new Resume
            //{
            //    PlayerId = 1,
            //    Description = "C.Ronaldo's Resume"
            //};
            //context.Resumes.Add(resume);

            #endregion

            #region Load Relationship

            //Load All data from database
            //var clubs = context.Clubs
            //    .Where(x => x.Name == "A")
            //    .Include(x => x.League)
            //    .Include(x => x.Players)
            //        .ThenInclude(y => y.Resume)
            //    .Include(x => x.Players)
            //        .ThenInclude(y => y.GamePlayers)
            //            .ThenInclude(z => z.Game)
            //    .ToList();


            //var club1 = context.Clubs.Find(1);
            //Load All data from database
            //var info = context.Clubs
            //    .Where(x => x.Id > 0)
            //    .Select(x => new
            //    {
            //        x.Id,
            //        LeagueName = x.League.Name,
            //        x.Name,
            //        Players = x.Players
            //        .Where(p =>
            //        p.DateOfBirth >= new DateTime(1990, 1, 1))
            //    }).ToList();


            //Get a record first, then try to load its related model data
            //var info = context.Clubs.First();
            //context.Entry(info)
            //    .Collection(x => x.Players) // Load collection
            //    .Query()
            //    .Where(x=>x.DateOfBirth>new DateTime(1998,2,4))
            //    .Load();

            //context.Entry(info)
            //    .Reference(x => x.League)   //Load single reference
            //    .Load();


            //LazyLoading


            //Load with multi conditions
            //var data = context.Clubs
            //    .Where(x => x.League.Name.Contains("e"))
            //    .ToList();


            //Player -- GamePlayer -- Game
            //var gamePlayers = context.Set<GamePlayer>()
            //    .Where(x => x.Player.Id > 0)
            //    .ToList();

            #endregion

            #region Modify Relevent Relationship Data

            //Get Club , but update League
            //var club = context.Clubs
            //    .Include(x => x.League)
            //    .First();

            //club.League.Name += "#";



            //Only update itsself, not update relevent table data
            //var game = context.Games
            //    .Include(x => x.GamePlayers)
            //        .ThenInclude(x => x.Player)
            //    .First();

            //var firstPlayer = game.GamePlayers[0].Player;//In real biz case, the data may from JSON
            //firstPlayer.Name += "#";
            //{
            //    using var newContext = new DemoContext();
            //    //newContext.Players.Update(firstPlayer);//Will update all relevent data

            //    //suggest the below one, it will only update Player.
            //    newContext.Entry(firstPlayer).State = EntityState.Modified;
            //    newContext.SaveChanges();
            //}


            //Update intermiddeate table
            //var gamePlayer = new GamePlayer
            //{
            //    GameId = 1,
            //    PlayerId = 3
            //};
            //context.Add(gamePlayer);



            //var game = context.Games.First();
            //game.GamePlayers.Add(new GamePlayer
            //{
            //    PlayerId = 4
            //});

            //Remove a record
            //var gamePlayer = new GamePlayer
            //{
            //    GameId = 1,
            //    PlayerId = 3
            //};
            //context.Remove(gamePlayer);

            //Update Player's Resume
            //var player = context.Players
            //    .Include(x => x.Resume) //We should call Include, otherwise, EF will try to insert a new Resume rather update the exising resume
            //    .OrderBy(x => x.Id)
            //    .Last();

            //player.Resume = new Resume
            //{
            //    Description = "New resume"
            //};

            #endregion

            var count = context.SaveChanges();

            Console.WriteLine(count);

        }
    }
}
