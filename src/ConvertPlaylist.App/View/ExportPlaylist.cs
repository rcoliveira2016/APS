using ConvertPlaylist.App.ControlApi;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertPlaylist.App.View
{
    public partial class ExportPlaylist : Form
    {

        #region attributes
        private SpotifyApi spotifyApi; 
        #endregion

        #region Delegate
        delegate void SetSimplePlaylistCallback(ICollection<SimplePlaylist> list);
        delegate void SetStringBoolCallback(string text, bool isTrue);
        delegate void SetCallback();
        delegate void SetListStringCallback(ICollection<string> list);
        #endregion

        #region Construtor
        public ExportPlaylist(SpotifyApi _spotifyApi)
        {
            InitializeComponent();

            spotifyApi = _spotifyApi;
            Task.Run(() => RunView()).Wait();
        }

        #endregion

        #region privete methods        

        public async void RunView()
        {
            var playlists = await spotifyApi.GetPlaylists();
            Invoke(new SetSimplePlaylistCallback((ICollection<SimplePlaylist> list) =>
            {
                list.ToList().ForEach(x =>
                {
                    cbxPlaylist.Items.Add(x);
                });
            }), new object[] { playlists });
        }

        private async void RunCreateList(string idPlaylist, string name)
        {
            var listNameMusic = await spotifyApi.GetFullNamePlaylistFullTracksAll(idPlaylist);

            var youtube = new YoutubeApi();

            if (!listNameMusic.Any())
                return;

            youtube.Login();

            var idNewPlaylist = await youtube.CreatePlaylist(name);

            foreach (var music in listNameMusic)
            {
                var returnAddItem = await youtube.AddItemPlaylist(idNewPlaylist, music);

                var returnMusic = returnAddItem ? music : $"{music} -- não foi encontrada";

                Invoke(new SetStringBoolCallback((string text, bool found) =>
                {
                    ltvAddItens.Items.Add(new ListViewItem()
                    {
                        Text = text,
                        SubItems = { (found ? "Sim" : "Não") }
                    });

                }), new object[] { returnMusic, returnAddItem });


            }


            Invoke(new SetCallback(() =>
            {
                btnExportar.Enabled = true;
                MessageBox.Show("Exportação concluída");
            }));
        }
        #endregion

        #region Event Forms
        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (cbxPlaylist.SelectedItem == null || string.IsNullOrEmpty(txtNewPlaylist.Text))
                return;

            var playlist = cbxPlaylist.SelectedItem as SimplePlaylist;
            btnExportar.Enabled = false;
            Task.Run(() => RunCreateList(playlist.Id, txtNewPlaylist.Text)).Wait();
        }

        private void cbxPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNewPlaylist.Text = (cbxPlaylist.SelectedItem as SimplePlaylist).ToString();
        }

        #endregion

    }
}
