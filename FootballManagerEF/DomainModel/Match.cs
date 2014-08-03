using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace FootballManagerEF.EFModel
{
    public partial class Match : IMatch
    {
        public List<Match> GetMatches()
        {
            using (var footballEntities = new FootballEntities())
            {
                var result = from matches in footballEntities.Matches
                             select matches;
            
                return result.ToList();
            }
        }
    }
}
