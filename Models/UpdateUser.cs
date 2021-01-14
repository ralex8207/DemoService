using System.Text.Json.Serialization;
using DemoService.Entities;

namespace DemoService.Models {
    public class UpdateUser : Users {

        [JsonIgnore]
        public override int Id { get; set; }
    }
}