using Shiny;


namespace Sample
{
    public interface IMyCustomTestService
    {
        void Hello();
    }


    [ShinyService]
    public class MyCustomTestService : IMyCustomTestService
    {
        public void Hello()
        {
        }
    }
}
