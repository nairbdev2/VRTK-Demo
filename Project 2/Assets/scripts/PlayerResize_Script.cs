﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResize_Script : MonoBehaviour
{

	public GameObject avatar;
	public float shrinkAvatarSize;

    public void shrinkPlayer()
	{
		avatar.transform.position = new Vector3(transform.position.x, shrinkAvatarSize, transform.position.z);
        avatar.transform.localScale += new Vector3(shrinkAvatarSize, shrinkAvatarSize, shrinkAvatarSize);
    }

    protected Vector3? initialLocalPosition;
    protected Quaternion initialLocalRotation;
    protected Vector3 initialLocalScale;

    //Reset the Properties
    public virtual void ResetProperties()
    {
        if (avatar == null || initialLocalPosition == null)
        {
            return;
        }
        avatar.transform.localPosition = (Vector3)initialLocalPosition;
        avatar.transform.localRotation = initialLocalRotation;
        avatar.transform.localScale = initialLocalScale;
    }

    protected virtual void Awake()
    {
        CacheSourceTransformData();
    }

    protected virtual void CacheSourceTransformData()
    {
        if (avatar == null)
        {
            initialLocalPosition = null;
            return;
        }

        initialLocalPosition = avatar.transform.localPosition;
        initialLocalRotation = avatar.transform.localRotation;
        initialLocalScale = avatar.transform.localScale;
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
