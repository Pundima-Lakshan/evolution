using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.EventSystems;
using TMPro;

public class UserRadiationControl : MonoBehaviour
{
    [SerializeField] private GameObject radiation;
    [SerializeField] private GameObject radiationParentObject;
    [SerializeField] private GameObject mouseRadiationParentObject;
    [SerializeField] private TextMeshProUGUI radiationCount;

    Vector3 pos;
    GameObject radiationObject;

    // Start is called before the first frame update
    void Start()
    {
        CreateMousePointerObject();
    }

    void CreateMousePointerObject() {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(pos.x, pos.y, mouseRadiationParentObject.transform.position.z);
        radiationObject = Instantiate(radiation, pos, Quaternion.identity);
        radiationObject.transform.SetParent(mouseRadiationParentObject.transform);
        radiationObject.transform.position = pos;
        radiationObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(pos.x, pos.y, mouseRadiationParentObject.transform.position.z);
        radiationObject.transform.position = pos;

        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI()) {
            pos = new Vector3(pos.x, pos.y, radiationParentObject.transform.position.z);

            GameObject newSpawnedObject = Instantiate(radiation, pos, Quaternion.identity);
            
            newSpawnedObject.transform.SetParent(radiationParentObject.transform);
            newSpawnedObject.layer = LayerMask.NameToLayer("RadiationAreas");
            
            AudioManager.instance.Play("EnvironmentPlacement");
            Destroy(radiationObject);
            CreateMousePointerObject();
            radiationCount.text = GameManager.instance.GetRadiationZoneCount().ToString();
        }
    }

    private bool IsMouseOverUI() {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
