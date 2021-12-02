using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrSelect : MonoBehaviour
{
    public string instrname;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        instrname = this.gameObject.name;
        Debug.Log(instrname);
    }
}
