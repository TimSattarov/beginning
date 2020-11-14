using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService.Models
{
    public class PoligonModel
    {
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string Resource_id { get; set; }
        public string Public_url { get; set; }
        public string Name { get; set; }
    }
}
