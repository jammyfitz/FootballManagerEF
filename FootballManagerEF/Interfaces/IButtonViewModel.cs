using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
        public interface IButtonViewModel
        {
            void UpdateButtonClicked();
            void SaveDataGrid();
            void SendErrorToUser();
            string GetErrorMessageOnUpdate();
            List<PlayerMatch> GetPlayerMatchesToInsert();
            bool DataGridIsValid();
            bool MoreThanMaxPlayersInATeam();
            bool GridRowIncomplete();
            bool RowsHavePlayerButNoTeam();
            bool RowsHaveTeamButNoPlayer();
            bool PlayerAppearsMoreThanOnce();
        }

}
