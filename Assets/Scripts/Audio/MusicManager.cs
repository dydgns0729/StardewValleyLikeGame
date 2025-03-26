using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // MusicManager는 게임의 배경 음악을 제어하는 싱글톤 클래스입니다.
    public class MusicManager : PersistentSingleton<MusicManager>
    {
        #region Variables
        // 배경 음악을 재생할 AudioSource
        [SerializeField] AudioSource audioSource;

        // 게임 시작 시 재생될 음악 클립
        [SerializeField] AudioClip playOnStart;
        // 추가적인 음악 클립
        [SerializeField] AudioClip audioClip2;

        // 음악 전환 시 볼륨이 변경되는 속도 (초 단위)
        [SerializeField] float volumeChangeSpeedtoSecond = 1f;

        // 현재 전환 중인 음악
        AudioClip swtichTo;

        // 음악 볼륨 (0~1 범위)
        float volume = 1f;
        #endregion

        // 게임 시작 시 초기 음악을 재생
        private void Start()
        {
            Play(playOnStart, true);
        }

        // 음악을 재생하는 메서드
        // interrupt: true일 경우 기존 음악을 끄고 새로운 음악을 재생, false일 경우 부드럽게 전환
        public void Play(AudioClip musicToPlay, bool interrupt = false)
        {
            if (musicToPlay == null) return;

            // interrupt가 true이면 기존 음악을 끄고 새로운 음악을 즉시 재생
            if (interrupt)
            {
                audioSource.clip = musicToPlay;
                audioSource.Play();

                // 음악을 부드럽게 전환하기 위해 SmoothSwtichMusicIn 코루틴 실행
                StartCoroutine(SmoothSwtichMusicIn());
            }
            else
            {
                // interrupt가 false일 경우, 새로운 음악을 부드럽게 전환하기 위해 저장 후 SmoothSwitchMusicOut 코루틴 실행
                swtichTo = musicToPlay;
                StartCoroutine(SmoothSwitchMusicOut());
            }
        }

        // 음악을 부드럽게 줄여가며 전환하는 코루틴
        IEnumerator SmoothSwitchMusicOut()
        {
            volume = 1f;

            // 볼륨이 0보다 클 때까지 서서히 볼륨을 줄여감
            while (volume > 0)
            {
                volume = Mathf.Max((volume -= (Time.deltaTime / volumeChangeSpeedtoSecond)), 0);

                // audioSource의 볼륨을 변경
                audioSource.volume = volume;
                yield return new WaitForEndOfFrame();
            }

            // 볼륨이 0이 되면, 새로운 음악을 재생 (부드럽게 전환)
            Play(swtichTo, true);
        }

        // 음악을 부드럽게 키워가며 전환하는 코루틴
        IEnumerator SmoothSwtichMusicIn()
        {
            volume = 0f;

            // 볼륨이 1보다 작을 때까지 서서히 볼륨을 증가시킴
            while (volume < 1)
            {
                volume = Mathf.Min((volume += (Time.deltaTime / volumeChangeSpeedtoSecond)), 1);
                audioSource.volume = volume;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
