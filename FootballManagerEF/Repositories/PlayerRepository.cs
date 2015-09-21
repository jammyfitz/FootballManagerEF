using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace FootballManagerEF.Repositories
{
    public class PlayerRepository : IPlayerRepository, IDisposable
    {
        private FootballEntities context;

        public PlayerRepository(FootballEntities context)
        {
            this.context = context;
        }

        public ObservableCollection<Player> GetActivePlayers()
        {
            return new ObservableCollection<Player>(context.Players.Where(x => x.Active == true).OrderBy(x => x.PlayerName).ToList());
        }

        public ObservableCollection<Player> GetAllPlayers()
        {
            return new ObservableCollection<Player>(context.Players.OrderBy(x => x.PlayerName).ToList());
        }

        public Player GetPlayerByID(int playerId)
        {
            return context.Players.Find(playerId);
        }

        public bool InsertPlayers(ObservableCollection<Player> players)
        {
            foreach (Player player in players)
                context.Players.Add(player);

            return true;
        }

        public List<string> GetEmailAddresses(List<int?> playerIDs)
        {
            List<string> emailAddresses = new List<string>();

            foreach (int playerId in playerIDs)
            {
                string emailAddress = GetPlayerByID(playerId).EmailAddress;

                if (!string.IsNullOrEmpty(emailAddress))
                    emailAddresses.Add(emailAddress);
            }
                
            return emailAddresses;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
