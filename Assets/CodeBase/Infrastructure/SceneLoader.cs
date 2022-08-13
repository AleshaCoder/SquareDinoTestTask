using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
          _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null, bool reload = false) =>
          _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded, reload));

        public IEnumerator LoadScene(string nextScene, Action onLoaded = null, bool reload = false)
        {
            if (SceneManager.GetActiveScene().name == nextScene && (reload == false))
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
