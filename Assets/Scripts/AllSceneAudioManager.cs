using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSceneAudioManager : MonoBehaviour
{
	[SerializeField]
	private AudioClip m_title_bgm;
	[SerializeField]
	private AudioClip m_main_bgm;
	[SerializeField]
	private AudioClip m_result_bgm;

	private AudioSource m_audioSource;

	public void Init()
	{
		m_audioSource = this.gameObject.GetComponent<AudioSource>();
	}

	public void StopBGM()
	{
		m_audioSource.Stop();
	}

	public void PlayTitleBGM()
	{
		m_audioSource.clip = m_title_bgm;
		m_audioSource.Play();
	}

	public void PlayMainBGM()
	{
		m_audioSource.clip = m_main_bgm;
		m_audioSource.Play();
	}

	public void PlayResultBGM()
	{
		m_audioSource.clip = m_result_bgm;
		m_audioSource.Play();
	}
}
