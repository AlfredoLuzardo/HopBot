using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Target;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, Target.transform.position);
        if(distance >= 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 5 * Time.deltaTime);
        }
    }
}
