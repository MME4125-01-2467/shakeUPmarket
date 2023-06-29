//https://codingmania.tistory.com/173

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectDrag : MonoBehaviour
{

    Material outline;

    Renderer renderers;
    List<Material> materialList = new List<Material>();
    
    float distance = 1;

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,  Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        this.transform.position = objPosition;
        //print("Drag!!"+objPosition);

    }
    private void OnMouseEnter()
    {
        renderers = this.GetComponent<Renderer>();
        materialList.Clear();
        materialList.AddRange(renderers.sharedMaterials);
        materialList.Add(outline);

        renderers.materials = materialList.ToArray();
    }
        private void OnMouseExit()
    {
        Renderer renderer = this.GetComponent<Renderer>();

        materialList.Clear();
        materialList.AddRange(renderer.sharedMaterials);       
        materialList.Remove(outline);

        renderer.materials = materialList.ToArray();  
    }
    
        void Start()
    {
        outline = new Material(Shader.Find("Unlit/OutLine"));
    }

}
