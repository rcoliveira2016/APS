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
        delegate void SetStringBoolCallback(string text, bool isTrue=true);
        delegate void SetStringCallback(string text);
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

        private async void RunCreateList(string idPlaylist, string name, string idUser)
        {
            var listNameMusic = await spotifyApi.GetFullNamePlaylistFullTracksAll(idPlaylist, idUser);


            if (!listNameMusic.Any())
                return;           


            var youtube = new YoutubeApi();

            var listCount = 0;

            Invoke(new SetStringCallback((string text) =>
            {
                lblTotal.Text = text;
                pBarPlaylist.Maximum = Convert.ToInt32(text);
            }), new object[] { listNameMusic.Count().ToString() });

            youtube.Login();

            var idNewPlaylist = await youtube.CreatePlaylist(name);

            foreach (var music in listNameMusic)
            {

                listCount++;

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

                Invoke(new SetStringCallback((string text) =>
                {
                    pBarPlaylist.PerformStep();
                    lblAtual.Text = text;
                }), new object[] { listCount.ToString() });

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
            Task.Run(() => RunCreateList(playlist.Id, txtNewPlaylist.Text, playlist.Owner.Id)).Wait();
        }

        private void cbxPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNewPlaylist.Text = (cbxPlaylist.SelectedItem as SimplePlaylist).ToString();
        }

        private void ExportPlaylist_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !btnExportar.Enabled;
        }


        #endregion
    }
}
