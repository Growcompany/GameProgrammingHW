using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void MuteAll()
    {
        AudioListener.pause = true; // ��� �Ҹ� ����
    }

    public void UnmuteAll()
    {
        AudioListener.pause = false; // ��� �Ҹ� �ѱ�
    }
}
