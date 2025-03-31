using UnityEngine;

public class ShieldCrackManager : MonoBehaviour
{
    public GameObject[] cracks;
    public GameObject shieldObject;
    void Start()
    {
    
    }

    public void UpdateShieldDisplay(int maxShieldStrength, int currentShieldStrength)
    {
        int cracksToShow = maxShieldStrength - currentShieldStrength;

        if (currentShieldStrength <= 0)
        {
            // Hide the shield's visual but keep its collider active
            shieldObject.GetComponent<SpriteRenderer>().enabled = false;
            
            foreach (GameObject crack in cracks)
            {
                crack.SetActive(false);
                Debug.Log($"Crack {crack} deactivated.");
            }
        }
        else{
            for(int i = 0; i<cracksToShow; i++)
            {
                cracks[i].SetActive(true);
                Debug.Log($"Crack {i} activated.");
            }
        }
    }
}
