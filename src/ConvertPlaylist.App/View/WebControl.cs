using SpotifyAPI.Example.ControlApi;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace ConvertPlaylist.App.View
{
    public partial class WebControl : UserControl
    {
        private SpotifyApi spotifyApi;


        delegate void SetTextCallback(string texto);
        delegate void SetSimplePlaylistCallback(ICollection<SimplePlaylist> list);


        public WebControl()
        {
            InitializeComponent();
            spotifyApi = new SpotifyApi();
        }

        /*private async void InitialSetup()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(InitialSetup));
                return;
            }
            
            _profile = await _spotify.GetPrivateProfileAsync();

            _savedTracks = GetSavedTracks();
            savedTracksCountLabel.Text = _savedTracks.Count.ToString();
            _savedTracks.ForEach(track => savedTracksListView.Items.Add(new ListViewItem()
            {
                Text = track.Name,
                SubItems = { string.Join(",", track.Artists.Select(source => source.Name)), track.Album.Name }
            }));

            _playlists = GetPlaylists();
            playlistsCountLabel.Text = _playlists.Count.ToString();
            _playlists.ForEach(playlist => playlistsListBox.Items.Add(playlist.Name));

            displayNameLabel.Text = _profile.DisplayName;
            countryLabel.Text = _profile.Country;
            emailLabel.Text = _profile.Email;
            accountLabel.Text = _profile.Product;

            if (_profile.Images != null && _profile.Images.Count > 0)
            {
                using (WebClient wc = new WebClient())
                {
                    byte[] imageBytes = await wc.DownloadDataTaskAsync(new Uri(_profile.Images[0].Url));
                    using (MemoryStream stream = new MemoryStream(imageBytes))
                        avatarPictureBox.Image = Image.FromStream(stream);
                }
            }
        }*/


        private void callBack(Delegate action, object value)
        {
            this.Invoke(action, new object[] { value });
        }

        private async void RunAuthentication()
        {
            spotifyApi.Login();

            var profileTask = await spotifyApi.GetUserProflie();

            this.callBack(new SetTextCallback((string texto) => displayNameLabel.Text = texto), profileTask.DisplayName);


            /*countryLabel.Text = profileTask.Country;
            emailLabel.Text = profileTask.Email;
            accountLabel.Text = profileTask.Product;

            if (profileTask.Images != null && profileTask.Images.Count > 0)
            {
                using (WebClient wc = new WebClient())
                {
                    byte[] imageBytes = await wc.DownloadDataTaskAsync(new Uri(profileTask.Images[0].Url));
                    using (MemoryStream stream = new MemoryStream(imageBytes))
                        avatarPictureBox.Image = Image.FromStream(stream);
                }
            }

            var _savedTracks = await spotifyApi.GetSavedTracks();
            savedTracksCountLabel.Text = _savedTracks.Count.ToString();
            _savedTracks.ForEach(track => savedTracksListView.Items.Add(new ListViewItem()
            {
                Text = track.Name,
                SubItems = { string.Join(",", track.Artists.Select(source => source.Name)), track.Album.Name }
            }));*/

            var playlists = await spotifyApi.GetPlaylists();

            var d = new SetSimplePlaylistCallback((ICollection<SimplePlaylist> lisa) => {
                playlistsCountLabel.Text = playlists.Count.ToString();
                playlists.ForEach(playlist => playlistsListBox.Items.Add(playlist.Name));
            });

            this.callBack(d, playlists);
            

        }

        private void DefinirTexto(string texto)
        {
            displayNameLabel.Text = texto;
        }

        private void tspBtnLogarSpotify_Click(object sender, EventArgs e)
        {
            Task.Run(() => RunAuthentication()).Wait();
        }
    }
}