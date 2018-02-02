using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationInitializer : MonoBehaviour
{
    public string nextSceneName = "reduction";

	void Start()
	{
        DontDestroyOnLoad(gameObject);

        Initialize();

        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
	}

    private void Initialize(){
        SharedInitialization();
        DeploySpecificInitialization();
        EditorSpecificInitialization();
    }

    private void SharedInitialization(){
        Debug.Log("SharedInitialization");
    }

    private void DeploySpecificInitialization(){
        Debug.Log("DeployInitialization");
    }

    private void EditorSpecificInitialization(){
        Debug.Log("EditorInitialization");
    }
}
