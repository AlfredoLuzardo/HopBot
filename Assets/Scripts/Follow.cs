using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Target;
    public float distance;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, Target.transform.position);
        if(distance >= 3)
        {
            // transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 5 * Time.deltaTime);
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * 5 * Time.fixedDeltaTime);
        }
    }
}
