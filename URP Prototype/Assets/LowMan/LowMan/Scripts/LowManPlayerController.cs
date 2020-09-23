using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
public class LowManPlayerController : MonoBehaviour {


    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public bool auto;
    public float area;

    private void Start() {
        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //agent = this.GetComponent<NavMeshAgent>();
        // character = this.GetComponent<ThirdPersonCharacter>();

        agent.updateRotation = false;
        Wonder();
    }

    void Update() {
        if (!auto) {

            // User Input
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) {
                    //move our agent
                    agent.SetDestination(hit.point);
                }

            }


            if (agent.remainingDistance > agent.stoppingDistance) {
                if (Input.GetButton("Jump"))
                {
                    Debug.Log("Trying to jump");

                    character.Move(new Vector3(0, 100, 0), false, true);

                }
                else
                {
                    //move until too close
                    //character.Move(agent.desiredVelocity, false, false);
                }
            } else {
                character.Move(Vector3.zero, false, false);
            }

        } else {

            // Auto Input
            if (agent.remainingDistance > agent.stoppingDistance) {
                //move until too close
                character.Move(agent.desiredVelocity, false, false);
            } else {
                Wonder();
            }
        }
        

    }

    public void Wonder() {
        Vector3 randomDirection = Random.insideUnitSphere * area;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, area, 1);
        Vector3 finalPosition = hit.position;
        Debug.Log("Final Pos = " + finalPosition.ToString());
        GetComponent<NavMeshAgent>().destination = finalPosition;
    }

}









