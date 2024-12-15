using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void MuteAll()
    {
        AudioListener.pause = true; // 葛电 家府 掺扁
    }

    public void UnmuteAll()
    {
        AudioListener.pause = false; // 葛电 家府 难扁
    }
}
