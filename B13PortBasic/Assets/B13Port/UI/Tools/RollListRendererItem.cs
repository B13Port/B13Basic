using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollListRendererItem : MonoBehaviour
{

    protected object mData;
    public void SetData(object data)
    {
        if (data == null)
        {
            return;
        }

        mData = data;
        OnRenderer();
    }
    public T GetData<T>()
    {
        return (T)mData;
    }
    public virtual void OnRenderer()
    {

    }
}
