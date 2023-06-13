using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserClearControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI()) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits2D = Physics2D.GetRayIntersectionAll(ray);
            for (int i = 0; i < hits2D.Length; i++) {
                if (hits2D[i].collider.gameObject.layer == LayerMask.NameToLayer("FoodSources") || hits2D[i].collider.gameObject.layer == LayerMask.NameToLayer("RadiationAreas")) {
                    Destroy(hits2D[i].collider.gameObject);
                    AudioManager.instance.Play("EnvironmentClear");
                    break;
                }
            }
        }
    }

    private bool IsMouseOverUI() {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
