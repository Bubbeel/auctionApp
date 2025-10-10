namespace WebApp_dotNet.Core;

public class Bid
{
    public int  Id { get; set; }
    public string Username { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Amount {get; set;}

    public Bid(int id,  string username, int amount)
    {
        Id = id;
        Username = username;
        CreatedAt = DateTime.Now;
        Amount = amount;
    }
}