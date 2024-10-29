using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransitionName { get; private set; }

    public void SetTransitionName(string sceneTransitionName) {
        this.SceneTransitionName = sceneTransitionName;
    }
    
    public UnityEvent OnReady;

    protected override void Awake() {
        base.Awake();
        Debug.Log("SceneManagement Awake: Instance is ready."); 
        OnReady?.Invoke();
    }

    
}


