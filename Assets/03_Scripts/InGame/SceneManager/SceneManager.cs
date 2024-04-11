using System;
using System.Collections.Generic;
using UnityEngine;

namespace JH.InGameManager
{
    /// <summary>
    /// 모든 씬들 목록을 작성
    /// </summary>
    public enum SceneList
    { 
        Login,
        Lobby,
        Room,
        Game
    }

    /// <summary>
    /// 모든 씬에서 각각 실행할 목록들을 나누기 위한 관리자 클래스
    /// </summary>
    public  class SceneManager : SingletonOfT<SceneManager> 
    {
        /// <summary>
        /// 현재 씬
        /// </summary>
        [SerializeField]
        public SceneList currentlist = SceneList.Login;

        //public Action<BaseUI> register;
        public override void Awake()
        {
            base.Awake();

        }

        private void Update()
        {
            
        }

        /// <summary>
        /// 현재 씬에 대한 분기함수
        /// </summary>
        public void CurrentScene()
        {
            switch (currentlist)
            {
                case SceneList.Login:
                    break;
                case SceneList.Lobby:
                    break;
                case SceneList.Room:
                    break;
                case SceneList.Game:
                    break;
            }
        }
    }
}
