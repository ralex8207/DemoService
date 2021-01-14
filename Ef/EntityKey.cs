using System.ComponentModel.DataAnnotations;

namespace DemoService.Ef {

    public class EntityKey<T> : IEntity<T> where T : struct {

        [Key]
        public virtual T Id { get; set; }
    }

}