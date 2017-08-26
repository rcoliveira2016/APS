using SpotifyAPI.Example.Interface.Core;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotifyAPI.Example.ControlApi
{
    public sealed class SpotifyApi : IApi
    {
        private SpotifyWebAPI spotifyWebApi;
        private PrivateProfile privateProfile;

        public async void Login()
        {
            WebAPIFactory webApiFactory = new WebAPIFactory(
                "http://localhost",
                8000,
                "26d287105e31491889f3cd293d85bfea",
                Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
                Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
                Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState);

            try
            {
                spotifyWebApi = await webApiFactory.GetWebApi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task<PrivateProfile> GetUserProflie()
        {
            if(privateProfile==null)
                privateProfile = await spotifyWebApi.GetPrivateProfileAsync();
            return privateProfile;
        }

        public async Task<List<FullTrack>> GetSavedTracks()
        {
            Paging<SavedTrack> savedTracks = await spotifyWebApi.GetSavedTracksAsync();
            List<FullTrack> list = savedTracks.Items.Select(track => track.Track).ToList();

            while (savedTracks.Next != null)
            {
                savedTracks = await spotifyWebApi.GetSavedTracksAsync(20, savedTracks.Offset + savedTracks.Limit);
                list.AddRange(savedTracks.Items.Select(track => track.Track));
            }

            return list;
        }

        public async Task<List<SimplePlaylist>> GetPlaylists()
        {
            PrivateProfile _profile = GetUserProflie().Result;
            Paging<SimplePlaylist> playlists = spotifyWebApi.GetUserPlaylists(_profile.Id);
            List<SimplePlaylist> list = playlists.Items.ToList();

            while (playlists.Next != null)
            {
                playlists = spotifyWebApi.GetUserPlaylists(_profile.Id, 20, playlists.Offset + playlists.Limit);
                list.AddRange(playlists.Items);
            }

            return list;
        }

    }

}
