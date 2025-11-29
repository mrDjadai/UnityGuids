using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)), out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
