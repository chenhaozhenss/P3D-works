using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public GameObject baocai;
    internal GameObject pick;

    public void Open()
    {
        transform.DORotate(new Vector3 (0, -90, 0), 1).OnComplete(delegate
        {
            pick = Instantiate(baocai);
        });
    }

    public void Close ()
    {
        transform.DORotate(Vector3.zero, 1);
    }
}
