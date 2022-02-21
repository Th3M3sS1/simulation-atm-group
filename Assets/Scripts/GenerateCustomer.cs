using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCustomer : MonoBehaviour
{
    public int customerLength = 500;
    //public List<GameObject> customers = new List<GameObject>();

    //public GameObject arriveVisual;
    //public GameObject serviceVisual;

    public GameObject customerPrefab;

    public Transform[] wayPointsArray1;
    public Transform[] wayPointsArray2;

    [SerializeField]
    private Transform atmTransportPoint;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < customerLength; i++)
        {
            GameObject newCustomer = Instantiate(customerPrefab) as GameObject;
            //customers.Add(newCustomer);
            //customers.Add(new Customer(arrive, service));
            newCustomer.GetComponent<Customer>().SetIndex(i);
            newCustomer.GetComponent<Customer>().generator = this;
            newCustomer.GetComponent<Customer>().SetWayPointArray();
            newCustomer.GetComponent<Customer>().SetATMpoint(atmTransportPoint);
            //Instantiate(arriveVisual, new Vector3(Random.Range(-25.0f, 25.0f), (float)arrive * 10.0f, 0.0f * 10.0f), Quaternion.identity);
            //Instantiate(serviceVisual, new Vector3(Random.Range(-25.0f, 25.0f), -(float)service * 10.0f, 0.0f), Quaternion.identity);
        }
    }

    public Transform[] GetWayPointArray1()
    {
        return wayPointsArray1;
    }

    public Transform[] GetWayPointArray2()
    {
        return wayPointsArray2;
    }
}
