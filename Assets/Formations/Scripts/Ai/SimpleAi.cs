using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]
public class SimpleAi : MonoBehaviour , IPositonable
{  
    NavMeshAgent _agent;
    Animator _anim;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }
    
    
    void Update()
    {
        ChekAnim(_agent.velocity.magnitude);
        
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetPositon(Vector3 val)
    {
        _agent.SetDestination(val);

    }

    public void ChekAnim(float val)
    {
        if(val <= 0.4f)
        {
            _anim.SetBool("idle" ,true);
            _anim.SetBool("walk" , false);
            var vec3 = new Vector3(transform.parent.position.x , transform.position.y , transform.parent.position.z);;
            transform.LookAt(vec3);
        }
        else
        {
            _anim.SetBool("walk",true);
            _anim.SetBool("idle" ,false);

        }
    }   
}
