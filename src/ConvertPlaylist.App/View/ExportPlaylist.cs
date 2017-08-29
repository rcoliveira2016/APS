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
                pBarPlaylist.Value = 0;
                pBarPlaylist.Maximum = Convert.ToInt32(text);
            }), new object[] { listNameMusic.Count().ToString() });

            var logged = await youtube.Login();

            if (!logged)
                return;

            var idNewPlaylist = await youtube.CreatePlaylist(name);

            var url = $"https://www.youtube.com/playlist?list={idNewPlaylist}";

            foreach (var music in listNameMusic)
            {

                listCount++;

                var returnAddItem = await youtube.AddItemPlaylist(idNewPlaylist, music);
                Invoke(new SetStringBoolCallback((string text, bool found) =>
                {                      

                    ltvAddItens.Items.Add(new ListViewItem()
                    {
                        Text = text,
                        SubItems = { (found ? "Sim" : "Não") }
                    });

                }), new object[] { music, returnAddItem });

                Invoke(new SetStringCallback((string text) =>
                {                    
                    pBarPlaylist.PerformStep();
                    lblAtual.Text = text;
                }), new object[] { listCount.ToString() });

            }

         
            Invoke(new SetStringCallback((string text) =>
            {
                System.Diagnostics.Process.Start(text);
                btnExportar.Enabled = true;
                MessageBox.Show("Exportação concluída ");
            }), new object[] { url });
        }
        #endregion

        #region Event Forms
        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (cbxPlaylist.SelectedItem == null || string.IsNullOrEmpty(txtNewPlaylist.Text))
                return;

            var playlist = cbxPlaylist.SelectedItem as SimplePlaylist;
            btnExportar.Enabled = false;
            try
            {
                Task.Run(() => RunCreateList(playlist.Id, txtNewPlaylist.Text, playlist.Owner.Id)).Wait();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro inesperado");
            }
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
