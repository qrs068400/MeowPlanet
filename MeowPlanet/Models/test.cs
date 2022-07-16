namespace MeowPlanet.Models
{
    public class test : IMemberRepostry
    {
        public Member selectMember(int id)
        {
            
            Console.WriteLine(id);


            return new Member();
        }
    }
}
