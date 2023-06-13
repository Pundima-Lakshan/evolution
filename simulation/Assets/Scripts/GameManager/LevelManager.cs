using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private GameObject loaderScreen;
    [SerializeField] private Image progressBar;

    private float target; 

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    public async void LoadScene(string sceneName) {
        if(progressBar!= null) {
            progressBar.fillAmount = 0;
            target = 0;
        }        

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loaderScreen.SetActive(true);

        do {
            await Task.Delay(1000); // no need delay
            target = scene.progress / 0.9f;
            Debug.Log(scene.progress);
        } while (scene.progress < 0.9f);
                
        await Task.Delay(1000); // no need delay

        scene.allowSceneActivation = true;
        loaderScreen.SetActive(false);
    }

    void Update() {
        if (progressBar != null) {
            progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 3 * Time.deltaTime);
        }
    }

}
