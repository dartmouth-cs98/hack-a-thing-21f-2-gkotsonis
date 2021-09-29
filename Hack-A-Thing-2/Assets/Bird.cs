// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{   
    
    Vector3 _initialPosition;
    private bool _birdWasLaunched;
    private float _timeSittingAround;
    // allow launch power changes
    [SerializeField] private float _launchPower = 250;


    // record bird start position
    private void Awake()
    {
        _initialPosition = transform.position;
        GetComponent<LineRenderer>().enabled = false;
    }

    // reset bird if necessary
    private void Update()
    {   

        // get line/arrow renderer
        GetComponent<LineRenderer>().SetPosition(1,_initialPosition);
        GetComponent<LineRenderer>().SetPosition(0,transform.position);


        // reset bird when it exits the stage
        if (transform.position.y > 10 ||
            transform.position.y < -10 ||
            transform.position.x > 10 ||
            transform.position.x < -10 ||
            _timeSittingAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        // reset bird when it's laid on the ground unmoving for some time
        if (_birdWasLaunched && 
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        // check X position
        if (transform.position.y > 10)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

    }

    // runs every time I click on the bird
    private void OnMouseDown()
    {
        // change bird color
        GetComponent<SpriteRenderer>().color = Color.red;

        // render arrows to start position
        GetComponent<LineRenderer>().enabled = true;
    }

    // run when we release mouse on bird
    private void OnMouseUp()
    {   
        GetComponent<SpriteRenderer>().color = Color.white;

        // add force to bird upon release
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;

        _birdWasLaunched = true;

        // stop rendering arrows
        GetComponent<LineRenderer>().enabled = false;
    }

    // when dragging bird
    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}
