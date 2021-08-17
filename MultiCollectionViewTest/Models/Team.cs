using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCollectionViewTest.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool Active { get; set; }
        #region non-mapped properties

        #endregion
    }
}
