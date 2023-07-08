[System.Serializable]
public class CreatureData
{
    // unique to a life time
    public float maxHealth;
    public float maxAge;
    public float moveSpeed;
    public float sensorDistance;
    public float fitnessGene;
    public float reproductionFitness;
    public float mutationRatio;
    public float hungerLimitToDeath;
    public float reproductionHealth;

    // limits imposed to avoid crazy scenarios
    private float healthLimit = 100f;
    private float ageLimit = 5 * 60f;
    private float moveSpeedLimit = 20f;
    private float sensorDistanceLimit = 20f;
    private float fitnessGeneLimit = 1f;

    public CreatureData()
    {
        this.maxHealth = 10f;
        this.maxAge = 1 * 60f;
        this.moveSpeed = 1f;
        this.sensorDistance = 5f;
        this.fitnessGene = 0.5f;
        this.reproductionFitness = this.fitnessGene * 0.8f;
        this.mutationRatio = 0.2f;
        this.hungerLimitToDeath = this.maxAge * 0.1f;
        this.reproductionHealth = this.maxHealth * 0.8f;
    }

    public CreatureData(Creature creature)
    {
        if (creature.creatureData.maxHealth > healthLimit)
            this.maxHealth = healthLimit;
        else if (creature.creatureData.maxHealth > 0)
            this.maxHealth = creature.creatureData.maxHealth;
        else
            this.maxHealth = 0;

        if (creature.creatureData.maxAge > ageLimit)
            this.maxAge = ageLimit;
        else if (creature.creatureData.maxAge > 0)
            this.maxAge = creature.creatureData.maxAge;
        else
            this.maxAge = 0;

        if (creature.creatureData.moveSpeed > moveSpeedLimit)
            this.moveSpeed = moveSpeedLimit;
        else if (creature.creatureData.moveSpeed > 0)
            this.moveSpeed = creature.creatureData.moveSpeed;
        else
            this.moveSpeed = 0;

        if (creature.creatureData.sensorDistance > sensorDistanceLimit)
            this.sensorDistance = sensorDistanceLimit;
        else if (creature.creatureData.sensorDistance > 0)
            this.sensorDistance = creature.creatureData.sensorDistance;
        else
            this.sensorDistance = 0;

        if (creature.creatureData.fitnessGene > fitnessGeneLimit)
            this.fitnessGene= fitnessGeneLimit;
        else if (creature.creatureData.fitnessGene > 0)
            this.fitnessGene = creature.creatureData.fitnessGene;
        else
            this.fitnessGene = 0;
    }

    public void SetCreatureData(float maxHealth, float maxAge, float moveSpeed, float sensorDistance) {
        if (maxHealth > healthLimit)
            this.maxHealth = healthLimit;
        else if (maxHealth > 0)
            this.maxHealth = maxHealth;
        else
            this.maxHealth = 0;

        if (maxAge > ageLimit)
            this.maxAge = ageLimit;
        else if (maxAge > 0)
            this.maxAge = maxAge;
        else
            this.maxAge = 0;

        if (moveSpeed > moveSpeedLimit)
            this.moveSpeed = moveSpeedLimit;
        else if (moveSpeed > 0)
            this.moveSpeed = moveSpeed;
        else
            this.moveSpeed = 0;

        if (sensorDistance > sensorDistanceLimit)
            this.sensorDistance = sensorDistanceLimit;
        else if (sensorDistance > 0)
            this.sensorDistance = sensorDistance;
        else
            this.sensorDistance = 0;
    }
}