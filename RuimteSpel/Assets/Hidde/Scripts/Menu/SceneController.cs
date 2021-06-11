using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public UpgradeSystem us;
    public StationInventory si;

    public void CallStart()
    {
        us.Initiate();
        si.Initiate();
    }
}
