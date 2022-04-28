using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class VR_UI : MonoBehaviour
{
    public AudioSource musicObject;
    public GameObject ball;
    public GameObject dayNightCycleObject;
    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void ChangeMusicVolume(float f)
    {
        musicObject.volume = f;
    }

    public void ChangeSFXVolume(float s)
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("SFX"))
        {
            g.GetComponent<AudioSource>().volume = s;
        }
    }

    public void SpawnBall()
    {
        GameObject.Instantiate(ball, this.gameObject.transform.position, Quaternion.identity);
    }

    public void ChangeTime(float t)
    {
        dayNightCycleObject.transform.SetPositionAndRotation(dayNightCycleObject.transform.position, Quaternion.Euler(0, 0, t));
    }

    public void ResetButton()
    {
        SceneManager.LoadScene(0);
    }
}
