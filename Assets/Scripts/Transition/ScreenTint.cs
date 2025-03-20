using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyStardewValleylikeGame
{
    public class ScreenTint : MonoBehaviour
    {
        #region Variables
        // 색상 변수: 초기(색이 없는) 상태 색상과, 틴트가 적용된 색상
        [SerializeField] Color unTintedColor;
        [SerializeField] Color tintedColor;

        // 틴트 효과가 진행되는 시간 (0~1 사이의 값으로 틴트 진행 상태를 제어)
        public float tintedTime;
        // 틴트 효과 진행 속도 (높아질수록 빠름)
        public float tintedSpeed;

        // UI 이미지 컴포넌트를 저장할 변수 (스크린 틴트를 적용할 대상)
        Image image;

        // 틴트 효과 코루틴을 저장할 변수
        Coroutine tintCoroutine;
        Coroutine unTintCoroutine;


        #endregion
        private void Awake()
        {
            // 컴포넌트 초기화: 스크린 틴트를 적용할 UI 이미지 컴포넌트를 가져옴
            image = GetComponent<Image>();
        }

        // 틴트 시작 함수 (스크린을 틴트 색으로 변경)
        public void Tint()
        {
            //이미 틴트 진행 중이면 함수 종료
            if (tintCoroutine != null) return;

            //틴트해제가 진행 중이라면 멈추고 틴트 해제를 시작
            if (unTintCoroutine != null)
            {
                StopCoroutine(unTintCoroutine);
                unTintCoroutine = null;
            }

            //틴트 효과 코루틴 시작
            tintCoroutine = StartCoroutine(ChangeTintScreen(true));  //true -> 틴트를 해제
        }

        // 틴트 해제 함수 (스크린을 원래 색으로 되돌림)
        public void UnTint()
        {
            //이미 틴트 해제가 진행 중이면 함수 종료
            if (unTintCoroutine != null) return;

            //틴트가 진행 중이라면 멈추고 틴트 해제를 시작
            if (tintCoroutine != null)
            {
                StopCoroutine(tintCoroutine);
                tintCoroutine = null;
            }

            //틴트 해제 효과 코루틴 시작
            unTintCoroutine = StartCoroutine(ChangeTintScreen(false));  //false -> 틴트를 해제
        }

        private IEnumerator ChangeTintScreen(bool isTinting)
        {
            // tinting일 경우 tintedTime을 1로, unTinting일 경우 0으로 목표 설정
            float targetTime = isTinting ? 1f : 0f;
            float direction = isTinting ? 1f : -1f; // tinting일 경우 1로 증가, unTinting일 경우 -1로 감소
            //목표 시간에 도달할 때까지 반복
            while (!Mathf.Approximately(tintedTime, targetTime))
            {
                // tintedTime을 direction만큼 증가
                tintedTime += Time.deltaTime * tintedSpeed * direction;
                //tintedTime이 0보다 작거나 1보다 크면 0 또는 1로 고정
                tintedTime = Mathf.Clamp01(tintedTime);

                // 이미지의 색상을 unTintedColor와 tintedColor 사이의 비율로 설정
                image.color = Color.Lerp(unTintedColor, tintedColor, tintedTime);
                // 한 프레임 대기
                yield return new WaitForEndOfFrame();
            }
            if (isTinting)
            {
                tintCoroutine = null;
            }
            else
            {
                unTintCoroutine = null;
            }
        }
    }
}
