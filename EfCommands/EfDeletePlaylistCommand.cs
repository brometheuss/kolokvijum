using Application.Commands;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfDeletePlaylistCommand : EfBaseCommand, IDeletePlaylistCommand
    {
        public EfDeletePlaylistCommand(ChinookContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var playlist = Context.Playlist.Find(request);

            if (playlist == null)
                throw new Exception("Entity not found.");

            if (Context.PlaylistTrack.Any(pt => pt.PlaylistId == request))
                throw new Exception("Playlist has tracks in it.");

            Context.Remove(playlist);
            Context.SaveChanges();
        }
    }
}
