using Model;
using Newtonsoft.Json;
using Stack_Overflow_Tag_List_API.Interfaces;
using System.Net;
using System;
using System.Text;
using System.IO.Compression;

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
        private int tagNumber = 1;   
        private List<Tag> tags;
        private readonly IDatabaseTagService _dataTagService;

        public TagService(IDatabaseTagService databaseTagService)
        {
            tags = new List<Tag>();
            _dataTagService = databaseTagService;
        }

        public async Task<bool> GetTags()
        {
            int count = -1;
            for (int i = 1; tags.Count() < 1000; i++)
            {
                count = tags.Count();
                await GetTagsFromPage(i);
            }
            return true;//_dataTagService.AddTags(tags);
        }

        private async Task GetTagsFromPage(int page)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                   
                    var response = await httpClient.GetStreamAsync($"https://api.stackexchange.com/2.3/tags?page={page}&pagesize=100&order=desc&sort=popular&site=stackoverflow&filter=!21k7qaosV)V8y5XQ1QjJd");
                    GZipStream gs = new GZipStream(response, CompressionMode.Decompress);
                    var ms = new MemoryStream();
                    gs.CopyTo(ms);
                    var jsonContent = Encoding.UTF8.GetString(ms.ToArray());
                    var items = JsonConvert.DeserializeObject<ItemResponse>(jsonContent);

                    foreach (var item in items.Items)
                    {
                        
                        _dataTagService.AddTag(new Tag { Name = item.Name, Count = item.Count });
                        Console.WriteLine($"Tag Added: Tag number: {tagNumber}, Name: {item.Name}, Count: {item.Count}");
                        tagNumber++;
                        tags.Add(new Tag { Name = item.Name, Count = item.Count });
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
