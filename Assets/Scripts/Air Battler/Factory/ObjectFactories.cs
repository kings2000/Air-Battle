using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public abstract class ObjectFactories : ScriptableObject
{
    Scene contentScene;
    protected T CreateObject<T>(T item, bool moveToScene = true) where T : MonoBehaviour
    {
        Assert.IsTrue(item != null);
        T instance = Instantiate(item);
        if(moveToScene)
            MoveObjetToScene(instance.gameObject);
        return instance;
    }

    protected void MoveObjetToScene(GameObject o)
    {
        if (!contentScene.isLoaded)
        {
            if (Application.isEditor)
            {
                contentScene = SceneManager.GetSceneByName(name);
                if (!contentScene.isLoaded)
                {
                    contentScene = SceneManager.CreateScene(name);
                }
            }
            else
            {
                contentScene = SceneManager.CreateScene(name);
            }

        }

        SceneManager.MoveGameObjectToScene(o, contentScene);
    }
}
