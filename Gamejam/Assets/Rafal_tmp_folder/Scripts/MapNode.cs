using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public List<MapNode> childNodes = new List<MapNode>();
    public int spawnPointId;//0 - left, 1 - middle, 2 - right
    public int depth = 0;//basicly how far from start this tile is
    public bool canBeSelected = false;//if can be selected

    LineRenderer _lineRend;
    List<Transform> _points;
    bool _enableDrawingLines = false;
    
    // Start is called before the first frame update
    public void Initialize(List<MapNode> allNodes)
    {
        FindChildNodes(allNodes);
        MapController.Instance.OnIntersectionsRemoved += SetLineRendererPoints;
        MapController.Instance.OnIntersectionsRemoved += ()=>_enableDrawingLines=true;
    }

    private void SetLineRendererPoints()
    {
        _lineRend = GetComponent<LineRenderer>();
        _lineRend.positionCount = childNodes.Count * 2;
        _points = new List<Transform>();
        _points.Add(transform);
        foreach (MapNode node in childNodes)
        {
            _points.Add(node.transform);
        }
    }

    private void Update()
    {
        if (_enableDrawingLines)
        {
            int posID = 0;
            for (int i = 0; i < childNodes.Count * 2; i++)
            {
                if (i % 2 == 0)
                {
                    _lineRend.SetPosition(i, _points[0].position);
                    posID++;
                }
                else
                {
                    _lineRend.SetPosition(i, _points[posID].position);
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

    public void LockRoom()//called when room was not selected
    {
        canBeSelected = false;
    }

    private void OnMouseDown()
    {
        if(canBeSelected)MapController.Instance.SelectRoom(this);
    }
}
