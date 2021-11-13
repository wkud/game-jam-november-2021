using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public List<MapNode> childNodes = new List<MapNode>();
    public int spawnPointId;//0 - left, 1 - middle, 2 - right
    public int depth = 0;
    public bool canBeSelected = false;
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Color lockedNodeColor;


    LineRenderer lineRend;
    List<Transform> points;
    
    // Start is called before the first frame update
    public void Initialize(List<MapNode> allNodes)
    {
        FindChildNodes(allNodes);
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = childNodes.Count*2;
        points = new List<Transform>();
        points.Add(transform);
        foreach (MapNode node in childNodes)
        {
            points.Add(node.transform);
        }
    }

    private void Update()
    {
        if (childNodes.Count > 0)
        {
            int posID = 0;
            for (int i = 0; i < childNodes.Count * 2; i++)
            {
                if (i % 2 == 0)
                {
                    lineRend.SetPosition(i, points[0].position);
                    posID++;
                }
                else
                {
                    lineRend.SetPosition(i, points[posID].position);
                }                                
            }
        }
    }

    void FindChildNodes(List<MapNode> allNodes)
    {
        foreach (MapNode node in allNodes)
        {
            if (node.depth == depth + 1 && Mathf.Abs(node.spawnPointId - spawnPointId) <= 1)
            {
                childNodes.Add(node);
            }
        }
    }

    public void LockRoom()
    {
        canBeSelected = false;
        //renderer.color = lockedNodeColor;
    }

    private void OnMouseDown()
    {
        if(canBeSelected)MapController.Instance.SelectRoom(this);
    }
}
