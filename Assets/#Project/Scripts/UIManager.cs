using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    internal CanvasRenderer canvasRenderer;
    internal TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        canvasRenderer = FindObjectOfType<CanvasRenderer>(includeInactive: true);
        tmp = canvasRenderer.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canvasRenderer.gameObject.activeInHierarchy){
            tmp.fontMaterial.SetFloat("_FaceDilate", Mathf.PingPong(Time.time / 3f, 0.3f));
            //tutoMaterial.SetFloat("_FaceDilate", Mathf.PingPong(Time.time * 2.35f, 0.3f));
        }
    }
}
