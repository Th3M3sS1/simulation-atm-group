
public class Customer
{
    public double arrivalTime;
    public double serviceTime;

    public Customer(double arrive, double service)
    {
        arrivalTime = arrive;
        serviceTime = service;
    }

    public string getInfo()
    {
        return "Arrival Time : " + arrivalTime.ToString() + " Service Time : " + serviceTime.ToString(); 
    }
}
