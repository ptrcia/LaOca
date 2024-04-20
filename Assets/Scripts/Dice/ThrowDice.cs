using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ThrowDice : MonoBehaviour
{
    new Rigidbody rigidbody;
    bool canRoll = true;
    bool hasRolled = false;
    DiceRaycast[] diceFaces;

    [SerializeField]
    int force;
    [SerializeField]
    Vector3 vectorTorque;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 100f;
        rigidbody.angularDrag = 0.5f;

        diceFaces = GetComponentsInChildren<DiceRaycast>();
    }

    void FixedUpdate()
    {
        if (hasRolled && rigidbody.velocity.magnitude == 0)
        {
            foreach (DiceRaycast face in diceFaces)
            {
                face.CheckForColliders();
            }
            hasRolled = false;
            canRoll = true;
        }
    }

    private void OnMouseDown()
    {
        if (canRoll)
        {
            hasRolled = true;
            rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);

            //rigidbody.AddTorque(new Vector3(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1)) * force, ForceMode.Impulse);
            int randomnumber;
            randomnumber = Random.Range(1, 3);
            if (randomnumber == 1)
            {
                vectorTorque = new Vector3(1, 0, 0);
            }if (randomnumber == 2)
            {
                vectorTorque = new Vector3(0, 1, 0);
            }if(randomnumber == 3)
            {
                vectorTorque = new Vector3(0, 0, 1);
            }
            Debug.Log(randomnumber);

            rigidbody.AddTorque(vectorTorque * force, ForceMode.Impulse);
        }
    }
}