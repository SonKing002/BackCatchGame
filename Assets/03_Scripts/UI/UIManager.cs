using System;
using System.Collections.Generic;
using UnityEngine;

namespace JH.UIManager
{
    public class UIManager : SingletonOfT<UIManager>
    {
        public override void Awake()
        { 
            base.Awake();

            DontDestroyOnLoad(this);
        }
        
        /// <summary>
        /// Stack구조로 현재 띄어진 UI 받기위함
        /// </summary>
        [SerializeField]        
        public Stack<PopUpBaseUI> uis = new Stack<PopUpBaseUI>();
    }
}
