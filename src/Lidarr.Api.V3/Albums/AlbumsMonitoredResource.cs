using System;
using System.Collections.Generic;

namespace Lidarr.Api.V3.Albums
{
    public class AlbumsMonitoredResource
    {
        public List<int> AlbumIds { get; set; }
        public bool Monitored { get; set; }
    }
}
