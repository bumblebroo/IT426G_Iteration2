using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelOnTrigger : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;

    public void SetNextSceneName(string nextSceneName) {
        this.nextSceneName = nextSceneName;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.GetComponent<PlayerMovement>()) {
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
