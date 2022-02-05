using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Numerics.Statistics.Distributions;

public class GenerateCustomer : MonoBehaviour
{
    public int customerLength = 500;
    public List<Customer> customers = new List<Customer>();

    //public GameObject arriveVisual;
    //public GameObject serviceVisual;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < customerLength; i++)
        {
            NormalDistribution n = new NormalDistribution(4, 1);
            double arrive = System.Math.Round(n.InverseLeftProbability(Random.Range(0.0f, 0.99f)), 2);

            NormalDistribution m = new NormalDistribution(3, 1);
            double service = System.Math.Round(m.InverseLeftProbability(Random.Range(0.0f, 0.99f)), 2);

            customers.Add(new Customer(arrive, service));

            Debug.Log("Customer " + i.ToString() + " - " + customers[i].getInfo());

            //Instantiate(arriveVisual, new Vector3(Random.Range(-25.0f, 25.0f), (float)arrive * 10.0f, 0.0f * 10.0f), Quaternion.identity);
            //Instantiate(serviceVisual, new Vector3(Random.Range(-25.0f, 25.0f), -(float)service * 10.0f, 0.0f), Quaternion.identity);
        }
    }
}
