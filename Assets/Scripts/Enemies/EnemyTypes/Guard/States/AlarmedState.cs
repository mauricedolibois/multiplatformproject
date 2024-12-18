using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AlarmedState : AStateBehaviour
{
    
    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("ALARMED");
        EnemySignToggle signToggle = GetComponent<EnemySignToggle>();
        signToggle.ShowExclamationMark();
        
        PlayerMovement.Instance.SetMovementAllowed(false);
        StartCoroutine(WaitAndLoadScene(2f));
    }

    public override void OnStateUpdate()
    {
        // Handle alarm behavior
    }

    public override void OnStateEnd()
    {
        // Cleanup if necessary
    }

    public override int StateTransitionCondition()
    {
        return (int)EGuardState.Invalid; // Default: no transition
    }
    
    private IEnumerator WaitAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(2);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        GameObject inventory = GameObject.FindGameObjectWithTag("inv");
        inventory.GetComponent<InventoryManagement>().inventoryBlock = true;
        inventory.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
