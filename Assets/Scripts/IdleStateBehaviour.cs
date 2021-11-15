using UnityEngine;

public class IdleStateBehaviour : StateMachineBehaviour
{
    [Range(2.0f, 10.0f)]
    public float waitTime = 5f;
    public GameObject marker;

    private float timeTillMove = 5f;
    private Vector3 stopPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("walk", false);
        stopPosition = new Vector3(animator.transform.position.x, 0.01f, animator.transform.position.z);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeTillMove -= Time.deltaTime;
        if (timeTillMove <= 0)
        {
            timeTillMove = waitTime;
            animator.SetBool("walk", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Instantiate(marker, stopPosition, Quaternion.identity);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
          // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
