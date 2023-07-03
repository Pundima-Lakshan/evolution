using UnityEngine;

public class FoodSource : MonoBehaviour
{
    private float percentage = 100f;

    [SerializeField] private Sprite[] foodSource1;
    [SerializeField] private Sprite[] foodSource2;
    private Sprite[][] foodSources;
    private int foodSourceSpriteIndex;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private float foodPercentage = 100f;
    private const float foodPercentageReduction = 0.1f;

    public void ReduceFood(Creature creature) {
        foodPercentage -= foodPercentageReduction;
        if (foodPercentage <= 0) {
            creature.isEating = false;
            Destroy(gameObject);
        }
        else if(foodPercentage <= 33) {
            spriteRenderer.sprite = foodSources[foodSourceSpriteIndex][2];
        }
        else if(foodPercentage <= 66) {
            spriteRenderer.sprite = foodSources[foodSourceSpriteIndex][1];
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Creature") {
            Creature creature = collision.gameObject.GetComponent<Creature>();
            if(creature == null) {
                Debug.Log("creature not found in trigger food");
                return;
            }
            creature.Collided("Food");
            if(creature.isEating)
                ReduceFood(creature);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foodSources = new Sprite[][] { foodSource1, foodSource2 };
        foodSourceSpriteIndex = Random.Range(0, foodSources.Length);
        spriteRenderer.sprite = foodSources[foodSourceSpriteIndex][0];
    }
}
