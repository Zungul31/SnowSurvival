using Cysharp.Threading.Tasks;
using UnityEngine;

public class Spawner : BasicInteractiveObj
{
    [SerializeField] private SpawnerController controller;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite aliveSprite;
    [SerializeField] private Sprite deadSprite;
    
    [SerializeField] private int maxHealth;
    private int currentHealth;
    
    [SerializeField] private float spawnTime;
    [SerializeField] private float recoveryTime;

    private bool isAlive;

    private void Awake()
    {
        currentHealth = maxHealth;
        isAlive = true;
        isConectd = false;
    }

    public override void Connected()
    {
        isConectd = true;
        StartSpawn().Forget();
    }

    public override void Disconnected()
    {
        isConectd = false;
    }

    private async UniTask StartSpawn()
    {
        while (isConectd && isAlive)
        {
            await UniTask.WaitForSeconds(spawnTime);
            controller.SetItem(this.transform.position, givenItemType);
            currentHealth--;
            isAlive = currentHealth > 0;
        }

        if (!isAlive)
        {
            StartRecovery().Forget();
            sprite.sprite = deadSprite;
        }
    }

    private async UniTask StartRecovery()
    {
        await UniTask.WaitForSeconds(recoveryTime);
        isAlive = true;
        currentHealth = maxHealth;
        sprite.sprite = aliveSprite;
        
        if (isConectd)
        {
            StartSpawn().Forget();
        }
    }
}