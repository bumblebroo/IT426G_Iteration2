using UnityEngine;

public class MainMusicHandler : MonoBehaviour
{
    private static MainMusicHandler instance;
    void Start()
    {
        if (instance) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }
}
