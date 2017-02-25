using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeejayApp.Models
{
    public class Playlist : EntityBase
    {
        public int Id { get; set; }
        public string PlaylistName { get; set; }
    }
}