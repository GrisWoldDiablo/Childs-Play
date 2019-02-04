using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    private AsyncOperation async;

    public void Load()
    {
        if (async == null)
        {
            Scene curScene = SceneManager.GetActiveScene();
            try
            {
                if ((async = SceneManager.LoadSceneAsync(curScene.buildIndex + 1)) != null)
                {
                    async.allowSceneActivation = true;
                }
                
            }
            catch (System.Exception e)
            {
                Debug.Log("No Scene found");
                Debug.LogException(e, this);
            }
        }
    }

    public void Load(int sceneIndex)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(sceneIndex);
            if (async != null)
            {
                async.allowSceneActivation = true;
            }
            else
            {
                Debug.Log("No Scene found");
            }
        }
    }

    public void Load(string sceneName)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(sceneName);
            if (async != null)
            {
                async.allowSceneActivation = true;
            }
            else
            {
                Debug.Log("No Scene found");
            }
        }
    }

}


