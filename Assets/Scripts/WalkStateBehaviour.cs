using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class WalkStateBehaviour : StateMachineBehaviour
{
    [Range(2.5f, 7f)]
    public float maximumWalkingDistance = 4f;

    private Text walkText;
    private NavMeshAgent agent;
    private int walks = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("idle", false);
        walkText = GameObject.Find("WalksText").GetComponent<Text>(); // have to set this on state enter since we can't use current scene objects to fill references in editor
        agent = animator.GetComponent<NavMeshAgent>(); // likewise for components.
        agent.destination = RandomPosition(animator.transform.position, maximumWalkingDistance, -1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= 0.01f)
            animator.SetBool("idle", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        walks++;
        walkText.text = "Walks Taken: " + walks.ToString();
    }

    Vector3 RandomPosition(Vector3 origin, float distance, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);
        return navHit.position;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
