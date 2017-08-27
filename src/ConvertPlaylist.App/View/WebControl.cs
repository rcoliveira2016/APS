using ConvertPlaylist.App.ControlApi;
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

        #region Attribute
        private SpotifyApi spotifyApi; 
        #endregion

        #region Delegete
        delegate void SetTextCallback(string texto);
        delegate void SetSimplePlaylistCallback(ICollection<SimplePlaylist> list);
        delegate void SetFullTrackCallback(ICollection<FullTrack> list);
        #endregion

        #region Construtor
        public WebControl()
        {
            InitializeComponent();
            spotifyApi = new SpotifyApi();
        } 
        #endregion

        #region Privete Methods
        private void callBack(object value, Delegate action)
        {
            this.Invoke(action, new object[] { value });
        }

        private async void RunAuthentication()
        {
            spotifyApi.Login();

            var profileTask = await spotifyApi.GetUserProflie();

            this.callBack(profileTask.DisplayName, new SetTextCallback((string texto) =>
            {
                displayNameLabel.Text = texto;
                tspBtnLogarSpotify.Enabled = false;
                tspBtnExportPlaylist.Enabled = true;
            }));

            this.callBack(profileTask.Country, new SetTextCallback((string texto) =>
                    countryLabel.Text = texto
                ));

            this.callBack(profileTask.Email, new SetTextCallback((string texto) =>
                    emailLabel.Text = texto
                ));

            this.callBack(profileTask.Product, new SetTextCallback((string texto) =>
                    accountLabel.Text = texto
                ));


            if (profileTask.Images != null && profileTask.Images.Count > 0)
            {
                using (WebClient wc = new WebClient())
                {
                    byte[] imageBytes = await wc.DownloadDataTaskAsync(new Uri(profileTask.Images[0].Url));
                    using (MemoryStream stream = new MemoryStream(imageBytes))
                        avatarPictureBox.Image = Image.FromStream(stream);
                }
            }

        }

        private async void RunPlaylist()
        {
            var playlists = await spotifyApi.GetPlaylists();
            this.callBack(playlists, new SetSimplePlaylistCallback((ICollection<SimplePlaylist> lisa) =>
            {


                playlists.ForEach(playlist => {
                    var users = playlist.Collaborative ? playlist.Owner.Id :playlist.Owner.DisplayName;
                    playlistsListBox.Items.Add(new ListViewItem()
                    {
                        Text = playlist.Name,
                        SubItems = { users }
                    });
                });

            }));
        }

        private async void RunSavedTrack()
        {
            var _savedTracks = await spotifyApi.GetSavedTracks();
            this.callBack(_savedTracks, new SetFullTrackCallback((ICollection<FullTrack> lisa) =>
            {
                _savedTracks.ToList().ForEach(track => savedTracksListView.Items.Add(new ListViewItem()
                {
                    Text = track.Name,
                    SubItems = { string.Join(",", track.Artists.Select(source => source.Name)), track.Album.Name }
                }));

                this.tspLabelState.Text = "";
            }));
        } 
        #endregion

        #region Events Forms
        private void tspBtnLogarSpotify_Click(object sender, EventArgs e)
        {
            if (!spotifyApi.logged)
            {
                this.tspLabelState.Text = "Carregando..";
                Task.Run(() => RunAuthentication()).Wait();
                Task.Run(() => RunPlaylist()).Wait();
                Task.Run(() => RunSavedTrack()).Wait();                
            }
        }

        private void tspBtnExportPlaylist_Click(object sender, EventArgs e)
        {
            var view = new ExportPlaylist(spotifyApi);

            view.ShowDialog();
        }
        #endregion
    }
}