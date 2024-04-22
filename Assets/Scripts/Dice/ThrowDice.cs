using System.Collections;
using System.Data;
using UnityEngine;

public class ThrowDice : MonoBehaviour
{
    new Rigidbody rigidbody;
    bool canRoll = true;
    bool hasRolled = false;
    DiceRaycast[] diceFaces;

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
        if (rigidbody.velocity.magnitude != 0) {
            hasRolled = true;
        } else if (hasRolled && rigidbody.velocity.magnitude <0.5)
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
            int resultRandomForce = RandomForce();
            int resultRandomNumberofAxis = RandomNumberOfAxis();
            int x, y,z;
            x = RandomVector();
            y = RandomVector();
            z = RandomVector();

            rigidbody.AddForce(Vector3.up * resultRandomForce, ForceMode.Impulse);

            //Debug.Log("Numero random de chances: "+resultRandomNumberofAxis);
            if (resultRandomNumberofAxis >= 1 && resultRandomNumberofAxis <= 5) //0.5% chances
            {
                int resultValue = Random1Axis();
                if (resultValue == 1)
                {
                    vectorTorque = new Vector3(x, 0, 0);
                }
                else if (resultValue == 2)
                {
                    vectorTorque = new Vector3(0, y, 0);
                }
                else if (resultValue == 3)
                {
                    vectorTorque = new Vector3(0, 0, z);
                }
            }
            else if (resultRandomNumberofAxis >= 6 && resultRandomNumberofAxis <= 100) //10% chances 
            {
                int resultValue = Random1Axis();
                if (resultValue == 1)
                {
                    vectorTorque = new Vector3(x, 0, z);
                }
                else if (resultValue == 2)
                {
                    vectorTorque = new Vector3(x, y, 0);
                }
                else if (resultValue == 3)
                {
                    vectorTorque = new Vector3(0, y, z);
                }
            }
            else if (resultRandomNumberofAxis >= 100 && resultRandomNumberofAxis <= 1000) //90% chances 
            {
                vectorTorque = new Vector3(x, y, z);
            }
   
            rigidbody.AddTorque(vectorTorque * resultRandomForce, ForceMode.Impulse);
            //Debug.Log(vectorTorque);
        }
    }

    #region Rabndom Methods
    int Random1Axis()
    {
        int randomnumber;
        randomnumber = Random.Range(1, 4);
        return randomnumber;
    }
    int RandomNumberOfAxis()
    {
        int randomnumber;
        randomnumber = Random.Range(1, 1001);
        return randomnumber;
    }
    int RandomForce()
    {
        int randomnumber;
        randomnumber = Random.Range(150, 200);
        return randomnumber;
    }
    int RandomVector()
    {
        int randomnumber;
        randomnumber = Random.Range(-1, 2);
        return randomnumber;
    }
    #endregion
}