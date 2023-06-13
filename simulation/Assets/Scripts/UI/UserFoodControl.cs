using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static UnityEditor.PlayerSettings;
using UnityEngine.EventSystems;

public class UserFoodControl : MonoBehaviour
{
    [SerializeField] private GameObject food;
    [SerializeField] private GameObject foodParentObject;
    [SerializeField] private GameObject mouseFoodParentObject;

    Vector3 pos;
    GameObject foodObject;

    // Start is called before the first frame update
    void Start()
    {
        CreateMousePointerObject();
    }

    void CreateMousePointerObject() {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(pos.x, pos.y, mouseFoodParentObject.transform.position.z);
        foodObject = Instantiate(food, pos, Quaternion.identity);
        foodObject.transform.SetParent(mouseFoodParentObject.transform);
        foodObject.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(pos.x, pos.y, mouseFoodParentObject.transform.position.z);
        foodObject.transform.position = pos;

        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI()) {
            pos = new Vector3(pos.x, pos.y, foodParentObject.transform.position.z);
            
            GameObject newSpawnedObject = Instantiate(food, pos, Quaternion.identity);
            SpriteRenderer newSpawnedObjectSpriteRenderer = newSpawnedObject.GetComponent<SpriteRenderer>();
            
            newSpawnedObjectSpriteRenderer.sprite = foodObject.GetComponent<SpriteRenderer>().sprite;
            newSpawnedObject.transform.SetParent(foodParentObject.transform);
            newSpawnedObject.layer = LayerMask.NameToLayer("FoodSources");
            
            AudioManager.instance.Play("EnvironmentPlacement");
            Destroy(foodObject);
            CreateMousePointerObject();

            GameManager.instance.SetFoodSourceCount(GameManager.instance.GetFoodSourceCount()-1);
        }
    }

    private bool IsMouseOverUI() {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
