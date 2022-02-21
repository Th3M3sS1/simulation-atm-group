using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomerQueue : MonoBehaviour
{
    private Queue<Customer> customers = new Queue<Customer>();

    [SerializeField]
    private Transform[] queuePositions;

    [SerializeField]
    private Customer firstCustomer;

    public void AddCustomerToQueue(Customer newCustomer)
    {
        customers.Enqueue(newCustomer);

        //Debug.Log("Customer added to the queue");

        if(customers.Count > 1)
            newCustomer.gameObject.transform.position = queuePositions[customers.Count - 1].position;

        if (customers.Count > 0 && firstCustomer == null)
        {
            //newCustomer.gameObject.transform.position = queuePositions[customers.Count].position;

            firstCustomer = customers.Peek();
            StartCoroutine(firstCustomer.MoveTowardsATM());

            //ResetPosition();
        }
    }

    public void RemoveCustomerFromQueue()
    {
        StopCoroutine(firstCustomer.MoveTowardsATM());
        StopCoroutine(firstCustomer.UseATMService());
        customers.Dequeue();

        if (customers.Count > 0)
        {
            firstCustomer = customers.Peek();
            StartCoroutine(firstCustomer.MoveTowardsATM());

            ResetPosition();
        }
    }

    void ResetPosition()
    {
        List<Customer> allCustomer = customers.ToList();
        for(int i = 1; i < allCustomer.Count; i++)
        {
            allCustomer[i].gameObject.transform.position = queuePositions[i - 1].position;
        }

        allCustomer.Clear();
    }
}
