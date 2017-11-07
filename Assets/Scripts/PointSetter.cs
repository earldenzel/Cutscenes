using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSetter : MonoBehaviour {

    private List<GameObject> targets;
    public GameObject target;
    public GameObject car;
    private bool hideVehicle;
    public AnimationCurve carMovement;
    public Text message;
    //public GameObject resetButton;
    //public GameObject startButton;

    public List<GameObject> Targets
    {
        get
        {
            return targets;
        }

        set
        {
            targets = value;
        }
    }

    public bool HideVehicle
    {
        get
        {
            return hideVehicle;
        }

        set
        {
            hideVehicle = value;
        }
    }

    // Use this for initialization
    void Start () {
        targets = new List<GameObject>();
        HideVehicle = true;
        message.text = "Use left mouse click to add points\nPress R to reset points";
        //startButton.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetMouseButtonDown(0))
        {
            Setup();
        }
        if (targets.Count > 2 && HideVehicle)
        {
            ReadyForDeployment();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPoints();
        }
	}

    public void ReadyForDeployment()
    {
        message.text = "Use left mouse click to add points\nPress R to reset points\nPress [Spacebar] to start sequence";
        //startButton.GetComponent<Renderer>().enabled = true;
        Instantiate(car, targets[0].transform.position, Quaternion.identity);
        HideVehicle = false;
    }

    public void ResetPoints()
    {
        message.text = "Use left mouse click to add points\nPress R to reset points";
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        foreach (GameObject gameObject in targets)
        {
            Destroy(gameObject);
        }
        targets.Clear();
        HideVehicle = true;
        //startButton.GetComponent<Renderer>().enabled = false;
    }

    public void Setup()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.Log(hit.transform.name);
        }
        else if (!Physics.Raycast(ray) && targets.Count < 10)
        {
            //if (ray.origin.x < 8.4)
            //{
                GameObject newTarget = Instantiate(target, ray.origin, Quaternion.identity);
                targets.Add(newTarget);
            //}
        }
    }
}
