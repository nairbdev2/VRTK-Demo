using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResize_Script : MonoBehaviour
{

	public GameObject avatarSimulated;
    public GameObject avatarVR;
    public GameObject cielingObject;
	public float shrinkAvatarSize;
    public float normalYHeight;
    public float growAvatarSize;
    

    public void shrinkPlayer()
	{
        if (cielingObject.activeInHierarchy == false)
        {
            cielingObject.SetActive(true);
        }

        avatarVR.transform.position = new Vector3(transform.position.x, transform.position.y - shrinkAvatarSize, transform.position.z);
        //avatarVR.transform.localScale = new Vector3(shrinkAvatarSize, shrinkAvatarSize, shrinkAvatarSize);
        avatarSimulated.transform.position = new Vector3(transform.position.x, transform.position.y - shrinkAvatarSize, transform.position.z);
        //avatarSimulated.transform.localScale = new Vector3(shrinkAvatarSize, shrinkAvatarSize, shrinkAvatarSize);
    }

    public void normalSizePlayer()
    {
        if (cielingObject.activeInHierarchy == false)
        {
            cielingObject.SetActive(true);
        }

        avatarVR.transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        //avatarVR.transform.localScale = new Vector3(1f,1f,1f);
        avatarSimulated.transform.position = new Vector3(transform.position.x, normalYHeight, transform.position.z);
        //avatarSimulated.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void enlargePlayer()
    {

        if (cielingObject.activeInHierarchy == true)
        {
            cielingObject.SetActive(false);
        }

        avatarVR.transform.position = new Vector3(transform.position.x, growAvatarSize, transform.position.z);
        //avatarVR.transform.localScale = new Vector3(growAvatarSize, growAvatarSize, growAvatarSize);
        avatarSimulated.transform.position = new Vector3(transform.position.x, growAvatarSize, transform.position.z);
        //avatarSimulated.transform.localScale = new Vector3(growAvatarSize, growAvatarSize, growAvatarSize);
    }

    protected Vector3? initialLocalPosition;
    protected Quaternion initialLocalRotation;
    protected Vector3 initialLocalScale;

    //Reset the Properties
    public virtual void ResetProperties()
    {
        if (avatarVR == null || initialLocalPosition == null || avatarSimulated == null)
        {
            return;
        }
        avatarVR.transform.localPosition = (Vector3)initialLocalPosition;
        avatarVR.transform.localRotation = initialLocalRotation;
        //avatarVR.transform.localScale = initialLocalScale;
        avatarSimulated.transform.localPosition = (Vector3)initialLocalPosition;
        avatarSimulated.transform.localRotation = initialLocalRotation;
      //avatarSimulated.transform.localScale = initialLocalScale;
    }

    protected virtual void Awake()
    {
        CacheSourceTransformData();
    }

    protected virtual void CacheSourceTransformData()
    {
        if (avatarVR == null || avatarSimulated == null)
        {
            initialLocalPosition = null;
            return;
        }

        if (avatarVR.activeInHierarchy == true)
        {
            initialLocalPosition = avatarVR.transform.localPosition;
            initialLocalRotation = avatarVR.transform.localRotation;
            initialLocalScale = avatarVR.transform.localScale;
        }

        else if (avatarSimulated.activeInHierarchy == true)
        {
            initialLocalPosition = avatarSimulated.transform.localPosition;
            initialLocalRotation = avatarSimulated.transform.localRotation;
            initialLocalScale = avatarSimulated.transform.localScale;
        }
        
    }

    protected virtual void OnAfterSourceChange()
    {
        CacheSourceTransformData();
    }

    // Update is called once per frame
    void Update()
    {




    }
}
