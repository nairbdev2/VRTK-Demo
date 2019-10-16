using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff_Script : MonoBehaviour
{
	public GameObject genericObject;

    public void genericSwitch()
	{
		if (genericObject.activeInHierarchy == false)
		{
			genericObject.SetActive(true);
		} else
		{
			genericObject.SetActive(false);
		}
	}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       

    }
}
