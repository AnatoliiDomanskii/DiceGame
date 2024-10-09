using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infractrucure
{
    public class SceneLoader
    {
        public async void LoadSceneAsync(string sceneName, Action onLoaded = null)
        {
            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName);

            while (!sceneLoadOperation.isDone)
            {
                await Task.Yield();
            }

            onLoaded?.Invoke();
        }
    }
}

