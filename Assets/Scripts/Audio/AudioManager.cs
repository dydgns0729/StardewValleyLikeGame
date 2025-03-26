using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // AudioManager는 게임 내에서 오디오를 관리하는 싱글톤 클래스입니다.
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        #region Variables
        // 오디오 소스를 생성할 프리팹
        [SerializeField] GameObject audioSourcePrefab;
        // 생성할 오디오 소스의 개수
        [SerializeField] int audioSourceCount;

        // 오디오 소스 목록을 저장할 리스트
        List<AudioSource> audioSources;
        #endregion

        // 초기화 메서드 (게임 시작 시 호출)
        private void Start()
        {
            // AudioManager 초기화
            Init();
        }

        // AudioManager의 초기 설정을 처리하는 메서드
        private void Init()
        {
            // 오디오 소스 리스트를 초기화
            audioSources = new List<AudioSource>();

            // 지정된 개수만큼 오디오 소스를 생성하고 리스트에 추가
            for (int i = 0; i < audioSourceCount; i++)
            {
                // 오디오 소스 프리팹을 인스턴스화
                GameObject go = Instantiate(audioSourcePrefab, transform);
                // 오디오 소스는 부모 객체에 추가되며, 로컬 위치를 (0,0,0)으로 설정
                go.transform.localPosition = Vector3.zero;
                // 생성된 오디오 소스를 리스트에 추가
                audioSources.Add(go.GetComponent<AudioSource>());
            }
        }

        // 주어진 오디오 클립을 재생하는 메서드
        public void Play(AudioClip audioClip)
        {
            // 오디오 클립이 null이면 아무것도 하지 않음
            if (audioClip == null) return;

            // 사용 가능한 오디오 소스를 가져옴
            AudioSource audioSource = GetFreeAudioSource();

            // 오디오 소스에 클립을 설정하고 재생
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        // 현재 사용되지 않는 오디오 소스를 찾아 반환하는 메서드
        private AudioSource GetFreeAudioSource()
        {
            // 모든 오디오 소스를 확인하여 사용 중이지 않은 소스를 찾음
            for (int i = 0; i < audioSources.Count; i++)
            {
                if (!audioSources[i].isPlaying)
                {
                    return audioSources[i];  // 사용 중이지 않으면 해당 오디오 소스를 반환
                }
            }
            // 모든 오디오 소스가 사용 중일 경우, 첫 번째 오디오 소스를 반환 (대체)
            return audioSources[0];
        }
    }
}
