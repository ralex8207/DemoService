namespace DemoService.Ef {

    public interface IEntity<TKey> where TKey : struct {

        TKey Id { get; set; }
    }

}