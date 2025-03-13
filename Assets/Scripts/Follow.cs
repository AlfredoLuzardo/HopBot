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
        if(distance >= 0)
        {
            // transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 5 * Time.deltaTime);
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            Debug.Log($"target pos: {Target.transform.position}");
            rb.MovePosition(transform.position + direction * 2 * Time.fixedDeltaTime);
        }
    }
}
