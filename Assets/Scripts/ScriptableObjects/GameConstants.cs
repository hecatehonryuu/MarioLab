using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    [Header("Stuff")]
    public int maxLives;

    [Header("Mario Properties")]
    public int speed;
    public int maxSpeed;
    public int upSpeed;
    public int deathImpulse;
    public float flickerInterval;

    [Header("Goomba Properties")]
    public float goombaPatrolTime;
    public float goombaMaxOffset;
}
