using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using SpotifyAPI.Example.Interface.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConvertPlaylist.App.ControlApi
{
    public class YoutubeApi : IApi
    {

        private YouTubeService youtubeService;

        public async void Login()
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string camilhoJson = Path.Combine(executableLocation, "./Content/client_secret.json");

            UserCredential credential;
            using (var stream = new FileStream(camilhoJson, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeUpload },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(this.GetType().ToString())
                );
            }

            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Convert Playlist"
            });

            
        }

        private async Task<string> RunSearch(string query)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query; // Replace with your search term.
            searchListRequest.MaxResults = 1;
            searchListRequest.Type = "video";

            var searchListResponse = await searchListRequest.ExecuteAsync();

            return searchListResponse.Items.FirstOrDefault()?.Id?.VideoId;

        }

        private async Task<ICollection<string>> RunCreatePlaylist(ICollection<string> listNameMusic, string name) {

            var listNotFind = new List<string>();

            var newPlaylist = new Playlist();
            newPlaylist.Snippet = new PlaylistSnippet();
            newPlaylist.Snippet.Title = name;
            newPlaylist.Status = new PlaylistStatus();
            newPlaylist.Status.PrivacyStatus = "public";
            newPlaylist = await youtubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();



            foreach (var music in listNameMusic)
            {

                string idMusic = await RunSearch(music);
                if (!string.IsNullOrEmpty(idMusic))
                {

                    var newPlaylistItem = new PlaylistItem();
                    newPlaylistItem.Snippet = new PlaylistItemSnippet
                    {
                        PlaylistId = newPlaylist.Id,
                        ResourceId = new ResourceId()
                    };
                    newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
                    newPlaylistItem.Snippet.ResourceId.VideoId = idMusic;
                    newPlaylistItem = await youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();
                }
                else {
                    listNotFind.Add(music);
                }
                
                
            }

            return listNameMusic;
        }

        public async Task<string> CreatePlaylist(string name) {            

            var newPlaylist = new Playlist();
            newPlaylist.Snippet = new PlaylistSnippet();
            newPlaylist.Snippet.Title = name;
            newPlaylist.Status = new PlaylistStatus();
            newPlaylist.Status.PrivacyStatus = "public";
            newPlaylist = await youtubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();

            return newPlaylist.Id;
        }

        public async Task<bool> AddItemPlaylist(string idPlaylist, string music)
        {

            string idMusic = await RunSearch(music);
            if (string.IsNullOrEmpty(idMusic))
                return false;
            
            var newPlaylistItem = new PlaylistItem();
            newPlaylistItem.Snippet = new PlaylistItemSnippet
            {
                PlaylistId = idPlaylist,
                ResourceId = new ResourceId()
            };
            newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
            newPlaylistItem.Snippet.ResourceId.VideoId = await RunSearch(music);
            newPlaylistItem = await youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();

            return true;
        }

        public ICollection<string> CreatePlaylistByListMusic(ICollection<string> listNameMusic, string name) {

            return Task.Run(() => RunCreatePlaylist(listNameMusic, name)).Result;

        }

        public void Logout()
        {
            
        }
    }
}

