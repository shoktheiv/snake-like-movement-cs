using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed = 7; // Speed of the snake
    private Rigidbody2D rb;

    [SerializeField] private Transform player;

    // Tail
    [SerializeField] private List<GameObject> bodyParts = new List<GameObject>();  // Length of the tail. This is not used to make the tail move! Its used to instantiate the tail
    private List<GameObject> snakeBody = new List<GameObject>(); // The actual body where the tail moves. Make sure to add the head of the snake to this list for this to work

    [SerializeField] private float distanceBetween = 0.2f;

    float countUp = 0;

    void Start()
    {
        // Adding the head to the body of the snake for the tail to follow
        snakeBody.Add(gameObject);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Look();
    }

    void Look(){
        // This can be changed
        Vector2 dif = (Vector2)(player.position - transform.position);
        float angle = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    void FixedUpdate()
    {
        // Don't touch this
        if(bodyParts.Count > 0) CreateBodyPart(); // Creating the tail
        
        Move();
    }

    void Move(){
        
        // This can be changed
        Vector2 dir = (Vector2)(player.position - transform.position).normalized;
        rb.velocity = dir * speed * Time.deltaTime;

        // This is to update the tail to follow the head
        if(snakeBody.Count > 1){
            for(int i = 1; i < snakeBody.Count; i++){
                MarkerManager markM = snakeBody[i - 1].GetComponent<MarkerManager>();
                print(markM);
                snakeBody[i].transform.position = markM.markerList[0].position;
                snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                markM.markerList.RemoveAt(0);
            }
        }
    }


    // Creating the Tail, each tail object needs to have a MarkerManager component
    void CreateBodyPart(){
        MarkerManager markM = snakeBody[snakeBody.Count - 1].GetComponent<MarkerManager>();

        if (countUp == 0) markM.ClearMarkerList();

        countUp += Time.deltaTime;

        if (countUp >= distanceBetween){
            GameObject temp = Instantiate(bodyParts[0], markM.markerList[0].position, markM.markerList[0].rotation);
            temp.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

            snakeBody.Add(temp);
            bodyParts.RemoveAt(0);
            temp.GetComponent<MarkerManager>().ClearMarkerList();
            countUp = 0;
        }
    }
}
