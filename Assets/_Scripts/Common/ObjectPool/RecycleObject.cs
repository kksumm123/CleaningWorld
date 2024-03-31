using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    protected Action<RecycleObject> restore;

    public void InitializedByObjectPoolSystem(Action<RecycleObject> restore)
    {
        this.restore = restore;
    }

    protected void Restore()
    {
        restore(this);
    }
}
