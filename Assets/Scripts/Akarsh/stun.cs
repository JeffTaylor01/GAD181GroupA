using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stun : MonoBehaviour
{
    public float stunTime;
    private bool stunned;

    void Update()
    {

        if (stunned)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; // stop movement
            stunTime -= Time.deltaTime;
            if (stunTime <= 0)
            {
                stunned = false;
                stunTime = 4f;
            }
        }



        if (stunned)
        {
            StartCoroutine(FreezeMovement());
        }


        IEnumerator FreezeMovement()
        {
            //Backup and clear velocities
            Vector3 linearBackup = GetComponent<Rigidbody>().velocity;// backup distance (linear) vector after colliding with other object 
            Vector3 angularBackup = GetComponent<Rigidbody>().angularVelocity; // backup distance (angular) vector after colliding with other object
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            // freeze the body in place so forces like gravity or movement won't affect it
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

          
            yield return new WaitForSeconds(2); // Freezes coroutine actions for 2 seconds and unfreezes before restoring movement
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //restoring the velocities
            GetComponent<Rigidbody>().velocity = linearBackup;
            GetComponent<Rigidbody>().angularVelocity = angularBackup;
        }
    }
}
