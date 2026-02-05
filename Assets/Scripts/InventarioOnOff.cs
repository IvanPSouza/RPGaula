using UnityEngine;
using UnityEngine.SocialPlatforms;

public class InventarioOnOff : MonoBehaviour
{
    public GameObject Inventario;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Inventario.activeSelf == true)
            {
            Inventario.gameObject.SetActive(false);
            }
            else if (Inventario.activeSelf == false)
            {
                Inventario.gameObject.SetActive(true);
            }
        }

    }
}
