using System;
using System.Collections.Generic;
using UnityEngine;


public class PopUpUIManager : MonoBehaviour
{

    /// <summary>
    /// Stack구조로 현재 띄어진 UI 받기위함
    /// </summary>
    public Stack<PopUpBaseUI> uis = new Stack<PopUpBaseUI>();
    [SerializeField]
    public List<PopUpBaseUI> uiList =new List<PopUpBaseUI> ();//인스펙터에서 보기 위함


    public void Awake()
    {
        ResetPopUpUIList();
    }
    public void ResetPopUpUIList()
    {
        uiList.Clear();

        for (int i = 0; i < uis.Count; i++)
        {
            //배열 -> 대입
            uiList.Add(uis.ToArray()[i]);
            print(uiList[i].gameObject);
        }
    }
    public void RemoveList() 
    {
            
    }

    public void ReSortingOrder(Canvas canvas)
    {
        int sortingNum = -1;
        if (uis.Count <= 0)
        {
            sortingNum = 0;

        }
        else
        { 
            sortingNum = uis.Count;
            print(uis.Peek().gameObject.name + $" 내가 선택한 : {sortingNum}");
        }
        canvas.sortingOrder = sortingNum;
    }

}

