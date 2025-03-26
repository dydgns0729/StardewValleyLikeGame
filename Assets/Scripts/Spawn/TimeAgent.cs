using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyStardewValleylikeGame
{
    // 시간에 흐름에따라 게임 내의 여러 요소들을 업데이트하는 클래스
    public class TimeAgent : MonoBehaviour
    {
        #region Variables
        public Action onTimeTick; // 시간이 흐를 때 호출할 함수

        private bool isSubscribed = false; // 구독 여부
        #endregion


        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (isSubscribed) return;
            // 게임 매니저의 시간 컨트롤러에 이 클래스를 등록
            isSubscribed = true;
            GameManager.Instance.dayTimeController.Subscribe(this);

        }

        public virtual void OnDestroy()
        {
            if (!isSubscribed) return;
            // 게임 매니저의 시간 컨트롤러에 이 클래스를 등록 해제
            GameManager.Instance.dayTimeController.Unsubscribe(this);
            isSubscribed = false;
        }

        private void OnApplicationQuit()
        {
            // 게임 종료 시 구독 해제
            OnDestroy();
        }

        public void Invoke()
        {
            // 시간이 흐를 때마다 호출되는 함수
            // 여기에 시간에 따라 변화해야 하는 요소들을 업데이트
            onTimeTick?.Invoke();
        }
    }
}