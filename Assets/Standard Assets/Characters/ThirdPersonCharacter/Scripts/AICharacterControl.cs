using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        private Transform target;
        public Transform target1;                                    // target to aim for
        public Transform target2;
        public bool reached = false;
        public float countdown = .1f;
                
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
            SetTarget(target1);
            agent.SetDestination(target.position);
        }


        private void Update()
        {
           
            if (target != null)
            {
                

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    character.Move(agent.desiredVelocity, false, false);

                }
                else
                {
                    character.Move(Vector3.zero, false, false);
                    reached = true;
                }
                if(reached)
                {
                    countdown -= Time.deltaTime;
                    if(countdown < 0)
                    {
                        
                        reached = false;
                        countdown = .1f;
                        if (target.Equals(target1))
                        {
                            SetTarget(target2);
                            agent.SetDestination(target.position);
                        }
                        else
                        {
                            SetTarget(target1);
                            agent.SetDestination(target.position);
                        }
                    }
                }
            }
                
            
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public Transform GetTarget()
        {
            return target;
        }
    }
}
