using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationInitializer : MonoBehaviour
{
    public string nextSceneName = "reduction";

    void Start()
    {
        Initialize();
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }

    private void Initialize()
    {
        SharedInitialization();
        if (Application.isEditor) EditorSpecificInitialization();
        else DeploySpecificInitialization();
    }

    /// <summary>
    /// Initialization that always needs to be done, indepedent of the appliction.
    /// </summary>
    private void SharedInitialization()
    {
        SimpleFileBrowser.FileBrowser.SetDefaultFilter(".obj");
    }

    /// <summary>
    /// Initialization that only needs to be done if we are running from a deployed application.
    /// </summary>
    private void DeploySpecificInitialization()
    {
        Debug.Log("DeployInitialization");
    }

    /// <summary>
    /// Initialization that only needs to be done if wer are running from the editor.
    /// </summary>
    private void EditorSpecificInitialization()
    {
        Random.InitState(42);
    }
}
