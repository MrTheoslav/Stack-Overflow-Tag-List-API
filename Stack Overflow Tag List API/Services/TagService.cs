using Model;
using Newtonsoft.Json;
using Stack_Overflow_Tag_List_API.Interfaces;
using System.Net;
using System;

namespace Stack_Overflow_Tag_List_API.Services
{
    public class Item
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Count")]
        public int Count { get; set; }
    }

    public class ItemResponse
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public class TagService : ITagService
    {
        private List<Tag> tags;
        private readonly IDatabaseTagService _dataTagService;

        public TagService(IDatabaseTagService databaseTagService)
        {
            tags = new List<Tag>();
            _dataTagService = databaseTagService;
        }

        public bool GetTags()
        {
            int count = -1;
            for (int i = 1; count == tags.Count(); i++)
            {
                count = tags.Count();
                GetTagsFromPage(i);
            }
            return _dataTagService.AddTags(tags);
        }

        public bool IsSiteOnline()
        {
            string url = "https://api.stackexchange.com/docs/tags#pagesize=1&order=desc&sort=popular&filter=!bMsg5CXICdlFSp&site=stackoverflow";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("The connection to the website is active.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to connect to the website. Response code: {response.StatusCode}");
                        return false;
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"An error has occurred while trying to connect to the website: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return false;
            }
        }

        private async Task GetTagsFromPage(int page)
        {

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://api.stackexchange.com/docs/tags#page={page}&order=desc&sort=popular&filter=!bMsg5CXICdlFSp&site=stackoverflow");
                var jsonContent = await response.Content.ReadAsStringAsync();

                var items = JsonConvert.DeserializeObject<ItemResponse>(jsonContent);

                foreach (var item in items.Items)
                {
                    tags.Add(new Tag { Name = item.Name, Count = item.Count });
                }
            }
        }
    }
}
