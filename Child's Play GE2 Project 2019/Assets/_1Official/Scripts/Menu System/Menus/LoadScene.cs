using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    private AsyncOperation _async;

    /// <summary>
    /// Called to load a new scene, will load the next scene in line.
    /// </summary>
    public void Load()
    {
        if (_async == null)
        {
            Scene curScene = SceneManager.GetActiveScene();
            int nextScene = curScene.buildIndex + 1;
            if (nextScene >= SceneManager.sceneCountInBuildSettings)
            {
                nextScene = 0;
                Debug.Log("Last scene werapping arround to first scene.");
            }
            if ((_async = SceneManager.LoadSceneAsync(nextScene)) != null)
            {
                _async.allowSceneActivation = true;
            }
        }
    }

    /// <summary>
    /// Called to load a new scene, will load the scene based on the param
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to load</param>
    public void Load(int sceneIndex)
    {
        if (_async == null)
        {
            _async = SceneManager.LoadSceneAsync(sceneIndex);
            if (_async != null)
            {
                _async.allowSceneActivation = true;
            }
            else
            {
                Debug.Log("No Scene found");
            }
        }
    }

    /// <summary>
    /// Call to load a new scene, will load the scene based on the param 
    /// </summary>
    /// <param name="sceneName">Name of the scene to load</param>
    public void Load(string sceneName)
    {
        if (_async == null)
        {
            _async = SceneManager.LoadSceneAsync(sceneName);
            if (_async != null)
            {
                _async.allowSceneActivation = true;
            }
            else
            {
                Debug.Log("No Scene found");
            }
        }
    }

}


