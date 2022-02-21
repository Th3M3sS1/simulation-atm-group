using UnityEngine;
using Meta.Numerics.Statistics.Distributions;
using System.Collections;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    public double arrivalTime;
    public double serviceTime;

    private float arrivalSpeed;
    private float servicSpeed;

    private int customerIndex;

    //public Material currentMat;

    public List<Transform> waypoints = new List<Transform>();
    private int _currentWaypointIndex = 0;

    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;

    public bool canMove = false;

    public GenerateCustomer generator;
    public CustomerQueue CustomerQueue;

    [SerializeField]
    private Transform atmAccessPoint;

    public Material defaultMat;
    public Material arrivalMat;
    public Material serviceMat;

    private void Start()
    {
        NormalDistribution n = new NormalDistribution(4, 1);
        double arrive = System.Math.Round(n.InverseLeftProbability(Random.Range(0.0f, 0.99f)), 2);

        NormalDistribution m = new NormalDistribution(3, 1);
        double service = System.Math.Round(m.InverseLeftProbability(Random.Range(0.0f, 0.99f)), 2);

        arrivalTime = arrive;
        serviceTime = service;

        arrivalSpeed = (float)arrive;
        servicSpeed = (float)service;

        transform.position = new Vector3(Random.Range(-50.0f, 50.0f), 0.0f, Random.Range(-50.0f, 50.0f));

        //getInfo();
        //SetWayPointArray();

        CustomerQueue = generator.gameObject.GetComponent<CustomerQueue>();
    }

    private void Update()
    {
        if (!canMove)
            return;

        if (_waiting && _currentWaypointIndex == 1)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime)
                return;
            _waiting = false;

            int random = Random.Range(0, 100);

            if (random < 25)
            {
                CustomerQueue.AddCustomerToQueue(this);
                canMove = false;
            }
            else
                return;

        }

        Transform wp = waypoints[_currentWaypointIndex];
        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            transform.position = wp.position;
            _waitCounter = 0f;
            _waiting = true;

            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Count;
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                wp.position,
                arrivalSpeed * 2.5f * Time.deltaTime);
        }
    }

    public void SetIndex(int i)
    {
        customerIndex = i;
    }

    public void getInfo()
    {
        Debug.Log(customerIndex.ToString() + " : Arrival Time : " + arrivalTime.ToString() + " Service Time : " + serviceTime.ToString()); 
    }

    public void SetWayPointArray()
    {
        int temp = Random.Range(1, 10);
        Transform[] newWayPoints; 

        if (temp <= 5)
        {
            newWayPoints = generator.GetWayPointArray1();

            for (int i = 0; i < newWayPoints.Length; i++)
                waypoints.Add(newWayPoints[i]);
        }
        else
        {
            newWayPoints = generator.GetWayPointArray2();
            for (int i = 0; i < newWayPoints.Length; i++)
                waypoints.Add(newWayPoints[i]);
        }

        canMove = true;

        _currentWaypointIndex = Random.Range(0, waypoints.Count);
    }

    public void SetATMpoint(Transform newPos)
    {
        atmAccessPoint = newPos;
    }

    public IEnumerator MoveTowardsATM()
    {
        gameObject.GetComponent<MeshRenderer>().material = arrivalMat;

        float elapsedTime = 0.0f;
        Vector3 startingPos = transform.position;
        while (elapsedTime < arrivalSpeed)
        {
            transform.position = Vector3.Lerp(startingPos, atmAccessPoint.position, (elapsedTime / arrivalSpeed));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        transform.position = atmAccessPoint.position;

        StartCoroutine(UseATMService());
    }

    public IEnumerator UseATMService()
    {
        gameObject.GetComponent<MeshRenderer>().material = serviceMat;

        float elapsedTime = 0.0f;

        while (elapsedTime < servicSpeed)
        {
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        CustomerQueue.RemoveCustomerFromQueue();
        canMove = true;
        gameObject.GetComponent<MeshRenderer>().material = defaultMat;
    }
}
