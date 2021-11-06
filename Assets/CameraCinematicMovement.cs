using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCinematicMovement : MonoBehaviour
{

    public Vector2 desiredPosition;
    public Vector2 currentPosition;
    public float moveSpeed;
    public bool positionReached;
    // Start is called before the first frame update
    void Start()
    {
        desiredPosition = Vector2.zero;
    }

    public void GoToPosition(Vector2 position, float delay)
    {
        StartCoroutine(SetDesiredPosition(position, delay));
    }

    IEnumerator SetDesiredPosition(Vector2 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        desiredPosition = position;
    }
    // Update is called once per frame
    void Update()
    {
        positionReached = Vector2.Distance(desiredPosition, currentPosition) < 0.1f ? true : false;
        currentPosition = Vector2.Lerp(currentPosition, desiredPosition, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(currentPosition.x, currentPosition.y, transform.position.z);
    }
}
